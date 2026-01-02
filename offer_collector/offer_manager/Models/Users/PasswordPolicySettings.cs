namespace offer_manager.Models.Users
{
    /// <summary>
    /// Password policy configuration used to validate user passwords.
    /// Values are designed to be bound from configuration (e.g., appsettings.json),
    /// with sensible defaults matching the constructor defaults.
    /// </summary>
    internal sealed class PasswordPolicySettings
    {
        /// <summary>
        /// Minimum allowed password length.
        /// </summary>
        public int MinLength { get; init; } = 12;

        /// <summary>
        /// Maximum allowed password length.
        /// </summary>
        public int MaxLength { get; init; } = 128;

        /// <summary>
        /// If true, password must contain at least one uppercase letter (A-Z).
        /// </summary>
        public bool RequireUppercase { get; init; } = false;

        /// <summary>
        /// If true, password must contain at least one lowercase letter (a-z).
        /// </summary>
        public bool RequireLowercase { get; init; } = false;

        /// <summary>
        /// If true, password must contain at least one digit (0-9).
        /// </summary>
        public bool RequireDigit { get; init; } = false;

        /// <summary>
        /// If true, password must contain at least one non-alphanumeric character (special character).
        /// </summary>
        public bool RequireNonAlphanumeric { get; init; } = false;

        /// <summary>
        /// Minimum number of unique characters required in the password.
        /// </summary>
        public int RequiredUniqueChars { get; init; } = 4;

        /// <summary>
        /// Allowed special characters when <see cref="RequireNonAlphanumeric"/> is enabled.
        /// If null, the allowed set is not restricted by this setting.
        /// </summary>
        public string? AllowedSpecialCharacters { get; init; } = "!@#$%^&*()-_=+[]{};:,.<>?";

        /// <summary>
        /// Optional file path to a list of known/compromised passwords to reject.
        /// If null, this check is disabled.
        /// </summary>
        public string? KnownPasswordsListPath { get; init; } = null;
    }
}
