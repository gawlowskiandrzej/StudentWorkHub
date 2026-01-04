namespace shared_models
{
    public class DatabaseSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string Database { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
