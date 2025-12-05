using Npgsql;
using NpgsqlTypes;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

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
    public class User(NpgsqlDataSource datasource) : IDisposable
    {
        /// <summary>
        /// PostgreSQL data source used for all database operations related to this user.
        /// </summary>
        private readonly NpgsqlDataSource _dataSource = datasource ?? throw new UserException("`datasource` is empty");

        /// <summary>
        /// Identifier of the currently logged-in user for this instance, or <c>null</c> if no user is assigned.
        /// </summary>
        private long? _userId = null;

        /// <summary>
        /// Synchronization primitive protecting user selection and login operations for this instance.
        /// </summary>
        private readonly SemaphoreSlim _selectUserSemaphore = new(1, 1);

        /// <summary>
        /// Flag used to ensure Dispose is safe to call multiple times.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Indicates whether this user instance is currently associated with a logged-in user.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a user identifier is assigned; otherwise <c>false</c>.
        /// </returns>
        public bool IsLoggedIn()
        {
            return _userId is not null;
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
        public async Task<(bool result, string error, string? rememberToken)> StandardAuthAsync(string? username, string? password, bool? rememberMe, CancellationToken cancellation = default)
        {
            // Allow fast cancellation before any expensive work is done.
            cancellation.ThrowIfCancellationRequested();

            // Serialize login attempts on this user instance to avoid race conditions on _userId.
            await _selectUserSemaphore.WaitAsync(cancellation);

            // Start a stopwatch to ensure a minimum response time (mitigates timing attacks).
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                // Prevent reusing the same User instance for multiple logins.
                if (IsLoggedIn())
                    throw new UserException("This object already have an assigned user");

                // Re-check cancellation after the logical preconditions.
                cancellation.ThrowIfCancellationRequested();

                // Reject obviously invalid input early with a clear error message.
                if (string.IsNullOrWhiteSpace(username))
                    return (false, "Username is empty", null);

                if (string.IsNullOrWhiteSpace(password))
                    return (false, "Password is empty", null);

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

                    return (false, "Incorrect username", null);
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
                    return (false, "Incorrect username", null);

                // If remember-me is disabled, only bind the user id to this instance and finish.
                if (!(rememberMe ?? true))
                {
                    _userId = userId;
                    return (true, "", null);
                }

                // Try to generate and persist a unique remember-me token with a bounded number of attempts.
                int tries = 0;
                while(tries < SharedUserSettings._generateRememberTokenMaxTries)
                {
                    // Generate a cryptographically strong random token and its hash for storage.
                    (string token, string tokenHash) = 
                        RememberMeUtils.Generate(SharedUserSettings._rememberMeTokenLength);

                    // Store the hashed token in the database; only the plain token is returned to the caller.
                    if (await DatabaseQueries.SetUserRememberTokenAsync((long)userId, tokenHash, _dataSource, cancellation))
                    {
                        // On success, bind the user id to this instance and return the token to the caller.
                        _userId = userId;
                        return (true, "", token);
                    }

                    // Retry with a new token if the database reports failure (e.g. collision or transient error).
                    tries++;
                }

                // If token persistence repeatedly fails, clear any existing token for safety,
                // log the user in without remember-me and signal success without exposing internal errors.
                await DatabaseQueries.SetUserRememberTokenAsync((long)userId, null, _dataSource, cancellation);
                _userId = userId;
                return (true, "", null);
            }
            finally
            {
                // Always release the semaphore, regardless of success or failure.
                _selectUserSemaphore.Release();

                // Add a small delay to normalize total response time and make timing-based
                // user enumeration or password-guessing attacks harder.
                stopwatch.Stop();
                long elapsed = stopwatch.ElapsedMilliseconds;
                await Task.Delay((int)Math.Clamp(Math.Abs(1000 - elapsed), 0, 200), CancellationToken.None);
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use NewMethod instead.")]
        public async Task<(bool result, string error, string? rememberToken)> AuthWithTokenAsync(string? rememberToken, CancellationToken cancellation = default)
        {
            return (false, "obsolete",  null);
            /*
            if (cancellation.IsCancellationRequested)
                return (false, "Operation was cancelled", rememberToken);
            await _selectUserSemaphore.WaitAsync(cancellation);
            try
            {
                if (IsLoggedIn())
                    throw new UserException("This object already have an assigned user");

                if (string.IsNullOrWhiteSpace(rememberToken))
                    return (false, "Token is empty", null);

                if (rememberToken?.Length != _rememberMeTokenLength)
                    return (false, "Token is incorrect", null);

                await using NpgsqlCommand command = _dataSource.CreateCommand("SELECT check_user_token(@token);");
                command.Parameters.AddWithValue("token", rememberToken);
                long? userId = null;
                try
                {
                    object? result = await command.ExecuteScalarAsync(cancellation);
                    if (result is null || result is DBNull)
                        return (false, "Invalid remember token.", null);

                    userId = (long)result;

                }
                catch (OperationCanceledException)
                {
                    return (false, "Operation was cancelled.", null);
                }
                catch (Exception ex)
                {
                    throw new UserException("Database error", ex);
                }

                if (userId is null)
                    return (false, "Invalid remember token.", null);

                int tries = 0;
                while (tries < _generateRememberTokenMaxTries)
                {
                    if (cancellation.IsCancellationRequested)
                        return (false, "Operation was cancelled.", null);

                    string? newRememberToken = null;
                    try
                    {
                        newRememberToken = SharedUserSettings.GenerateRememberToken(_rememberMeTokenLength);
                        if (newRememberToken is null)
                            return (false, "Failed to generate new remember me token", null);
                    }
                    catch
                    {
                        return (false, "Failed to generate new remember me token", null);
                    }
                    command.Parameters.Clear();
                    command.CommandText = "SELECT public.set_user_remember_token(@userid, @token)";
                    command.Parameters.AddWithValue("userid", userId);
                    command.Parameters.AddWithValue("token", newRememberToken);

                    object? dbResult = null;
                    try
                    {
                        dbResult = await command.ExecuteScalarAsync(cancellation);
                    }
                    catch (OperationCanceledException)
                    {
                        return (false, "Operation was cancelled.", null);
                    }
                    catch (Exception ex)
                    {
                        throw new UserException("Database error", ex);
                    }
                    if (dbResult is null || dbResult is DBNull)
                        throw new UserException("Database returned empty result");

                    if (dbResult is not bool setRememberToken)
                        throw new UserException("Database returned empty result");

                    if (setRememberToken)
                    {
                        _userId = userId;
                        return (true, "", newRememberToken);
                    }

                    tries++;
                }

                return (false, "Failed to add new token", null);
            }
            finally
            {
                _selectUserSemaphore.Release();
            }
            */
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use NewMethod instead.")]
        public async Task<(bool result, string error)> StandardRegisterAsync(UserPasswordPolicy upp, string? email, string? rawPassword, string? firstName, string? secondName, string? lastName = null, string? phone = null, CancellationToken cancellation = default)
        {
            return (false, "Obsolete");
            /*
            if (cancellation.IsCancellationRequested)
                return (false, "Operation was cancelled");

            await _selectUserSemaphore.WaitAsync(cancellation);
            try
            {
                if (IsLoggedIn())
                    throw new UserException("This object already have an assigned user");

                if (string.IsNullOrWhiteSpace(email)) throw new UserException("email is empty");
                if (string.IsNullOrWhiteSpace(rawPassword)) throw new UserException("password is empty");
                if (string.IsNullOrWhiteSpace(firstName)) throw new UserException("first name is empty");
                if (string.IsNullOrWhiteSpace(lastName)) throw new UserException("last name is empty");

                email = SharedUserSettings.SanitizeString(email, null, false);
                firstName = SharedUserSettings.SanitizeString(firstName, 100, false);
                secondName = SharedUserSettings.SanitizeString(secondName, 100, true);
                lastName = SharedUserSettings.SanitizeString(lastName, 100, false);
                phone = SharedUserSettings.SanitizeString(phone, 16, true);

                Type encryptionFunctionsType = SharedUserSettings.GetEncryptionFunctions(SharedUserSettings.encryptionFunctionVersion);
                MethodInfo? getPasswordHashMethod = null;
                try
                {
                    // Locate static public getPasswordHash(string? password, UserPasswordPolicy passwordPolicy) method.
                    getPasswordHashMethod = encryptionFunctionsType.GetMethod(
                        "GetPasswordHash",
                        BindingFlags.Public | BindingFlags.Static,
                        binder: null,
                        types: [typeof(string), typeof(UserPasswordPolicy)],
                        modifiers: null);

                    // Ensure method exists and has correct return type.
                    if (getPasswordHashMethod is null || getPasswordHashMethod.ReturnType != typeof(string))
                        throw new UserException($"Incorrect `GetPasswordHash` signature in EncryptionFunctionsV{SharedUserSettings.encryptionFunctionVersion}");
                }
                catch (Exception ex) when (ex is not UserException)
                {
                    throw new UserException($"Incorrect `GetPasswordHash` signature in EncryptionFunctionsV{SharedUserSettings.encryptionFunctionVersion}");
                }

                string? passwordHash = null;
                try
                {
                    if (cancellation.IsCancellationRequested)
                        return (false, "Operation was cancelled.");

                    // Invoke the password verification method using reflection.
                    passwordHash = (string?)getPasswordHashMethod.Invoke(null, [rawPassword, upp]);
                }
                catch (TargetInvocationException ex)
                {
                    Exception? inner = ex.InnerException ?? throw new UserException($"Unknown exception while invoking `GetPasswordHash`");
                    if (inner is UserCryptographicException)
                        throw inner;

                    throw new UserException($"Unknown exception while invoking `GetPasswordHash`");
                }

                if (passwordHash is null)
                    throw new UserException($"Unknown exception while invoking `GetPasswordHash`");

                if (cancellation.IsCancellationRequested)
                    return (false, "Operation was cancelled");

                await using NpgsqlCommand command = _dataSource.CreateCommand("CALL public.standard_add_user(@email, @password, @first_name, @last_name, @second_name, @phone)");
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("password", passwordHash);
                command.Parameters.AddWithValue("first_name", firstName);
                command.Parameters.AddWithValue("last_name", lastName);
                command.Parameters.AddWithValue("second_name", (object?)secondName ?? DBNull.Value);
                command.Parameters.AddWithValue("phone", (object?)phone ?? DBNull.Value);
                
                bool? success = null;
                string? errorMessage = null;
                long? userId = null;
                try
                {
                    await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellation);
                    if (await reader.ReadAsync(cancellation))
                    {
                        success = reader.GetBoolean(reader.GetOrdinal("o_success"));
                        errorMessage = reader.GetString(reader.GetOrdinal("o_message"));

                        int userIdOrdinal = reader.GetOrdinal("o_user_id");
                        userId = reader.IsDBNull(userIdOrdinal)
                            ? null
                            : reader.GetInt64(userIdOrdinal);
                    }
                    else
                        throw new UserException("No result returned from `standard_add_user`");
                }
                catch (OperationCanceledException)
                {
                    return (false, "Operation was cancelled.");
                }
                catch (Exception ex) when (ex is not UserException)
                {
                    throw new UserException("Database error", ex);
                }

                if (success is null || errorMessage is null)
                    throw new UserException("Incorrect result returned from `standard_add_user`");

                if (success == false)
                    return (false, errorMessage);

                if (userId is null)
                    throw new UserException("User ID was not returned for a successful registration");

                if (cancellation.IsCancellationRequested)
                    return (false, "Operation was cancelled");

                _userId = userId.Value;
                return (true, "");
            }
            finally
            {
                _selectUserSemaphore.Release();
            }*/
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
        public async Task LogoutAsync(CancellationToken cancellation = default)
        {
            // If there is no associated user, there is nothing to log out.
            if (_userId is null)
                return;

            // Respect cancellation before acquiring the semaphore or touching the database.
            cancellation.ThrowIfCancellationRequested();

            // Serialize logout with other operations that manipulate _userId and user-related state.
            await _selectUserSemaphore.WaitAsync(cancellation);
            try
            {
                // Best-effort attempt to clear the remember-me token for the current user in the database.
                await DatabaseQueries.SetUserRememberTokenAsync((long)_userId, null, _dataSource, cancellation);
            }
            catch (Exception ex) when (ex is not OperationCanceledException) { }
            finally
            {
                // Regardless of database outcome, detach the user from this instance
                // and release the semaphore to avoid deadlocks.
                _userId = null;
                _selectUserSemaphore.Release();

                // After logout the instance is no longer meant to be reused; dispose it.
                Dispose();
            }
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

            // Dispose managed resources owned by this class.
            _selectUserSemaphore.Dispose();
            _userId = null;

            // No finalizer, but follow the standard pattern for completeness.
            GC.SuppressFinalize(this);
        }
    }
}
