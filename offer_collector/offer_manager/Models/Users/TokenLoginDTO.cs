namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for logging in using a remember-me token.
    /// </summary>
    public sealed class TokenLoginRequest
    {
        /// <summary>
        /// Remember-me token obtained from standard login.
        /// </summary>
        public string Token { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for token-based login.
    /// </summary>
    public sealed class TokenLoginResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Newly issued JSON Web Token; empty on failure.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Refreshed remember-me token; empty on failure.
        /// </summary>
        public string RememberMeToken { get; init; } = string.Empty;
    }
}
