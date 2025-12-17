using Npgsql;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Users
{
    /// <summary>
    /// Handles user-related operations using a provided Npgsql data source.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: Do NOT expose any error messages, exception messages or returned error values
    /// from this class directly to end users. These values are intended only for logging,
    /// diagnostics and internal debugging. A higher layer MUST translate them into generic,
    /// user-friendly messages without leaking internal details.
    /// </remarks>
    public class User : IDisposable
    {
        /// <summary>
        /// PostgreSQL data source used for all database operations related to this user.
        /// </summary>
        private readonly NpgsqlDataSource _dataSource;

        /// <summary>
        /// Flag used to ensure Dispose is safe to call multiple times.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Creates a pooled NpgsqlDataSource based on provided connection parameters.
        /// </summary>
        /// <param name="username">Database username (must not be empty).</param>
        /// <param name="password">Database password (must not be empty).</param>
        /// <param name="host">Database host address.</param>
        /// <param name="port">Database port (allowed range: 1024–65535).</param>
        /// <param name="dbName">Database name (must not be empty).</param>
        /// <returns>Configured and ready-to-use NpgsqlDataSource instance.</returns>
        /// <exception cref="UserException">
        /// Thrown when configuration is invalid or data source creation fails.
        /// These messages are for logs only and should not be exposed to end users.
        /// </exception>
        public User(string username, string password, string host = "127.0.0.1", int port = 5433, string dbName = "general")
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(username, nameof(username));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(password, nameof(password));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(host, nameof(host));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(dbName, nameof(dbName));

            if (port < 1024 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port), "port must be in range <1024; 65535>");
            // Build the PostgreSQL connection string with pooling enabled
            NpgsqlConnectionStringBuilder builder = new()
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = dbName,
                Pooling = true,
                MinPoolSize = 20,
                MaxPoolSize = 100,
                Timeout = 15,
                CommandTimeout = 60
            };

            try
            {
                // Attempt to create the data source using the validated builder
                NpgsqlDataSourceBuilder dataSourceBuilder = new(builder.ConnectionString);
                _dataSource = dataSourceBuilder.Build();
            }
            catch (ArgumentException ex)
            {
                throw new UserException("Invalid connection string", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new UserException("Configuration error", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new UserException("PostgreSQL error", ex);
            }
            catch (Exception ex)
            {
                throw new UserException("Unexpected error", ex);
            }
        }

        /// <summary>
        /// Performs a standard email/password authentication for this user instance,
        /// optionally issuing a persistent remember-me token on successful login.
        /// </summary>
        /// <param name="username">
        /// User identifier used for lookup (an email address).
        /// Must not be null or whitespace.
        /// </param>
        /// <param name="password">
        /// Plain-text password supplied by the client. Must not be null or whitespace.
        /// </param>
        /// <param name="rememberMe">
        /// Optional flag indicating whether a persistent remember-me token should be created.
        /// If <c>null</c>, the default is <c>true</c>. When <c>false</c>, the user is logged in
        /// without issuing a remember-me token.
        /// </param>
        /// <param name="cancellation">
        /// Cancellation token used to cancel the database operations and overall login process.
        /// </param>
        /// <returns>
        /// A tuple where:
        /// <list type="bullet">
        /// <item><description><c>result</c> is <c>true</c> when authentication succeeds; otherwise <c>false</c>.</description></item>
        /// <item><description><c>error</c> contains a descriptive error message when <c>result</c> is <c>false</c>;
        /// on success it is an empty string.</description></item>
        /// <item><description><c>rememberToken</c> contains a newly generated remember-me token when
        /// <c>result</c> is <c>true</c> and <paramref name="rememberMe"/> is enabled and successfully stored;
        /// otherwise it is <c>null</c>.</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled via the provided <paramref name="cancellation"/> token.
        /// </exception>
        /// <exception cref="UserException">
        /// Thrown when this user instance is already associated with a logged-in user
        /// or when the stored password hash has an invalid format.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while retrieving the user's credentials
        /// or updating the remember-me token.
        /// </exception>
        /// <exception cref="UserCryptographicException">
        /// Thrown when password or remember-me token hashing or verification fails.
        /// </exception>
        public async Task<(bool result, string error, string? rememberToken, long? userId)> StandardAuthAsync(
            string? username,
            string? password,
            bool? rememberMe,
            CancellationToken cancellation = default)
        {
            // Allow fast cancellation before any expensive work is done.
            cancellation.ThrowIfCancellationRequested();

            // Start a stopwatch to ensure a minimum response time (mitigates timing attacks).
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                // Re-check cancellation after the logical preconditions.
                cancellation.ThrowIfCancellationRequested();

                // Reject obviously invalid input early with a clear error message.
                if (string.IsNullOrWhiteSpace(username))
                    return (false, "Username is empty", null, null);

                if (string.IsNullOrWhiteSpace(password))
                    return (false, "Password is empty", null, null);

                // Fetch user identifier and password hash from the database for the given username.
                (bool result, long? userId, string? passwordHash) = await DatabaseQueries.GetUserPasswordAsync(username, _dataSource, cancellation);
                if (!result)
                {
                    // When no user is found, perform a full dummy password verification to match
                    // the cost of a real login and reduce timing differences between "user exists"
                    // and "user does not exist" cases.
                    Func<string?, string?, bool> verifyPasswordFunctionDummy = 
                        SharedUserSettings.ResolveVerifyPassword(SharedUserSettings.encryptionFunctionVersion);

                    Func<int, string> generateDummyHash = 
                        SharedUserSettings.ResolveGenerateDummyHash(SharedUserSettings.encryptionFunctionVersion);

                    // Use a random dummy password and a matching dummy hash with comparable cost.
                    verifyPasswordFunctionDummy(
                        RememberMeUtils.Generate(password?.Length ?? 12).token,
                        generateDummyHash(password?.Length ?? 12));

                    return (false, "Username is incorrect", null, null);
                }

                // Extract hash version from the stored hash format to resolve the correct verification function.
                string[] parts = passwordHash?.Split('$', StringSplitOptions.RemoveEmptyEntries) ?? [];
                if (parts.Length <= 1 || !int.TryParse(parts[1].Replace("v=", ""), out var hashVersion))
                    throw new UserException("Database returned incorrect hash");

                // Resolve versioned password verification function (e.g. different Argon2 parameters per version).
                Func<string?, string?, bool> verifyPasswordFunction =
                    SharedUserSettings.ResolveVerifyPassword(hashVersion);

                // Verify supplied password against the stored hash using constant-time comparison.
                bool verifyResult = verifyPasswordFunction(password, passwordHash);
                if (!verifyResult)
                    return (false, "Username is incorrect", null, null);

                // If remember-me is disabled, only bind the user id to this instance and finish.
                if (!(rememberMe ?? true))
                    return (true, "", null, userId);

                // Try to generate and persist a unique remember-me token with a bounded number of attempts.
                int tries = 0;
                while(tries < SharedUserSettings.generateRememberTokenMaxTries)
                {
                    // Generate a cryptographically strong random token and its hash for storage.
                    (string token, string tokenHash) = 
                        RememberMeUtils.Generate(SharedUserSettings.rememberMeTokenLength);

                    // Store the hashed token in the database; only the plain token is returned to the caller.
                    if (await DatabaseQueries.SetUserRememberTokenAsync((long)userId, tokenHash, _dataSource, cancellation))
                        return (true, "", token, userId);

                    // Retry with a new token if the database reports failure (e.g. collision or transient error).
                    tries++;
                }

                // If token persistence repeatedly fails, clear any existing token for safety,
                // log the user in without remember-me and signal success without exposing internal errors.
                await DatabaseQueries.SetUserRememberTokenAsync((long)userId, null, _dataSource, cancellation);
                return (true, "", null, userId);
            }
            finally
            {

                // Add a small delay to normalize total response time and make timing-based
                // user enumeration or password-guessing attacks harder.
                stopwatch.Stop();
                long elapsed = stopwatch.ElapsedMilliseconds;
                await Task.Delay((int)Math.Clamp(Math.Abs(1000 - elapsed), 0, 200), CancellationToken.None);
            }
        }

        /// <summary>
        /// Performs authentication for this user instance using a persistent remember-me token.
        /// </summary>
        /// <param name="rememberToken">
        /// Plain-text remember-me token supplied by the client (Base64-encoded string).
        /// Must not be null or whitespace and must match the expected token length
        /// defined by <see cref="SharedUserSettings.rememberMeTokenLength"/>.
        /// </param>
        /// <param name="cancellation">
        /// Cancellation token used to cancel the database operations and overall login process.
        /// </param>
        /// <returns>
        /// A tuple where:
        /// <list type="bullet">
        /// <item><description><c>result</c> is <c>true</c> when authentication succeeds; otherwise <c>false</c>.</description></item>
        /// <item><description><c>error</c> contains a descriptive error message when <c>result</c> is <c>false</c>;
        /// on success it is an empty string.</description></item>
        /// <item><description><c>rememberToken</c> contains a newly generated remember-me token when
        /// <c>result</c> is <c>true</c> and the token refresh operation succeeds; otherwise it is <c>null</c>.</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled via the provided <paramref name="cancellation"/> token.
        /// </exception>
        /// <exception cref="UserException">
        /// Thrown when this user instance is already associated with a logged-in user.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while checking or updating the remember-me token.
        /// </exception>
        /// <exception cref="UserCryptographicException">
        /// Thrown when hashing of the remember-me token fails or when the provided token has an invalid format.
        /// </exception>
        public async Task<(bool result, string error, string? rememberToken, long? userId)> AuthWithTokenAsync(
            string? rememberToken,
            CancellationToken cancellation = default)
        {
            // Allow early cancellation before any lock or heavy work is performed.
            cancellation.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(rememberToken))
                return (false, "Token is empty", null, null);
                
            if (rememberToken?.Length != SharedUserSettings.rememberMeTokenLength)
                return (false, "Token has incorrect length", null, null);

            // Hash the supplied token and validate it against the database.
            long? userId = await DatabaseQueries.CheckUserTokenAsync(RememberMeUtils.GetHash(rememberToken), _dataSource, cancellation);

            // Null indicates invalid or unknown token.
            if (userId is null)
                return (false, "Incorrect token", null, null);

            cancellation.ThrowIfCancellationRequested();

            int tries = 0;
            // Try to rotate the remember-me token to a fresh value with bounded attempts.
            while (tries < SharedUserSettings.generateRememberTokenMaxTries)
            {
                cancellation.ThrowIfCancellationRequested();

                (string token, string hash) = RememberMeUtils.Generate(SharedUserSettings.rememberMeTokenLength);
                try
                {
                    // Persist the new hashed token in the database.
                    bool result = await DatabaseQueries.SetUserRememberTokenAsync((long)userId, hash, _dataSource, cancellation);
                    if (result)
                        return (true, "", token, userId);
                }
                catch (Exception ex) when (ex is not OperationCanceledException) { }

                tries++;
            }

            // If rotation fails repeatedly, keep the login successful but without returning a new token.
            return (true, "", null, userId);
        }

        /// <summary>
        /// Registers a new user using the standard email/password flow and binds the newly
        /// created user identifier to this <see cref="User"/> instance on success.
        /// </summary>
        /// <param name="upp">
        /// Password policy that the provided plain-text password must satisfy and that is used
        /// when generating the password hash.
        /// </param>
        /// <param name="email">
        /// User email address to register. Must not be null or whitespace. The value is sanitized
        /// before being passed to the database.
        /// </param>
        /// <param name="rawPassword">
        /// Plain-text password provided by the client. Must not be null or whitespace.
        /// The password is validated and hashed according to <paramref name="upp"/>.
        /// </param>
        /// <param name="firstName">
        /// User first name. Must not be null or whitespace. The value is sanitized and truncated
        /// to a safe length before being stored.
        /// </param>
        /// <param name="lastName">
        /// User last name. Must not be null or whitespace. The value is sanitized and truncated
        /// to a safe length before being stored.
        /// </param>
        /// <param name="cancellation">
        /// Cancellation token used to cancel the registration operation before or during
        /// the database call.
        /// </param>
        /// <returns>
        /// A tuple where:
        /// <list type="bullet">
        /// <item><description><c>result</c> is <c>true</c> when the user is successfully created and assigned
        /// to this instance; otherwise <c>false</c>.</description></item>
        /// <item><description><c>error</c> contains a diagnostic error message when <c>result</c> is <c>false</c>;
        /// on success it is an empty string.</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled via the provided <paramref name="cancellation"/> token.
        /// </exception>
        /// <exception cref="UserException">
        /// Thrown when this user instance is already associated with a logged-in user or when
        /// the input arguments are invalid beyond simple validation failures returned as <c>error</c>.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while inserting the new user.
        /// </exception>
        /// <exception cref="UserCryptographicException">
        /// Thrown when password hashing fails or when the hashing configuration is invalid.
        /// </exception>
        public async Task<(bool result, string error)> StandardRegisterAsync(
            UserPasswordPolicy upp,
            string? email,
            string? rawPassword,
            string? firstName,
            string? lastName = null,
            CancellationToken cancellation = default)
        {
            // Allow fast cancellation before acquiring the semaphore or touching the database.
            cancellation.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(email))
                return (false, "email is empty");
                
            if (string.IsNullOrWhiteSpace(rawPassword))
                return (false, "password is empty");

            if (string.IsNullOrWhiteSpace(firstName))
                return (false, "first name is empty");

            if (string.IsNullOrWhiteSpace(lastName))
                return (false, "last name is empty");

            // Sanitize user data to remove unsafe HTML and trim/truncate values.
            email = SharedUserSettings.SanitizeString(email, null, false);
            firstName = SharedUserSettings.SanitizeString(firstName, 100, false);
            lastName = SharedUserSettings.SanitizeString(lastName, 100, false);

            // Resolve the password hashing function for the configured encryption version.
            Func<string?, UserPasswordPolicy, string> getHashFunction =
                SharedUserSettings.ResolveGetPasswordHash(SharedUserSettings.encryptionFunctionVersion);

            // Compute a secure password hash according to the provided policy.
            string hashedPassword = getHashFunction(rawPassword, upp);

            // Delegate the actual insertion of the new user to the database layer.
            (bool result, string message, long? userId) = await DatabaseQueries.StandardAddNewUserAsync(email, hashedPassword, firstName, lastName, _dataSource, cancellation);

            if (!result)
            {
                // Defensive check for missing message from the database.
                if (message is null)
                    return (false, "Database error, no error message returned");

                return (false, message);
            }

            // A successful registration must return a valid user identifier.
            if (userId is null)
                return (false, "Database error, no user id returned");

            return (true, "");
        }

        /// <summary>
        /// Retrieves the JSON representation of the weights row for the current user
        /// from the <c>public.weights</c> table.
        /// </summary>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A JSON string representing the weights configuration for the current user if it exists;
        /// otherwise a default empty JSON object <c>"{}"</c>.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when this <c>User</c> instance is not associated with any user identifier
        /// or when the underlying database query fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the provided cancellation token requests cancellation before or during the operation.
        /// </exception>
        public async Task<string> GetWeightsAsync(long userId, CancellationToken cancellation = default)
        {
            // Ensure the caller can cancel the operation before any database interaction begins.
            cancellation.ThrowIfCancellationRequested();


            // Ensure the caller can cancel the operation before any database interaction begins.
            cancellation.ThrowIfCancellationRequested();

            string? result;
            try
            {
                // Delegate the actual retrieval of the weights row (as JSON) to the database layer.
                result = await DatabaseQueries.GetWeightsJsonAsync(userId, _dataSource, cancellation);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserException("Failed to fetch user weights", ex);
            }

            // Normalize the absence of data to a default empty JSON object for the caller.
            return result ?? "{}";
        }

        /// <summary>
        /// Gets the current user's public data as a JSON string.
        /// </summary>
        /// <param name="cancellation">Cancellation token.</param>
        /// <returns>
        /// JSON string with the current user's data, or <c>"{}"</c> if no data exists.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when this instance is not associated with a user or when the database query fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled.
        /// </exception>
        public async Task<string> GetDataAsync(long userId, CancellationToken cancellation = default)
        {
            // Ensure the caller can cancel the operation before any database interaction begins.
            cancellation.ThrowIfCancellationRequested();

            // Ensure the caller can cancel the operation before any database interaction begins.
            cancellation.ThrowIfCancellationRequested();

            string? result;
            try
            {
                // Delegate the actual retrieval of the user data row (as JSON) to the database layer.
                result = await DatabaseQueries.GetUserJsonAsync(userId, _dataSource, cancellation);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserException("Failed to fetch user weights", ex);
            }

            // Normalize the absence of data to a default empty JSON object for the caller.
            return result ?? "{}";

        }

        /// <summary>
        /// Applies partial updates to the weights row of the current user in the <c>public.weights</c> table
        /// based on a dictionary of column names mapped to new values.
        /// </summary>
        /// <param name="fieldNames">
        /// Dictionary mapping column names from the <c>public.weights</c> table to values that should be written
        /// for the current user (for example <c>"vector"</c>, <c>"mean_dist"</c>, <c>"means_weight_sum"</c>, etc.).
        /// Keys must be non-empty and values must be non-null; invalid entries are ignored.
        /// </param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A dictionary that maps each processed column name to a boolean indicating whether the update
        /// for that specific column was reported as successful by the database.
        /// Columns that are unknown, have mismatched value types, or encounter an error are reported as <c>false</c>.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when the <paramref name="fieldNames"/> dictionary is null or when this <c>User</c> instance
        /// has no associated user identifier.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the provided cancellation token requests cancellation before or during the operation.
        /// </exception>
        public async Task<Dictionary<string, bool>> UpdateWeightsAsync(long userId, Dictionary<string, object?>? fieldNames, CancellationToken cancellation = default)
        {
            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            if (fieldNames is null)
                throw new UserException("`fieldNames` is empty");

            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            // Clean up the input:
            // - drop entries with empty keys or null values,
            // - keep only the first occurrence for duplicate keys to avoid conflicting updates.
            Dictionary<string, object> cleanedFieldNames = fieldNames
                .Where(entry => entry.Value is not null)
                .GroupBy(entry => entry.Key)
                .Select(group => group.Last())
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            // Track per-column success so the caller knows which individual updates actually went through.
            ConcurrentDictionary<string, bool> fieldSuccesses = [];
            foreach (KeyValuePair<string, object> field in cleanedFieldNames)
            {
                // Re-check cancellation between individual column updates to keep long batches responsive.
                cancellation.ThrowIfCancellationRequested();
                (string fieldName, object fieldValues) = field;

                bool? result = null;
                try
                {
                    switch (fieldName)
                    {
                        case "order_by_option":
                            // order_by_option TEXT[]
                            if (fieldValues is not string[] orderByOptionValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsOrderByOptionAsync(
                                userId,
                                orderByOptionValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "mean_value_ids":
                            // mean_value_ids TEXT[]
                            if (fieldValues is not string[] meanValueIds)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeanValueIdsAsync(
                                userId,
                                meanValueIds,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "vector":
                            // vector REAL[]
                            if (fieldValues is not float[] vectorValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsVectorAsync(
                                userId,
                                vectorValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "mean_dist":
                            // mean_dist REAL[]
                            if (fieldValues is not float[] meanDistValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeanDistAsync(
                                userId,
                                meanDistValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_value_sum":
                            // means_value_sum REAL[]
                            if (fieldValues is not float[] meansValueSumValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansValueSumAsync(
                                userId,
                                meansValueSumValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_value_ssum":
                            // means_value_ssum DOUBLE PRECISION[]
                            if (fieldValues is not double[] meansValueSsumValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansValueSsumAsync(
                                userId,
                                meansValueSsumValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_value_count":
                            // means_value_count INTEGER[]
                            if (fieldValues is not int[] meansValueCountValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansValueCountAsync(
                                userId,
                                meansValueCountValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_weight_sum":
                            // means_weight_sum REAL[]
                            if (fieldValues is not float[] meansWeightSumValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansWeightSumAsync(
                                userId,
                                meansWeightSumValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_weight_ssum":
                            // means_weight_ssum DOUBLE PRECISION[]
                            if (fieldValues is not double[] meansWeightSsumValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansWeightSsumAsync(
                                userId,
                                meansWeightSsumValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "means_weight_count":
                            // means_weight_count INTEGER[]
                            if (fieldValues is not int[] meansWeightCountValues)
                            {
                                fieldSuccesses.TryAdd(fieldName, false);
                                break;
                            }

                            result = await DatabaseQueries.SetWeightsMeansWeightCountAsync(
                                userId,
                                meansWeightCountValues,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        default:
                            // Unknown column name is treated as a failed update to keep the contract explicit.
                            fieldSuccesses.TryAdd(fieldName, false);
                            break;
                    }
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    // Any exception tied to a specific field update is isolated to that field
                    // so that other independent column updates can still proceed.
                    fieldSuccesses.TryAdd(fieldName, false);
                }
            }

            return fieldSuccesses.ToDictionary();
        }

        /// <summary>
        /// Updates selected fields for the current user in the database and returns per-field success flags.
        /// </summary>
        /// <param name="fieldNames">
        /// Mapping of field/column names to new string values to be applied for the current user; null is not allowed.
        /// </param>
        /// <param name="cancellation">
        /// Token used to cancel the operation before it starts or between individual field updates.
        /// </param>
        /// <returns>
        /// A dictionary mapping each processed field name to a boolean indicating whether its update succeeded.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when the input field dictionary is null or when this object has no user assigned.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the update operation is cancelled before processing or between individual field updates.
        /// </exception>
        public async Task<Dictionary<string, bool>> UpdateDataAsync(long userId, Dictionary<string, string?>? fieldNames, CancellationToken cancellation = default)
        {
            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            if (fieldNames is null)
                throw new UserException("`fieldNames` is empty");

            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            // Clean up the input:
            // - drop entries with empty keys or null values,
            // - keep only the first occurrence for duplicate keys to avoid conflicting updates.
            Dictionary<string, string> cleanedFieldNames = fieldNames
                .Where(entry => string.IsNullOrWhiteSpace(entry.Value))
                .GroupBy(entry => entry.Key)
                .Select(group => group.First())
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            // Track per-column success so the caller knows which individual updates actually went through.
            ConcurrentDictionary<string, bool> fieldSuccesses = [];
            foreach (KeyValuePair<string, string> field in cleanedFieldNames)
            {
                // Re-check cancellation between individual column updates to keep long batches responsive.
                cancellation.ThrowIfCancellationRequested();
                (string fieldName, string fieldValue) = field;

                bool? result = null;
                try
                {
                    switch (fieldName)
                    {
                        case "user_first_name":
                            result = await DatabaseQueries.SetUserFirstNameAsync(
                                userId,
                                fieldValue,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "user_second_name":
                            result = await DatabaseQueries.SetUserSecondNameAsync(
                                userId,
                                fieldValue,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "user_last_name":
                            result = await DatabaseQueries.SetUserLastNameAsync(
                                userId,
                                fieldValue,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        case "user_phone":
                            result = await DatabaseQueries.SetUserPhoneAsync(
                                userId,
                                fieldValue,
                                _dataSource,
                                cancellation);

                            fieldSuccesses.TryAdd(fieldName, result ?? false);
                            break;

                        default:
                            // Unknown column name is treated as a failed update to keep the contract explicit.
                            fieldSuccesses.TryAdd(fieldName, false);
                            break;
                    }
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    // Any exception tied to a specific field update is isolated to that field
                    // so that other independent column updates can still proceed.
                    fieldSuccesses.TryAdd(fieldName, false);
                }
            }

            return fieldSuccesses.ToDictionary();
        }

        /// <summary>
        /// Changes the current user's password according to the provided password policy and updates the remember token.
        /// </summary>
        /// <param name="upp">
        /// Password policy used to validate the new password before hashing.
        /// </param>
        /// <param name="newPassword">
        /// New plain-text password to set for the current user; must not be null.
        /// </param>
        /// <param name="cancellation">
        /// Token used to cancel the operation before it starts or while updating the password.
        /// </param>
        /// <returns>
        /// True if the password was successfully updated and the remember token was cleared; otherwise false.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when the new password is null or when this object has no user assigned.
        /// </exception>
        /// <exception cref="UserCryptographicException">
        /// Thrown when the password does not satisfy the policy or when password hashing fails.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while updating the password or remember token.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled before hashing the password or during any database update.
        /// </exception>
        public async Task<bool> ChangePasswordAsync(long userId, UserPasswordPolicy upp, string? newPassword, CancellationToken cancellation = default)
        {
            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            if (newPassword is null)
                throw new UserException("`newPassword` is empty");


            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            // Resolve the password hashing function for the configured encryption version.
            Func<string?, UserPasswordPolicy, string> getHashFunction =
                SharedUserSettings.ResolveGetPasswordHash(SharedUserSettings.encryptionFunctionVersion);

            // Compute a secure password hash according to the provided policy.
            string hashedPassword = getHashFunction(newPassword, upp);

            cancellation.ThrowIfCancellationRequested();

            bool result = await DatabaseQueries.SetUserPasswordAsync(userId, hashedPassword, _dataSource, cancellation);
            if (!result)
                return false;

            await DatabaseQueries.SetUserRememberTokenAsync(userId, null, _dataSource, cancellation);

            return true;
        }

        /// <summary>
        /// Logs out the current user from this instance by clearing their persistent
        /// remember-me token in the database and detaching the user from this object.
        /// After logout completes, the instance is disposed and should not be used again.
        /// </summary>
        /// <param name="cancellation">
        /// Cancellation token used to cancel the logout operation before or during
        /// the database call.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous logout operation.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled via the provided <paramref name="cancellation"/> token
        /// while waiting for the semaphore or during the database call.
        /// </exception>
        public async Task LogoutAsync(long userId, CancellationToken cancellation = default)
        {
            // Respect cancellation before acquiring the semaphore or touching the database.
            cancellation.ThrowIfCancellationRequested();

            // Best-effort attempt to clear the remember-me token for the current user in the database.
            await DatabaseQueries.SetUserRememberTokenAsync(userId, null, _dataSource, cancellation);
        }

        /// <summary>
        /// Checks whether the currently selected user has the specified permission.
        /// </summary>
        /// <param name="permission">
        /// Name of the permission to verify for the current user. If the value is
        /// <c>null</c>, empty, or consists only of whitespace, the method returns <c>false</c>.
        /// </param>
        /// <param name="cancellation">
        /// Token used to cancel the permission check before it starts, while waiting
        /// for synchronization, or during the database call.
        /// </param>
        /// <returns>
        /// <c>true</c> if a user is assigned to this object and the database reports
        /// that the user has the specified permission; otherwise <c>false</c>. In case of
        /// any database error during the check, the method returns <c>false</c>.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when this object has no user assigned and a permission check is requested.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the permission check is cancelled before or during the operation.
        /// </exception>
        public async Task<bool> CheckPermissionAsync(long userId, string? permission, CancellationToken cancellation)
        {
            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(permission))
                return false;

            cancellation.ThrowIfCancellationRequested();

            bool result;
            try
            {
                result = await DatabaseQueries.CheckPermission(userId, permission, _dataSource, cancellation);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Deletes the current user from the database.
        /// </summary>
        /// <param name="cancellation">
        /// Token used to cancel the delete operation before it starts or while executing the database command.
        /// </param>
        /// <returns>
        /// True if the user was successfully deleted; otherwise false.
        /// </returns>
        /// <exception cref="UserException">
        /// Thrown when this object has no user assigned to delete.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while deleting the user.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the delete operation is cancelled before or during the database call.
        /// </exception>
        public async Task<bool> DeleteUserAsync(long userId, CancellationToken cancellation = default)
        {
            // Allow the caller to interrupt the whole update sequence before any processing starts.
            cancellation.ThrowIfCancellationRequested();

            return await DatabaseQueries.DeleteUserAsync(userId, _dataSource, cancellation);
        }

        /// <summary>
        /// Releases resources held by this user instance.
        /// </summary>
        /// <remarks>
        /// This method is safe to call multiple times; subsequent calls have no effect.
        /// It does not perform logout logic; use <see cref="LogoutAsync"/> to log out the user
        /// and clear the remember-me token before disposing the instance.
        /// </remarks>
        public void Dispose()
        {
            // Ensure disposal logic is executed only once.
            if (_disposed)
                return;

            _disposed = true;

            // No finalizer, but follow the standard pattern for completeness.
            GC.SuppressFinalize(this);
        }
    }
}
