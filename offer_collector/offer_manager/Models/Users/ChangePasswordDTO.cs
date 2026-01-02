namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for changing the user password.
    /// </summary>
    public sealed class ChangePasswordRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// New password (must satisfy the server-side password policy).
        /// </summary>
        public string NewPassword { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for change-password operation.
    /// </summary>
    public sealed class ChangePasswordResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
