namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for deleting the authenticated user's account.
    /// </summary>
    public sealed class DeleteUserRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for delete-user operation.
    /// </summary>
    public sealed class DeleteUserResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
