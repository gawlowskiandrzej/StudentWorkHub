namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for logging out the authenticated user.
    /// </summary>
    public sealed class LogoutRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for logout operation.
    /// </summary>
    public sealed class LogoutResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
