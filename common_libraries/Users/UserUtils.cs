using Npgsql;

namespace Users
{
    /// <summary>
    /// Helper utilities for creating and configuring PostgreSQL data sources.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: Messages inside thrown <see cref="UserException"/> instances are
    /// intended for logs and internal diagnostics only. Do NOT pass them directly
    /// to end users or display them in UI. Always map to generic user-facing
    /// messages such as "Cannot connect to database" without internal details.
    /// </remarks>
    public class UserUtils
    {
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
        public static NpgsqlDataSource CreateDataSource(string username, string password, string host = "127.0.0.1", int port = 5433, string dbName = "general")
        {
            if (string.IsNullOrEmpty(username)) throw new UserException("username must not be empty");
            if (string.IsNullOrEmpty(password)) throw new UserException("password must not be empty");
            if (string.IsNullOrEmpty(host)) throw new UserException("host must not be empty");
            if (port < 1024 || port > 65535) throw new UserException("port must be in range <1024; 65535>");
            if (string.IsNullOrEmpty(dbName)) throw new UserException("dbName must not be empty");

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
                return dataSourceBuilder.Build();
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
    }
}
