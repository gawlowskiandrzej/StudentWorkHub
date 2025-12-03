using Npgsql;
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
    public class User(NpgsqlDataSource datasource)
    {
        /// <summary>
        /// Shared PostgreSQL data source instance used for all user operations.
        /// </summary>
        private readonly NpgsqlDataSource _dataSource = datasource ?? throw new UserException("`datasource` is empty");

        /// <summary>
        /// Performs standard username/password authentication.
        /// </summary>
        /// <param name="username">User identifier provided by the caller.</param>
        /// <param name="password">Plain-text password provided by the caller.</param>
        /// <returns>
        /// Tuple (result, error):
        /// - result: true if authentication succeeded, false otherwise.
        /// - error: technical error/reason string for logs and debugging only.
        /// </returns>
        /// <exception cref="UserException"> (for internal errors, DB issues, unexpected states).</exception>
        /// <exception cref="UserCryptographicException"> (bubbled from VerifyPassword).</exception>
        public async Task<(bool result, string error)> StandardAuthAsync(string? username, string? password)
        {
            // Start a stopwatch to ensure a minimum response time (mitigates timing attacks).
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return (false, "Username is empty");

                if (string.IsNullOrWhiteSpace(password))
                    return (false, "Password is empty");

                await using NpgsqlCommand command = _dataSource.CreateCommand("SELECT get_user_password(@username);");
                command.Parameters.AddWithValue("username", username);
                object? dbResult = null;
                try
                {
                    dbResult = await command.ExecuteScalarAsync();
                }
                catch (Exception ex)
                {
                    throw new UserException("Database error", ex);
                }
                if (dbResult is null || dbResult is DBNull)
                    return (false, "Invalid username");

                if (dbResult is not string passwordHash || string.IsNullOrWhiteSpace(passwordHash))
                    throw new UserException("Database returned empty hash");

                string[] parts = passwordHash.Split('$', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length <= 1 || !int.TryParse(parts[1].Replace("v=", ""), out var hashVersion))
                    throw new UserException("Database returned incorrect hash");

                // Resolve the appropriate encryption/verification functions based on hash version.
                Type encryptionFunctionsType = SharedUserSettings.GetEncryptionFunctions(hashVersion);
                MethodInfo? verifyPasswordMethod = null;
                try
                {
                    // Locate static public VerifyPassword(string password, string storedHash) method.
                    verifyPasswordMethod = encryptionFunctionsType.GetMethod(
                        "VerifyPassword",
                        BindingFlags.Public | BindingFlags.Static,
                        binder: null,
                        types: [typeof(string), typeof(string)],
                        modifiers: null);

                    // Ensure method exists and has correct return type.
                    if (verifyPasswordMethod is null || verifyPasswordMethod.ReturnType != typeof(bool))
                        throw new UserException($"Incorrect `VerifyPassword` signature in EncryptionFunctionsV{hashVersion}");
                }
                catch (Exception ex) when (ex is not UserException)
                {
                    throw new UserException($"Incorrect `VerifyPassword` signature in EncryptionFunctionsV{hashVersion}");
                }

                try
                {
                    // Invoke the password verification method using reflection.
                    bool? result = (bool?)verifyPasswordMethod.Invoke(null, [password, passwordHash]);
                    if (result is not null)
                    {
                        if (result == false)
                            return (false, "Invalid password");

                        return (true, "");
                    }

                    throw new UserException($"Unknown exception while invoking `VerifyPassword`");
                }
                catch (TargetInvocationException ex)
                {
                    Exception? inner = ex.InnerException ?? throw new UserException($"Unknown exception while invoking `VerifyPassword`");
                    if (inner is UserCryptographicException)
                        throw inner;

                    throw new UserException($"Unknown exception while invoking `VerifyPassword`");
                }
            }
            catch (Exception)
            {
                throw new UserException($"Unknown exception while invoking `VerifyPassword`");
            }
            finally
            {
                // Enforce a minimum total response time of ~1000 ms (+/- 200 ms clamp)
                // in order to reduce timing side-channel information.
                stopwatch.Stop();
                long elapsed = stopwatch.ElapsedMilliseconds;
                await Task.Delay((int)Math.Clamp(Math.Abs(1000 - elapsed), 0, 200));
            }
        }
    }
}
