namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for refreshing a JWT.
    /// </summary>
    public sealed class RefreshJwtRequest
    {
        /// <summary>
        /// Current JSON Web Token to refresh.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for refresh-jwt operation.
    /// </summary>
    public sealed class RefreshJwtResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Newly issued JSON Web Token; empty on failure.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }
}
