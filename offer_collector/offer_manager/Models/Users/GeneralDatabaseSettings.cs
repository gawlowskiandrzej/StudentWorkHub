namespace offer_manager.Models.Users
{
    /// <summary>
    /// Configuration model for connecting to the general PostgreSQL database.
    /// </summary>
    internal class GeneralDatabaseSettings
    {
        /// <summary>
        /// Database server host address (IP or DNS name).
        /// </summary>
        public string Host { get; init; } = "127.0.0.1";

        /// <summary>
        /// Database server port.
        /// </summary>
        public int Port { get; init; } = 5433;

        /// <summary>
        /// Target database name.
        /// </summary>
        public string Database { get; init; } = "general";

        /// <summary>
        /// Username used for authentication.
        /// </summary>
        public string Username { get; init; } = "postgres";

        /// <summary>
        /// Password used for authentication.
        /// </summary>
        public string Password { get; init; } = "1qazXSW@";
    }
}
