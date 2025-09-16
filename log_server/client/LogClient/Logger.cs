using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Globalization;
using System.Text;

namespace LogClient
{
    /// <summary>
    /// Asynchronous, thread-safe logger that buffers log entries in memory and flushes them to disk
    /// from a dedicated background thread. Supports bounded restart attempts for the worker thread,
    /// input sanitization/validation, and a non-throwing mode.
    /// </summary>
    /// <remarks>
    /// - Log entries are added via <see cref="Add"/> and written to a daily log file.
    /// - A background worker consumes a FIFO queue; on worker failure, buffered items are re-enqueued
    ///   to a priority LIFO stack to preserve original order on retry.
    /// </remarks>
    public class Logger : IDisposable
    {
        private bool _disposed;

        private bool _noThrow = false;
        private readonly string LogFolderPath = "./log";
        private readonly int _logFlushThreshold = 10;
        private readonly string _serverId;
        private readonly string _serverIdExt = "";
        private readonly string _logFilePath;

        private bool _isLoggerThreadAlive;
        private int _currentLoggerThreadRestart = 0;
        private readonly int _maxLoggerThreadRestart = 5;
        private string _lastLoggerThreadExceptionMessage = "";

        private Thread _logWorker;
        private readonly CancellationTokenSource _cts = new();

        // Main FIFO queue for new log entries (thread-safe)
        private readonly BlockingCollection<string> _logQueue = new(new ConcurrentQueue<string>());

        // Guard to coordinate Add() and re-enqueue operations
        private readonly object _queueGuard = new();

        // Priority LIFO used to simulate "push to front" semantics on failure re-enqueue
        private readonly ConcurrentStack<string> _frontQueue = new();

        private readonly FrozenSet<string> _allowedTypes =
            new[] { "INFO", "WARNING", "ERROR", "CRITICAL ERROR", "NOTIFICATION", "ATTENTION" }.ToFrozenSet();

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class and starts the background worker thread.
        /// </summary>
        /// <param name="serverId">Mandatory server identifier written into each log entry; sanitized.</param>
        /// <param name="serverIdExt">Optional server identifier extension; sanitized (may be empty).</param>
        /// <param name="createFolder">If true, the log directory will be created if it does not exist.</param>
        /// <param name="logFlushThreshold">Number of queued items to accumulate before a disk flush.</param>
        /// <param name="noThrow">If true, the logger attempts to recover and return false instead of throwing on validation errors.</param>
        /// <exception cref="LoggerException">Thrown if identifiers are invalid, the folder is not writable, or directory creation fails.</exception>
        public Logger(string serverId, string serverIdExt = "", bool createFolder = true, int logFlushThreshold = 10, bool noThrow = false)
        {
            Sanitize(ref serverId);
            if (serverId == string.Empty)
                throw new LoggerException("Server identifier must not be empty.");

            Sanitize(ref serverIdExt);
            this._serverId = serverId;
            this._serverIdExt = serverIdExt;

            this._logFlushThreshold = logFlushThreshold;
            this._noThrow = noThrow;

            CheckLogFolder(createFolder);
            this._logFilePath = Path.Combine(LogFolderPath, GenerateLogFileName());
            StartLoggerThread();
        }

        /// <summary>
        /// Starts the background logger worker thread that drains queues and writes to disk.
        /// </summary>
        private void StartLoggerThread()
        {
            this._isLoggerThreadAlive = true;
            // Create a dedicated background thread to persist buffered entries
            this._logWorker = new Thread(loggerThread) { IsBackground = true, Name = "LoggerWorker" };
            this._logWorker.Start();
        }

        /// <summary>
        /// Ensures the log folder exists and is writable; can create the folder if requested.
        /// </summary>
        /// <param name="createFolder">If true, attempts to create the folder when it does not exist.</param>
        /// <exception cref="LoggerException">Thrown when the folder is absent (and not created) or not writable.</exception>
        private void CheckLogFolder(bool createFolder = true)
        {
            if (!Directory.Exists(this.LogFolderPath))
            {
                if (createFolder)
                {
                    try
                    {
                        Directory.CreateDirectory(this.LogFolderPath);
                    }
                    catch (Exception ex)
                    {
                        throw new LoggerException($"Directory creation failed for path: {this.LogFolderPath}.", ex);
                    }
                }
                else
                {
                    throw new LoggerException($"Directory doesn't exist: {this.LogFolderPath}. Create folder or set createFolder = true");
                }
            }

            // Probe write permission by creating and removing a temporary file
            try
            {
                string randomFileName = Path.Combine(this.LogFolderPath, Logger.GenerateRandomFileName());
                using (FileStream fs = File.Create(randomFileName)) { } // probe write permission
                File.Delete(randomFileName); // cleanup probe file
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new LoggerException($"Unable to write to directory '{this.LogFolderPath}': operation not permitted.", ex);
            }
            catch (Exception ex)
            {
                throw new LoggerException($"Unable to write to directory '{this.LogFolderPath}': unknown error.", ex);
            }
        }

        /// <summary>
        /// Worker routine that continuously drains the priority stack and main queue,
        /// buffering entries and appending to the log file when the flush threshold is reached.
        /// </summary>
        /// <remarks>
        /// On unhandled exception, buffered items are re-enqueued to the priority stack to be retried first.
        /// On graceful shutdown (via <see cref="_cts"/>), remaining items are flushed to disk.
        /// </remarks>
        private void loggerThread()
        {
            List<string> buffer = new List<string>();
            bool error = false;
            try
            {
                while (!this._cts.IsCancellationRequested)
                {
                    // Take from the "front" requeue first (priority), without blocking the guard for long.
                    if (!_frontQueue.IsEmpty && _frontQueue.TryPop(out var frontItem))
                    {
                        buffer.Add(frontItem);
                    }
                    else
                    {
                        // Block until an item is available or cancellation is requested.
                        var item = _logQueue.Take(this._cts.Token);
                        buffer.Add(item);
                    }

                    // Flush to disk when the buffer reaches the threshold.
                    if (buffer.Count >= this._logFlushThreshold)
                    {
                        File.AppendAllLines(this._logFilePath, buffer);
                        buffer.Clear();
                    }
                }
            }
            catch (OperationCanceledException) { /* graceful shutdown */ }
            catch (Exception ex)
            {
                error = true;
                this._lastLoggerThreadExceptionMessage = $"Unhandled exception in logger thread. Details: {ex.Message}";
            }
            finally
            {
                this._isLoggerThreadAlive = false;

                if (error)
                {
                    // Re-enqueue buffered items to the FRONT so the next worker retries them first.
                    // Use reversed order to preserve original FIFO when popping from a LIFO stack.
                    lock (_queueGuard)
                    {
                        for (int i = buffer.Count - 1; i >= 0; i--)
                        {
                            _frontQueue.Push(buffer[i]);
                        }
                    }
                }
                else
                {
                    // Normal shutdown (Dispose): drain queue and persist what's left.
                    while (_logQueue.TryTake(out var item))
                    {
                        buffer.Add(item);
                    }
                    if (buffer.Count > 0)
                    {
                        File.AppendAllLines(this._logFilePath, buffer);
                    }
                }
            }
        }

        /// <summary>
        /// Normalizes a string by replacing square brackets with parentheses and trimming whitespace.
        /// If the input is null or whitespace, converts it to an empty string.
        /// </summary>
        /// <param name="text">Reference to the text to sanitize (may be null).</param>
        private void Sanitize(ref string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                text = string.Empty;

            // Square brackets are reserved as delimiters in log lines; replace them.
            text = text.Replace("[", "(").Replace("]", ")").Trim();
        }

        /// <summary>
        /// Validates and normalizes the log type against a fixed allowlist.
        /// </summary>
        /// <param name="type">Log type to validate (modified to upper-case and potentially set to "UNDEFINED").</param>
        /// <exception cref="LoggerException">Thrown if the type is not allowed and <see cref="_noThrow"/> is false.</exception>
        private void ValidateType(ref string type)
        {
            Sanitize(ref type);
            type = type.ToUpperInvariant();

            if (!this._allowedTypes.Contains(type))
            {
                if (this._noThrow)
                {
                    type = "UNDEFINED";
                    return;
                }
                throw new LoggerException($"Invalid log type: {type}. Allowed types: {string.Join(", ", this._allowedTypes)}");
            }
        }

        /// <summary>
        /// Validates and sanitizes tags and message: trims/escapes, enforces limits, and optionally truncates.
        /// </summary>
        /// <param name="tags">Optional tag array; sanitized; clamped to maximum of 5 if <see cref="_noThrow"/> is true.</param>
        /// <param name="message">Message text; sanitized; must be 1..256 chars or truncated if <see cref="_noThrow"/> is true.</param>
        /// <returns><c>true</c> when the inputs are valid after sanitation; <c>false</c> when invalid but tolerated in no-throw mode (e.g., empty message).</returns>
        /// <exception cref="LoggerException">Thrown on violations when <see cref="_noThrow"/> is false.</exception>
        private bool ValidateTagsAndMessage(ref string[]? tags, ref string message)
        {
            tags ??= Array.Empty<string>();
            if (tags.Length > 5)
            {
                if (this._noThrow)
                    tags = tags.Take(5).ToArray();
                else
                    throw new LoggerException("Maximum of 5 tags allowed.");
            }

            for (int i = 0; i < tags.Length; i++)
            {
                Sanitize(ref tags[i]);
            }

            Sanitize(ref message);
            if (message == string.Empty)
            {
                if (this._noThrow) return false;
                else throw new LoggerException("Message length must be between 1 and 256 characters; provided message is empty.");
            }

            if (message.Length > 256)
            {
                if (this._noThrow) message = message.Substring(0, 256);
                else throw new LoggerException("Message length must be between 1 and 256 characters; provided message is too long.");
            }

            return true;
        }

        /// <summary>
        /// Adds a log entry to the in-memory queue. Restarts the worker thread on demand (bounded attempts).
        /// </summary>
        /// <param name="type">Log entry type ("INFO", "WARNING", "ERROR", "CRITICAL ERROR", "NOTIFICATION", "ATTENTION"); validated against an allowlist.</param>
        /// <param name="tags">Optional tags (max 5), sanitized.</param>
        /// <param name="message">Message text (1..256 chars), sanitized and possibly truncated in no-throw mode.</param>
        /// <returns>
        /// <c>true</c> if the entry was accepted for logging; <c>false</c> when validation failed but tolerated in no-throw mode.
        /// </returns>
        /// <exception cref="LoggerException">
        /// Thrown when the worker cannot be restarted (ignores <see cref="_noThrow"/>), validation fails, or enqueueing fails and <see cref="_noThrow"/> is false.
        /// </exception>
        public bool Add(string type, string[]? tags, string message)
        {
            // Ensure the worker is alive; attempt bounded restarts if it is not.
            if (!this._isLoggerThreadAlive)
            {
                if (this._currentLoggerThreadRestart < this._maxLoggerThreadRestart)
                {
                    StartLoggerThread();
                    this._currentLoggerThreadRestart++;
                }
                else
                {
                    throw new LoggerException($"Failure during loggerThread restart, too many failed tries. Thread exception message: {this._lastLoggerThreadExceptionMessage}");
                }
            }

            string entry = "";
            try
            {
                string typeCpy = type;
                string messageCpy = message;
                string[]? tagsCpy = tags;

                // Validate and normalize inputs
                ValidateType(ref typeCpy);
                if (!ValidateTagsAndMessage(ref tagsCpy, ref messageCpy) && this._noThrow)
                    return false;

                // Compose a single-line log record with consistent delimiters and timestamp
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                entry = $"[{date}][{typeCpy}][{this._serverId}][{this._serverIdExt}][{string.Join(",", tagsCpy ?? Array.Empty<string>())}][{messageCpy}]";

                // Guard the add so we don't race with re-enqueue operations
                lock (_queueGuard)
                {
                    this._logQueue.Add(entry);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (this._noThrow) return false;
                throw new LoggerException($"Failure during log entry. Problem occurred while processing: {entry}", ex);
            }
        }

        /// <summary>
        /// Generates a unique log file name for today's date in the configured folder.
        /// If a file already exists, appends an incremental suffix.
        /// </summary>
        /// <returns>File name (without directory) for the log file to use.</returns>
        private string GenerateLogFileName()
        {
            string datePart = DateTime.Today.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string baseName = $"log_{datePart}";
            string extension = ".txt";
            string directory = this.LogFolderPath;
            string candidate = Path.Combine(directory, baseName + extension);
            if (!File.Exists(candidate)) return baseName + extension;

            int index = 2;
            while (true)
            {
                string name = $"{baseName}_{index}{extension}";
                string path = Path.Combine(directory, name);
                if (!File.Exists(path)) return name;
                index++;
            }
        }

        /// <summary>
        /// Generates a random file name for probing write access in the log directory.
        /// </summary>
        /// <returns>A random file name with a .txt extension.</returns>
        private static string GenerateRandomFileName()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(20);
            for (int i = 0; i < 20; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString() + ".txt";
        }

        /// <summary>
        /// Releases resources and stops the background worker thread, ensuring queued entries are flushed to disk.
        /// </summary>
        /// <remarks>
        /// Safe to call multiple times. Signals completion to the queue, cancels the token, waits for the worker to join,
        /// then disposes internal resources.
        /// </remarks>
        public void Dispose()
        {
            if (this._disposed) return;
            this._disposed = true;

            // Signal that no more items will be added and request cancellation for the worker
            this._logQueue.CompleteAdding();
            this._cts.Cancel();

            try
            {
                this._logWorker?.Join();
            }
            catch (ThreadStateException) { /* worker might not be started or in invalid state; ignore */ }

            // Dispose owned resources
            this._cts.Dispose();
            this._logQueue.Dispose();
        }
    }
}
