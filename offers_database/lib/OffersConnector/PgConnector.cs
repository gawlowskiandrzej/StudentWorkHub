using Npgsql;
using System.Data.Common;

namespace OffersConnector
{
    public sealed class PgConnector : IAsyncDisposable
    {
        private readonly NpgsqlDataSource _dataSource;
        public PgConnector(string username, string password, string host = "127.0.0.1", int port = 5432, string dbName = "offers")
        {
            // Validate input parameters before building connection string
            if (string.IsNullOrEmpty(username)) throw new PgConnectorException("username must not be empty");
            if (string.IsNullOrEmpty(password)) throw new PgConnectorException("password must not be empty");
            if (string.IsNullOrEmpty(host)) throw new PgConnectorException("host must not be empty");
            if (port < 1024 || port > 65535) throw new PgConnectorException("port must be in range <1024; 65535>");
            if (string.IsNullOrEmpty(dbName)) throw new PgConnectorException("dbName must not be empty");

            // Build the PostgreSQL connection string with pooling enabled
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = dbName,
                Pooling = true,  // Enable connection pooling for performance
                Timeout = 5      // Connection timeout in seconds
            };

            try
            {
                // Attempt to create the data source using the validated builder
                _dataSource = NpgsqlDataSource.Create(builder);
            }
            catch (ArgumentException ex)
            {
                throw new PgConnectorException("Invalid connection string", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PgConnectorException("Configuration error", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("PostgreSQL error", ex);
            }
            catch (Exception ex)
            {
                throw new PgConnectorException("Unexpected error", ex);
            }

            // Safety check – ensure datasource was successfully created
            if (_dataSource == null)
            {
                throw new PgConnectorException("NpgsqlDataSource creation failed");
            }
        } 

        public async Task<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
            return connection;
        }

        /// <summary>
        /// Asynchronously disposes of the NpgsqlDataSource, 
        /// ensuring that the connection pool and resources are properly released.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            // Dispose the data source asynchronously to gracefully clean up the pool
            await _dataSource.DisposeAsync().ConfigureAwait(false);
        }
    }
}
