using System.Collections.Concurrent;
using System.Globalization;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LogClient
{
    public sealed class Logger : IDisposable
    {
        private const byte MessageDelimiter = 0x1F;   // server uses b"\x1F"
        private const char BatchSeparator = '\x1E';   // server splits by '\x1E'
        private const int MaxServerMessageSizeBytes = 100 * 1024;
        private const int SafePayloadLimitBytes = 90 * 1024;
        private const int HandshakeTimeoutMs = 3000;

        private readonly bool _noThrow;
        private readonly int _logFlushThreshold;

        private readonly string _serverId;
        private readonly string _serverIdExt;

        private readonly string _logServerHost;
        private readonly int _logServerPort;

        // Paths (lazy loaded)
        private readonly string _clientPrivateKeyPath;
        private readonly string _clientPublicKeyPath;
        private readonly string _serverCertificatePath;

        // Lazy-loaded crypto
        private readonly object _cryptoLock = new();
        private bool _cryptoLoaded;
        private X509Certificate2? _pinnedServerCert;
        private RSA? _clientPrivateKey;
        private RSA? _clientPublicKey;

        // Connection
        private readonly object _connectionLock = new();
        private TcpClient? _tcpClient;
        private SslStream? _sslStream;

        // Backoff to avoid connect spam when files are missing / server down
        private DateTime _nextConnectAttemptUtc = DateTime.MinValue;
        private int _connectBackoffMs = 500; // grows up to max

        // Worker
        private readonly CancellationTokenSource _cts = new();
        private readonly BlockingCollection<string> _logQueue = new(new ConcurrentQueue<string>());
        private readonly ConcurrentStack<string> _frontQueue = new();
        private readonly object _queueGuard = new();
        private Thread? _worker;

        private readonly HashSet<string> _allowedTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "INFO", "WARNING", "ERROR", "CRITICAL ERROR", "NOTIFICATION", "ATTENTION", "DEBUG", "DIAGNOSTICS"
        };

        public Logger(
            string serverId,
            string clientPrivateKeyPath,
            string clientPublicKeyPath,
            string serverCertificatePath,
            string logServerHost = "log_server",
            int logServerPort = 54000,
            int logFlushThreshold = 10,
            bool noThrow = false,
            string serverIdExt = "")
        {
            _noThrow = noThrow;
            _logFlushThreshold = logFlushThreshold > 0 ? logFlushThreshold : 10;

            Sanitize(ref serverId);
            if (string.IsNullOrEmpty(serverId))
                throw new LoggerException("Server identifier must not be empty.");

            Sanitize(ref serverIdExt);

            _serverId = serverId;
            _serverIdExt = serverIdExt;

            _clientPrivateKeyPath = clientPrivateKeyPath ?? "";
            _clientPublicKeyPath = clientPublicKeyPath ?? "";
            _serverCertificatePath = serverCertificatePath ?? "";

            _logServerHost = string.IsNullOrWhiteSpace(logServerHost) ? "log_server" : logServerHost.Trim();
            _logServerPort = logServerPort;

            _worker = new Thread(WorkerLoop) { IsBackground = true, Name = "LoggerWorker" };
            _worker.Start();
        }

        public bool Add(string type, string[]? tags, string message)
        {
            try
            {
                string typeCpy = type;
                string[]? tagsCpy = tags;
                string messageCpy = message;

                ValidateType(ref typeCpy);
                if (!ValidateTagsAndMessage(ref tagsCpy, ref messageCpy))
                    return false;

                // Server expects UTC ISO8601 with microseconds and 'Z'
                string dateUtc = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", CultureInfo.InvariantCulture);

                string entry =
                    $"[{dateUtc}]" +
                    $"[{typeCpy}]" +
                    $"[{_serverId}]" +
                    $"[{_serverIdExt}]" +
                    $"[{string.Join(",", tagsCpy ?? Array.Empty<string>())}]" +
                    $"[{messageCpy}]";

                lock (_queueGuard)
                {
                    _logQueue.Add(entry);
                }

                return true;
            }
            catch (Exception)
            {
                if (_noThrow) return false;
                throw;
            }
        }

        private void WorkerLoop()
        {
            var buffer = new List<string>();

            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    if (!_frontQueue.IsEmpty && _frontQueue.TryPop(out var frontItem))
                    {
                        buffer.Add(frontItem);
                    }
                    else
                    {
                        buffer.Add(_logQueue.Take(_cts.Token));
                    }

                    if (buffer.Count >= _logFlushThreshold)
                    {
                        TrySendOrRequeue(buffer);
                        buffer.Clear();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // shutdown
            }
            catch
            {
                // in case of unexpected worker crash, requeue what we have
                lock (_queueGuard)
                {
                    for (int i = buffer.Count - 1; i >= 0; i--)
                        _frontQueue.Push(buffer[i]);
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        private void TrySendOrRequeue(List<string> entries)
        {
            if (entries.Count == 0)
                return;

            try
            {
                SendEntries(entries);
                // success -> reset backoff
                _connectBackoffMs = 500;
            }
            catch
            {
                // Requeue to front for retry
                lock (_queueGuard)
                {
                    for (int i = entries.Count - 1; i >= 0; i--)
                        _frontQueue.Push(entries[i]);
                }

                // Reset connection (forces fresh TLS next time)
                CloseConnection();

                // Backoff to avoid connect/disconnect loops every second
                _nextConnectAttemptUtc = DateTime.UtcNow.AddMilliseconds(_connectBackoffMs);
                _connectBackoffMs = Math.Min(_connectBackoffMs * 2, 10_000);

                Thread.Sleep(200); // small sleep to avoid tight loop
            }
        }

        private void SendEntries(IReadOnlyList<string> entries)
        {
            if (entries.Count == 0)
                return;

            EnsureConnectedWithBackoff();

            lock (_connectionLock)
            {
                if (_sslStream == null)
                    throw new LoggerException("Not connected to log server.");

                var chunk = new List<string>();
                int approxBytes = 0;

                for (int i = 0; i < entries.Count; i++)
                {
                    string e = entries[i]
                        .Replace((char)MessageDelimiter, ' ')
                        .Replace(BatchSeparator, ' ');

                    int entryBytes = Encoding.UTF8.GetByteCount(e) + 1;
                    if (entryBytes > SafePayloadLimitBytes)
                    {
                        e = TruncateUtf8ToBytes(e, SafePayloadLimitBytes - 16);
                        entryBytes = Encoding.UTF8.GetByteCount(e) + 1;
                    }

                    if (chunk.Count > 0 && (approxBytes + entryBytes) > SafePayloadLimitBytes)
                    {
                        SendChunk_NoLock(_sslStream, chunk);
                        chunk.Clear();
                        approxBytes = 0;
                    }

                    chunk.Add(e);
                    approxBytes += entryBytes;
                }

                if (chunk.Count > 0)
                    SendChunk_NoLock(_sslStream, chunk);
            }
        }

        private void EnsureConnectedWithBackoff()
        {
            if (DateTime.UtcNow < _nextConnectAttemptUtc)
                throw new LoggerException("Connect backoff active.");

            lock (_connectionLock)
            {
                if (_sslStream != null && _tcpClient != null && _tcpClient.Connected)
                    return;

                EnsureCryptoLoaded(); // may throw if files missing

                CloseConnection_NoLock();

                _tcpClient = new TcpClient { NoDelay = true };
                _tcpClient.Connect(_logServerHost, _logServerPort);

                // KeepAlive helps keep long-lived connections stable across NATs etc.
                try
                {
                    _tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                }
                catch { /* best effort */ }

                _sslStream = new SslStream(
                    _tcpClient.GetStream(),
                    leaveInnerStreamOpen: false,
                    userCertificateValidationCallback: ValidatePinnedServerCertificate);

                var options = new SslClientAuthenticationOptions
                {
                    TargetHost = _logServerHost,
                    EnabledSslProtocols = SslProtocols.Tls13,
                    CertificateRevocationCheckMode = X509RevocationMode.NoCheck
                };

                _sslStream.AuthenticateAsClient(options);

                _sslStream.ReadTimeout = HandshakeTimeoutMs;
                _sslStream.WriteTimeout = HandshakeTimeoutMs;

                PerformChallengeResponseHandshake(_sslStream);

                // IMPORTANT: don't set 0 here; use infinite (-1). 0 can behave like immediate timeout on some streams.
                _sslStream.ReadTimeout = Timeout.Infinite;
                _sslStream.WriteTimeout = 5000;
            }
        }

        private void EnsureCryptoLoaded()
        {
            if (_cryptoLoaded)
                return;

            lock (_cryptoLock)
            {
                if (_cryptoLoaded)
                    return;

                if (string.IsNullOrWhiteSpace(_serverCertificatePath) || !File.Exists(_serverCertificatePath))
                    throw new LoggerException($"Server certificate not found yet: {_serverCertificatePath}");

                if (string.IsNullOrWhiteSpace(_clientPrivateKeyPath) || !File.Exists(_clientPrivateKeyPath))
                    throw new LoggerException($"Client private key not found yet: {_clientPrivateKeyPath}");

                if (string.IsNullOrWhiteSpace(_clientPublicKeyPath) || !File.Exists(_clientPublicKeyPath))
                    throw new LoggerException($"Client public key not found yet: {_clientPublicKeyPath}");

                _pinnedServerCert = LoadCertificateFromPemOrDer(_serverCertificatePath);
                _clientPrivateKey = LoadRsaKeyFromPem(_clientPrivateKeyPath);
                _clientPublicKey = LoadRsaKeyFromPem(_clientPublicKeyPath);

                _cryptoLoaded = true;
            }
        }

        private bool ValidatePinnedServerCertificate(object? sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            if (certificate == null)
                return false;

            try
            {
                if (_pinnedServerCert == null)
                    return false;

                var remote = new X509Certificate2(certificate);
                return string.Equals(remote.Thumbprint, _pinnedServerCert.Thumbprint, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private void PerformChallengeResponseHandshake(SslStream sslStream)
        {
            if (_clientPrivateKey == null || _clientPublicKey == null)
                throw new LoggerException("Client keys not loaded.");

            byte[] challenge = ReadUntilDelimiter(sslStream, MessageDelimiter, MaxServerMessageSizeBytes, HandshakeTimeoutMs);
            if (challenge.Length == 0)
                throw new LoggerException("Handshake failed: empty challenge received from server.");

            byte[] signature = _clientPrivateKey.SignData(challenge, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            // sanity-check
            if (!_clientPublicKey.VerifyData(challenge, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))
                throw new LoggerException("Client keypair mismatch: public key does not verify signature from private key.");

            string signatureB64 = Convert.ToBase64String(signature);
            byte[] sigBytes = Encoding.ASCII.GetBytes(signatureB64);

            sslStream.Write(sigBytes, 0, sigBytes.Length);
            sslStream.WriteByte(MessageDelimiter);
            sslStream.Flush();
        }

        private static byte[] ReadUntilDelimiter(SslStream stream, byte delimiter, int maxBytes, int timeoutMs)
        {
            using var ms = new MemoryStream();
            var buf = new byte[1024];

            int oldTimeout = stream.ReadTimeout;
            stream.ReadTimeout = timeoutMs;

            try
            {
                while (ms.Length < maxBytes)
                {
                    int read = stream.Read(buf, 0, buf.Length);
                    if (read <= 0)
                        break;

                    int delimIndex = Array.IndexOf(buf, delimiter, 0, read);
                    if (delimIndex >= 0)
                    {
                        ms.Write(buf, 0, delimIndex);
                        return ms.ToArray();
                    }

                    ms.Write(buf, 0, read);
                }

                throw new LoggerException("Handshake failed: delimiter not found (message too large or truncated).");
            }
            finally
            {
                stream.ReadTimeout = oldTimeout;
            }
        }

        private static void SendChunk_NoLock(SslStream stream, List<string> entries)
        {
            string payload = string.Join(BatchSeparator, entries);
            byte[] data = Encoding.UTF8.GetBytes(payload);

            if (data.Length >= MaxServerMessageSizeBytes)
                throw new LoggerException("Payload exceeds server maximum message size.");

            stream.Write(data, 0, data.Length);
            stream.WriteByte(MessageDelimiter);
            stream.Flush();
        }

        private static string TruncateUtf8ToBytes(string input, int maxBytes)
        {
            if (maxBytes <= 0 || string.IsNullOrEmpty(input))
                return string.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(input);
            if (bytes.Length <= maxBytes)
                return input;

            byte[] cut = new byte[maxBytes];
            Buffer.BlockCopy(bytes, 0, cut, 0, maxBytes);
            return Encoding.UTF8.GetString(cut);
        }

        private static RSA LoadRsaKeyFromPem(string path)
        {
            string pem = File.ReadAllText(path, Encoding.UTF8);
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(pem);
            return rsa;
        }

        private static X509Certificate2 LoadCertificateFromPemOrDer(string path)
        {
            byte[] raw = File.ReadAllBytes(path);
            string asText = Encoding.ASCII.GetString(raw);

            if (asText.Contains("-----BEGIN CERTIFICATE-----", StringComparison.Ordinal))
            {
                const string begin = "-----BEGIN CERTIFICATE-----";
                const string end = "-----END CERTIFICATE-----";

                int start = asText.IndexOf(begin, StringComparison.Ordinal);
                if (start < 0) throw new LoggerException("Invalid PEM certificate: BEGIN marker not found.");
                start += begin.Length;

                int finish = asText.IndexOf(end, start, StringComparison.Ordinal);
                if (finish < 0) throw new LoggerException("Invalid PEM certificate: END marker not found.");

                string base64 = asText.Substring(start, finish - start)
                    .Replace("\r", "", StringComparison.Ordinal)
                    .Replace("\n", "", StringComparison.Ordinal)
                    .Trim();

                byte[] der = Convert.FromBase64String(base64);
                return new X509Certificate2(der);
            }

            return new X509Certificate2(raw);
        }

        private void CloseConnection()
        {
            lock (_connectionLock)
            {
                CloseConnection_NoLock();
            }
        }

        private void CloseConnection_NoLock()
        {
            try { _sslStream?.Dispose(); } catch { }
            try { _tcpClient?.Close(); } catch { }

            _sslStream = null;
            _tcpClient = null;
        }

        private void Sanitize(ref string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                text = string.Empty;

            text = text.Replace("[", "(").Replace("]", ")").Trim();
            text = text.Replace(((char)MessageDelimiter).ToString(), " ");
            text = text.Replace(BatchSeparator.ToString(), " ");
        }

        private void ValidateType(ref string type)
        {
            Sanitize(ref type);
            type = type.ToUpperInvariant();

            if (!_allowedTypes.Contains(type))
            {
                if (_noThrow)
                {
                    type = "UNDEFINED";
                    return;
                }
                throw new LoggerException($"Invalid log type: {type}. Allowed types: {string.Join(", ", _allowedTypes)}");
            }
        }

        private bool ValidateTagsAndMessage(ref string[]? tags, ref string message)
        {
            tags ??= Array.Empty<string>();
            if (tags.Length > 5)
            {
                if (_noThrow) tags = tags.Take(5).ToArray();
                else throw new LoggerException("Maximum of 5 tags allowed.");
            }

            for (int i = 0; i < tags.Length; i++)
                Sanitize(ref tags[i]);

            Sanitize(ref message);

            if (message == string.Empty)
            {
                if (_noThrow) return false;
                throw new LoggerException("Message must not be empty.");
            }

            if (message.Length > 512)
            {
                if (_noThrow) message = message.Substring(0, 512);
                else throw new LoggerException("Message length must be <= 512 characters.");
            }

            return true;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _logQueue.CompleteAdding();

            try { _worker?.Join(2000); } catch { }

            CloseConnection();

            lock (_cryptoLock)
            {
                try { _clientPrivateKey?.Dispose(); } catch { }
                try { _clientPublicKey?.Dispose(); } catch { }
                try { _pinnedServerCert?.Dispose(); } catch { }
            }

            _cts.Dispose();
            _logQueue.Dispose();
        }
    }
}
