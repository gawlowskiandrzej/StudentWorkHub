using Npgsql;
using System.Data;

namespace Users
{
    /// <summary>
    /// Represents errors that occur during execution of user-related database queries.
    /// </summary>
    internal class UserDbQueryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbQueryException"/> class.
        /// </summary>
        public UserDbQueryException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbQueryException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserDbQueryException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbQueryException"/> class with a specified 
        /// error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public UserDbQueryException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Provides low-level database access methods for user-related operations.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: Do NOT expose any error messages, exception messages or returned error values
    /// from this class directly to end users. These values are intended only for logging,
    /// diagnostics and internal debugging. A higher layer MUST translate them into generic,
    /// user-friendly messages without leaking internal details.
    /// </remarks>
    internal static class DatabaseQueries
    {
        /// <summary>
        /// Converts a database query result to a boolean value, treating null or DBNull as false.
        /// </summary>
        /// <param name="result">The value returned from the database query.</param>
        /// <returns>
        /// A boolean representation of the provided value; returns false if the input is null or DBNull.
        /// </returns>
        private static bool ConvertDbResultToBoolean(object? result)
        {
            // Treat NULL as a failure (false), since the function is expected to return a boolean.
            if (result is null || result is DBNull)
                return false;

            if (result is bool b)
                return b;

            // Fallback, should not normally happen.
            return Convert.ToBoolean(result, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Adds a new user using the standard registration stored procedure.
        /// </summary>
        /// <param name="email">User email to register.</param>
        /// <param name="passwordHash">Hashed password to store for the user.</param>
        /// <param name="firstName">User first name.</param>
        /// <param name="lastName">User last name.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A tuple where <c>result</c> indicates success, <c>message</c> contains the database-provided
        /// diagnostic message, and <c>userId</c> is the newly created user identifier on success;
        /// <c>userId</c> is <c>null</c> if the operation fails.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<(bool result, string message, long? userId)> StandardAddNewUserAsync(
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Fail fast if a cancellation was already requested before starting any I/O.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.standard_add_user(@p_email, @p_password, @p_first_name, @p_last_name, NULL, NULL, NULL)");

            command.Parameters.AddWithValue("p_email", email);
            command.Parameters.AddWithValue("p_password", passwordHash);
            command.Parameters.AddWithValue("p_first_name", firstName);
            command.Parameters.AddWithValue("p_last_name", lastName);

            try
            {
                // SingleRow hints to the provider that at most one row is expected, allowing optimizations.
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Enforce contract that the stored procedure must always return a row with status information.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `standard_add_user`");

                int successOrdinal = reader.GetOrdinal("o_success");
                int messageOrdinal = reader.GetOrdinal("o_message");
                int userIdOrdinal = reader.GetOrdinal("o_user_id");

                // Treat missing or NULL success flag as a failure for defensive handling.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                // Use database-provided message when available, otherwise fall back to a generic message.
                string message = reader.IsDBNull(messageOrdinal)
                    ? "Unknown error"
                    : reader.GetString(messageOrdinal);

                // User ID is only considered valid when the procedure explicitly returns a non-NULL value.
                long? userId = reader.IsDBNull(userIdOrdinal)
                    ? null
                    : reader.GetInt64(userIdOrdinal);

                // Avoid propagating a potentially inconsistent identifier when the operation failed.
                if (!success)
                    userId = null;

                return (success, message, userId);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Sets or clears the persistent "remember me" token for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose token should be updated.</param>
        /// <param name="token">Token value to store, or <c>null</c> to clear the token.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserRememberTokenAsync(
            long userId,
            string? token,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Ensure callers can cancel before establishing the database command.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_remember_token(@p_user_id, @p_token, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);

            // Convert null into DBNull to correctly represent SQL NULL for optional token.
            command.Parameters.AddWithValue("p_token", (object?)token ?? DBNull.Value);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Stored procedure is expected to always return a status row.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_remember_token`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Missing or NULL success flag is treated as failure.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the stored password hash for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose password is being changed.</param>
        /// <param name="passwordHash">New password hash to store for the user.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserPasswordAsync(
            long userId,
            string passwordHash,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Cancellation is checked before any network round-trip is started.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_password(@p_user_id, @p_password, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_password", passwordHash);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Enforce the expectation that the procedure always returns a status row.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_password`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Interpret any NULL success value as failure to avoid false positives.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the phone number for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose phone number is being updated.</param>
        /// <param name="phone">New phone number, or <c>null</c> to clear the stored value.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserPhoneAsync(
            long userId,
            string? phone,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Guard against doing work after cancellation has been requested.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_phone(@p_user_id, @p_phone, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);

            // Map optional phone number to SQL NULL when not provided.
            command.Parameters.AddWithValue("p_phone", (object?)phone ?? DBNull.Value);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Database contract requires a single-row status result.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_phone`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Use conservative semantics: absence of a success flag implies failure.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the first name for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose first name is being updated.</param>
        /// <param name="firstName">New first name to store.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserFirstNameAsync(
            long userId,
            string firstName,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Check for cancellation before allocating database resources.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_first_name(@p_user_id, @p_first_name, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_first_name", firstName);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // A missing status row indicates a contract violation in the stored procedure.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_first_name`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Any non-true or NULL value is treated as failure.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the second name (middle name) for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose second name is being updated.</param>
        /// <param name="secondName">New second name to store.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserSecondNameAsync(
            long userId,
            string secondName,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Early cancellation check prevents unnecessary network activity.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_second_name(@p_user_id, @p_second_name, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_second_name", secondName);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Fail explicitly if the expected status row is missing.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_second_name`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Use defensive interpretation of the success flag.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the last name for the specified user.
        /// </summary>
        /// <param name="userId">Identifier of the user whose last name is being updated.</param>
        /// <param name="lastName">New last name to store.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> SetUserLastNameAsync(
            long userId,
            string lastName,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Propagate cancellation as early as possible to the caller.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.set_user_last_name(@p_user_id, @p_last_name, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_last_name", lastName);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Explicitly signal that the stored procedure did not respect the contract.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `set_user_last_name`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Null-check ensures we do not interpret undefined values as success.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Deletes the specified user from the database.
        /// </summary>
        /// <param name="userId">Identifier of the user to delete.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the stored procedure reports success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when the database does not return a valid result or an unexpected error occurs.</exception>
        internal static async Task<bool> DeleteUserAsync(
            long userId,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Abort early if the caller has already requested cancellation.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.delete_user(@p_user_id, NULL)");

            command.Parameters.AddWithValue("p_user_id", userId);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Missing status row indicates a protocol error in the stored procedure.
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `delete_user`");

                int successOrdinal = reader.GetOrdinal("p_success");

                // Any non-true or NULL value indicates that the deletion did not succeed.
                bool success = !reader.IsDBNull(successOrdinal) &&
                               reader.GetBoolean(successOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Retrieves the user identifier and stored password hash for the specified email.
        /// </summary>
        /// <param name="email">Email address used to look up the user.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A tuple where <c>result</c> indicates success, <c>userId</c> is the retrieved user identifier,
        /// and <c>passwordHash</c> is the stored hash value; if either <c>userId</c> or <c>passwordHash</c>
        /// is not present, the method returns <c>(false, null, null)</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<(bool result, long? userId, string? passwordHash)> GetUserPasswordAsync(
            string email,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Allow caller to cancel before any query is sent to the database.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "SELECT user_id, password FROM public.get_user_password(@p_email);");

            command.Parameters.AddWithValue("p_email", email);

            try
            {
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Absence of any row means the user could not be found for the given email.
                if (!await reader.ReadAsync(cancellation))
                    return (false, null, null);

                int userIdOrdinal = reader.GetOrdinal("user_id");
                int passwordOrdinal = reader.GetOrdinal("password");

                // Treat database NULLs as missing values and propagate them as nullable references.
                long? userId = reader.IsDBNull(userIdOrdinal)
                    ? null
                    : reader.GetInt64(userIdOrdinal);

                string? passwordHash = reader.IsDBNull(passwordOrdinal)
                    ? null
                    : reader.GetString(passwordOrdinal);

                // Ensure both the identity and credential are present before signalling success.
                if (userId is null || passwordHash is null)
                    return (false, null, null);

                return (true, userId, passwordHash);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Validates a user token and returns the associated user identifier if valid.
        /// </summary>
        /// <param name="token">Token value to validate.</param>
        /// <param name="dataSource">PostgreSQL data source used to create the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// The user identifier associated with the token if the token is valid; otherwise <c>null</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<long?> CheckUserTokenAsync(
            string token,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Respect cancellation before initiating the scalar query.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "SELECT public.check_user_token(@p_token);");

            command.Parameters.AddWithValue("p_token", token);

            try
            {
                var result = await command.ExecuteScalarAsync(cancellation);

                // Null or DBNull indicates that the token is invalid or not associated with any user.
                if (result is null || result is DBNull)
                    return null;

                // Cast is safe because the SQL function is expected to return a bigint value.
                return (long)result;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Retrieves the weights configuration for the specified user as a JSON string
        /// based on the row stored in the <c>public.weights</c> table.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose row in the <c>public.weights</c> table should be retrieved.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A JSON string representing the weights configuration for the specified user if a row exists
        /// in the <c>public.weights</c> table; otherwise <c>null</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<string?> GetWeightsJsonAsync(
            long userId,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Respect cancellation before initiating the scalar query.
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.get_weights_json(@p_user_id);");

            command.Parameters.AddWithValue("p_user_id", userId);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);

                // Null or DBNull indicates that there is no weights row for this user.
                if (result is null || result is DBNull)
                    return null;

                // Npgsql maps jsonb to string by default.
                return (string)result;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Retrieves the public user profile for the specified user as a JSON string,
        /// based on the row stored in the <c>public.users</c> table and related name tables.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose profile should be retrieved.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A JSON string representing the public profile of the specified user if a row exists
        /// in the <c>public.users</c> table; otherwise <c>null</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<string?> GetUserJsonAsync(
            long userId,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Respect cancellation before initiating the scalar query.
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.get_user_json(@p_user_id);");

            command.Parameters.AddWithValue("p_user_id", userId);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);

                // Null or DBNull indicates that the user does not exist.
                if (result is null || result is DBNull)
                    return null;

                // Npgsql maps jsonb to string by default.
                return (string)result;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>order_by_option</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.order_by_option</c> value should be updated.
        /// </param>
        /// <param name="orderByOption">
        /// Array of ordering option values to be stored in the <c>order_by_option</c> column
        /// of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsOrderByOptionAsync(
            long userId,
            string[] orderByOption,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Allow caller to cancel before any query is sent to the database.
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_order_by_option(@p_user_id, @p_order_by_option);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_order_by_option", orderByOption);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>mean_value_ids</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.mean_value_ids</c> value should be updated.
        /// </param>
        /// <param name="meanValueIds">
        /// Array of mean value identifiers to be stored in the <c>mean_value_ids</c> column
        /// of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeanValueIdsAsync(
            long userId,
            string[] meanValueIds,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_mean_value_ids(@p_user_id, @p_mean_value_ids);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_mean_value_ids", meanValueIds);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>vector</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.vector</c> value should be updated.
        /// </param>
        /// <param name="vector">
        /// Array of floating-point values representing the weights vector to be stored in the
        /// <c>vector</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsVectorAsync(
            long userId,
            float[] vector,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_vector(@p_user_id, @p_vector);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_vector", vector);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>mean_dist</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.mean_dist</c> value should be updated.
        /// </param>
        /// <param name="meanDist">
        /// Array of floating-point values representing mean distances to be stored in the
        /// <c>mean_dist</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeanDistAsync(
            long userId,
            float[] meanDist,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_mean_dist(@p_user_id, @p_mean_dist);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_mean_dist", meanDist);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_value_sum</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_value_sum</c> value should be updated.
        /// </param>
        /// <param name="meansValueSum">
        /// Array of floating-point values representing the sum of mean values to be stored in the
        /// <c>means_value_sum</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansValueSumAsync(
            long userId,
            float[] meansValueSum,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_value_sum(@p_user_id, @p_means_value_sum);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_value_sum", meansValueSum);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_value_ssum</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_value_ssum</c> value should be updated.
        /// </param>
        /// <param name="meansValueSsum">
        /// Array of double-precision values representing the sum of squared mean values to be stored in the
        /// <c>means_value_ssum</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansValueSsumAsync(
            long userId,
            double[] meansValueSsum,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_value_ssum(@p_user_id, @p_means_value_ssum);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_value_ssum", meansValueSsum);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_value_count</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_value_count</c> value should be updated.
        /// </param>
        /// <param name="meansValueCount">
        /// Array of integer counts representing the number of values contributing to each mean to be stored in the
        /// <c>means_value_count</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansValueCountAsync(
            long userId,
            int[] meansValueCount,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_value_count(@p_user_id, @p_means_value_count);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_value_count", meansValueCount);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_weight_sum</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_weight_sum</c> value should be updated.
        /// </param>
        /// <param name="meansWeightSum">
        /// Array of floating-point values representing the sum of weights to be stored in the
        /// <c>means_weight_sum</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansWeightSumAsync(
            long userId,
            float[] meansWeightSum,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_weight_sum(@p_user_id, @p_means_weight_sum);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_weight_sum", meansWeightSum);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_weight_ssum</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_weight_ssum</c> value should be updated.
        /// </param>
        /// <param name="meansWeightSsum">
        /// Array of double-precision values representing the sum of squared weights to be stored in the
        /// <c>means_weight_ssum</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansWeightSsumAsync(
            long userId,
            double[] meansWeightSsum,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_weight_ssum(@p_user_id, @p_means_weight_ssum);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_weight_ssum", meansWeightSsum);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Updates the <c>means_weight_count</c> column in the <c>public.weights</c> table
        /// for the specified user.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose <c>public.weights.means_weight_count</c> value should be updated.
        /// </param>
        /// <param name="meansWeightCount">
        /// Array of integer counts representing the number of weights contributing to each mean to be stored in the
        /// <c>means_weight_count</c> column of the <c>public.weights</c> table for the specified user.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and reported success; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">Thrown when an unexpected database error occurs.</exception>
        internal static async Task<bool> SetWeightsMeansWeightCountAsync(
            long userId,
            int[] meansWeightCount,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.set_weights_means_weight_count(@p_user_id, @p_means_weight_count);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_means_weight_count", meansWeightCount);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Checks whether the specified user has the given permission by invoking
        /// the <c>public.user_has_permission</c> function in the database.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose permissions should be checked.
        /// </param>
        /// <param name="permissionName">
        /// Name of the permission to be verified for the specified user.
        /// </param>
        /// <param name="dataSource">
        /// PostgreSQL data source used to create and execute the command.
        /// </param>
        /// <param name="cancellation">
        /// Cancellation token to observe during the operation.
        /// </param>
        /// <returns>
        /// <c>true</c> if the database operation completed successfully and the user
        /// has the specified permission; otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown when the operation is cancelled.
        /// </exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs.
        /// </exception>
        internal static async Task<bool> CheckPermission(
            long userId,
            string permissionName,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.user_has_permission(@p_user_id, @p_permission_name);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_permission_name", permissionName);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);
                return ConvertDbResultToBoolean(result);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error", ex);
            }
        }

        /// <summary>
        /// Retrieves the search history for the specified user as a JSON string
        /// using the <c>public.get_search_history_json</c> PostgreSQL function.
        /// The JSON result contains an array of search history entries ordered
        /// from the most recent to the oldest one.
        /// </summary>
        /// <param name="userId">
        /// Identifier of the user whose search history should be retrieved.
        /// </param>
        /// <param name="limit">
        /// Maximum number of search history entries to return. If the value is less than
        /// or equal to zero, an empty JSON array is returned.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// A JSON string representing the search history for the specified user. When no
        /// matching entries exist, an empty JSON array (<c>[]</c>) is returned.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when an unexpected database error occurs while retrieving the search history.
        /// </exception>
        internal static async Task<string> GetSearchHistoryJsonAsync(
            long userId,
            int limit,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Respect cancellation before initiating the scalar query.
            cancellation.ThrowIfCancellationRequested();

            await using NpgsqlCommand command = dataSource.CreateCommand(
                "SELECT public.get_search_history_json(@p_user_id, @p_limit);");

            command.Parameters.AddWithValue("p_user_id", userId);
            command.Parameters.AddWithValue("p_limit", limit);

            try
            {
                object? result = await command.ExecuteScalarAsync(cancellation);

                // If the function would ever return NULL (it currently returns []), fallback to empty array.
                if (result is null || result is DBNull)
                    return "[]";

                // Npgsql maps jsonb to string by default.
                return (string)result;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error while retrieving search history.", ex);
            }
        }

        /// <summary>
        /// Inserts a new search history entry for the specified user using the
        /// <c>public.insert_search_history</c> stored procedure.
        /// </summary>
        /// <param name="userId">Identifier of the user for whom the search history entry is created.</param>
        /// <param name="keywords">Optional search keywords used by the user.</param>
        /// <param name="distance">Maximum distance used in the query.</param>
        /// <param name="isRemote">Indicates whether the search was limited to remote offers.</param>
        /// <param name="isHybrid">Indicates whether the search was limited to hybrid offers.</param>
        /// <param name="leadingCategoryId">Optional identifier of the leading category used in the search.</param>
        /// <param name="cityId">Optional identifier of the city used in the search.</param>
        /// <param name="salaryFrom">Lower bound of the salary range used in the search.</param>
        /// <param name="salaryTo">Upper bound of the salary range used in the search.</param>
        /// <param name="salaryPeriodId">Optional salary period identifier.</param>
        /// <param name="salaryCurrencyId">Optional salary currency identifier.</param>
        /// <param name="salaryTypeId">Optional salary type identifier.</param>
        /// <param name="employmentScheduleIds">
        /// Optional collection of employment schedule identifiers used in the search.
        /// </param>
        /// <param name="employmentTypeIds">
        /// Optional collection of employment type identifiers used in the search.
        /// </param>
        /// <param name="dataSource">PostgreSQL data source used to create and execute the command.</param>
        /// <param name="cancellation">Cancellation token to observe during the operation.</param>
        /// <returns>
        /// <c>true</c> when the stored procedure reports success via the <c>o_result</c> output parameter;
        /// otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
        /// <exception cref="UserDbQueryException">
        /// Thrown when the procedure does not return a valid result row or an unexpected database error occurs.
        /// </exception>
        internal static async Task<bool> InsertSearchHistoryAsync(
            long userId,
            string? keywords,
            int? distance,
            bool? isRemote,
            bool? isHybrid,
            short? leadingCategoryId,
            int? cityId,
            decimal salaryFrom,
            decimal salaryTo,
            short? salaryPeriodId,
            short? salaryCurrencyId,
            short? salaryTypeId,
            short[]? employmentScheduleIds,
            short[]? employmentTypeIds,
            NpgsqlDataSource dataSource,
            CancellationToken cancellation = default)
        {
            // Fail fast if a cancellation was already requested before starting any I/O.
            cancellation.ThrowIfCancellationRequested();

            await using var command = dataSource.CreateCommand(
                "CALL public.insert_search_history(" +
                "@p_user_id, " +
                "@p_keywords, " +
                "@p_distance, " +
                "@p_is_remote, " +
                "@p_is_hybrid, " +
                "@p_leading_category_id, " +
                "@p_city_id, " +
                "@p_salary_from, " +
                "@p_salary_to, " +
                "@p_salary_period_id, " +
                "@p_salary_currency_id, " +
                "@p_salary_type_id, " +
                "@p_employment_schedule_ids, " +
                "@p_employment_type_ids, " +
                "NULL)");

            // Required parameter
            command.Parameters.AddWithValue("p_user_id", userId);

            // Optional parameters: map nulls to DBNull.Value so that procedure defaults can be used when appropriate.
            command.Parameters.AddWithValue("p_keywords", (object?)keywords ?? DBNull.Value);
            command.Parameters.AddWithValue("p_distance", (object?)distance ?? DBNull.Value);

            command.Parameters.AddWithValue("p_is_remote", (object?)isRemote ?? DBNull.Value);
            command.Parameters.AddWithValue("p_is_hybrid", (object?)isHybrid ?? DBNull.Value);

            command.Parameters.AddWithValue("p_leading_category_id", (object?)leadingCategoryId ?? DBNull.Value);
            command.Parameters.AddWithValue("p_city_id", (object?)cityId ?? DBNull.Value);

            command.Parameters.AddWithValue("p_salary_from", salaryFrom);
            command.Parameters.AddWithValue("p_salary_to", salaryTo);

            command.Parameters.AddWithValue("p_salary_period_id", (object?)salaryPeriodId ?? DBNull.Value);
            command.Parameters.AddWithValue("p_salary_currency_id", (object?)salaryCurrencyId ?? DBNull.Value);
            command.Parameters.AddWithValue("p_salary_type_id", (object?)salaryTypeId ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "p_employment_schedule_ids",
                (object?)employmentScheduleIds ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "p_employment_type_ids",
                (object?)employmentTypeIds ?? DBNull.Value);

            try
            {
                // SingleRow hints to the provider that at most one row is expected, allowing optimizations.
                await using var reader = await command
                    .ExecuteReaderAsync(CommandBehavior.SingleRow, cancellation);

                // Enforce contract that the stored procedure must always return a row with the OUT parameter(s).
                if (!await reader.ReadAsync(cancellation))
                    throw new UserDbQueryException("No result returned from `insert_search_history`.");

                int resultOrdinal = reader.GetOrdinal("o_result");

                // Missing or NULL result is treated as failure.
                bool success = !reader.IsDBNull(resultOrdinal) &&
                               reader.GetBoolean(resultOrdinal);

                return success;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new UserDbQueryException("Database error while inserting search history.", ex);
            }
        }

    }
}
