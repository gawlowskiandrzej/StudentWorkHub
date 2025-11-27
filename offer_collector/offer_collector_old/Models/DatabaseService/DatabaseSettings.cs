namespace Offer_collector.Models.DatabaseService
{
    public class DatabaseSettings
    {
        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5432;
        public string Database { get; set; } = "offers";
        public string Username { get; set; } = "postgres";
        public string Password { get; set; } = "1qazXSW@";
    }
}
