namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for partially updating user profile data.
    /// At least one optional field (besides JWT) must be provided.
    /// </summary>
    public sealed class UpdateDataRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// New first name; null means "do not update".
        /// </summary>
        public string? UserFirstName { get; init; }

        /// <summary>
        /// New second/middle name; null means "do not update".
        /// </summary>
        public string? UserSecondName { get; init; }

        /// <summary>
        /// New last name; null means "do not update".
        /// </summary>
        public string? UserLastName { get; init; }

        /// <summary>
        /// New phone number (e.g. "+48111111111"); null means "do not update".
        /// </summary>
        public string? UserPhone { get; init; }
    }

    /// <summary>
    /// Response payload for profile update.
    /// </summary>
    public sealed class UpdateDataResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Indicates whether <c>UserFirstName</c> was updated.
        /// </summary>
        public bool UserFirstNameUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>UserSecondName</c> was updated.
        /// </summary>
        public bool UserSecondNameUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>UserLastName</c> was updated.
        /// </summary>
        public bool UserLastNameUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>UserPhone</c> was updated.
        /// </summary>
        public bool UserPhoneUpdated { get; init; } = false;
    }
}
