namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for checking whether the authenticated user has a given permission.
    /// </summary>
    public sealed class CheckPermissionRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Permission name to check (e.g. "admin_panel_access").
        /// </summary>
        public string PermissionName { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for permission check.
    /// </summary>
    public sealed class CheckPermissionResponse
    {
        /// <summary>
        /// Error message (empty string indicates permission granted; non-empty otherwise).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
