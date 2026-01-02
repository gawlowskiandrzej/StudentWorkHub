namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for logging in with email (login) and password.
    /// </summary>
    public sealed class StandardLoginRequest
    {
        /// <summary>
        /// Login identifier (email address).
        /// </summary>
        public string Login { get; init; } = string.Empty;

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; init; } = string.Empty;

        /// <summary>
        /// When true, the server may return a remember-me token to allow token-based login.
        /// </summary>
        public bool RememberMe { get; init; }
    }

    /// <summary>
    /// Response payload for standard login.
    /// </summary>
    public sealed class StandardLoginResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// JSON Web Token issued on successful authentication; empty on failure.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Remember-me token (returned when enabled); empty string if not issued or on error.
        /// </summary>
        public string RememberMeToken { get; init; } = string.Empty;
    }
}
