namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for registering a new user with email and password.
    /// </summary>
    public sealed class StandardRegisterUserRequest
    {
        /// <summary>
        /// User email address (acts as login).
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// User password (must satisfy the server-side password policy).
        /// </summary>
        public string Password { get; init; } = string.Empty;

        /// <summary>
        /// User first name.
        /// </summary>
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// User last name.
        /// </summary>
        public string LastName { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for standard registration.
    /// </summary>
    public sealed class StandardRegisterUserResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
