using Npgsql;
using System.Xml.Linq;

namespace OffersConnector
{
    public sealed class PgConnector : IAsyncDisposable
    {
        private readonly NpgsqlDataSource _dataSource;

        public PgConnector(string username, string password, string host = "127.0.0.1", int port = 5432, string dbName = "offers")
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(host)) throw new ArgumentNullException(nameof(host));
            if (port < 1024 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port), "Port must be in range <1024; 65535>");
            if (string.IsNullOrEmpty(dbName)) throw new ArgumentNullException(nameof(dbName));

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = dbName,
                Pooling = true,
                Timeout = 5
            };

            try
            {
                _dataSource = NpgsqlDataSource.Create(builder);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid connection string: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Configuration error: {ex.Message}");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            if (_dataSource == null) 
            { 
            
            }
#TODO customowe exceptions do tego
        }

        public async ValueTask DisposeAsync()
        {
            // Dispose the data source asynchronously to gracefully clean up the pool.
            await _dataSource.DisposeAsync().ConfigureAwait(false);
        }
    }
}
