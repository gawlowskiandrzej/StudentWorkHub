using Ganss.Xss;

namespace Users
{
    /// <summary>
    /// Shared configuration and utilities related to user encryption functions.
    /// </summary>
    /// <remarks>
    /// WARNING: Any details retrieved from this configuration (like versioning)
    /// should not be exposed directly to end users. Treat them as internal-only.
    /// </remarks>
    internal static class SharedUserSettings
    {
        /// <summary>
        /// Shared length of the remember-me token used across all user instances.
        /// </summary>
        internal static readonly int rememberMeTokenLength = 256;

        /// <summary>
        /// Maximum number of attempts to generate a unique remember-me token before failing.
        /// </summary>
        internal static readonly int generateRememberTokenMaxTries = 20;

        /// <summary>
        /// Latest supported encryption function version.
        /// </summary>
        internal static readonly int encryptionFunctionVersion = 1;

        /// <summary>
        /// Shared HtmlSanitizer instance reused across calls to minimize allocations
        /// and enforce a single, consistent sanitization configuration.
        /// </summary>
        private static readonly HtmlSanitizer _htmlSanitizer = new();

        /// <summary>
        /// Resolves the password hashing function delegate for the requested encryption version.
        /// </summary>
        /// <param name="encryptionFunctionVersion">
        /// Requested encryption function version. Values outside the supported range
        /// are clamped to the nearest supported version.
        /// </param>
        /// <returns>
        /// Delegate representing the selected password hashing function for the resolved version.
        /// </returns>
        internal static Func<string?, UserPasswordPolicy, string> ResolveGetPasswordHash(int encryptionFunctionVersion)
        {
            // Clamp requested version to supported range to avoid invalid values.
            encryptionFunctionVersion = Math.Clamp(encryptionFunctionVersion, 1, SharedUserSettings.encryptionFunctionVersion);

            return encryptionFunctionVersion switch
            {
                1 => EncryptionFunctionsV1.GetPasswordHash,
                _ => EncryptionFunctionsV1.GetPasswordHash, // Default should always point to newest version
            };
        }

        /// <summary>
        /// Resolves the password verification function delegate for the requested encryption version.
        /// </summary>
        /// <param name="encryptionFunctionVersion">
        /// Requested encryption function version. Values outside the supported range
        /// are clamped to the nearest supported version.
        /// </param>
        /// <returns>
        /// Delegate representing the selected password verification function for the resolved version.
        /// </returns>
        internal static Func<string?, string?, bool> ResolveVerifyPassword(int encryptionFunctionVersion)
        {
            // Clamp requested version to supported range to avoid invalid values.
            encryptionFunctionVersion = Math.Clamp(encryptionFunctionVersion, 1, SharedUserSettings.encryptionFunctionVersion);

            return encryptionFunctionVersion switch
            {
                1 => EncryptionFunctionsV1.VerifyPassword,
                _ => EncryptionFunctionsV1.VerifyPassword, // Default should always point to newest version
            };
        }

        /// <summary>
        /// Resolves the dummy hash generation function delegate for the requested encryption version.
        /// </summary>
        /// <param name="encryptionFunctionVersion">
        /// Requested encryption function version. Values outside the supported range
        /// are clamped to the nearest supported version.
        /// </param>
        /// <returns>
        /// A function delegate that generates a dummy hash for the resolved encryption version.
        /// The delegate takes the desired password length as an argument and returns the dummy hash string.
        /// </returns>
        internal static Func<int, string> ResolveGenerateDummyHash(int encryptionFunctionVersion)
        {
            // Clamp requested version to supported range to avoid invalid values.
            encryptionFunctionVersion = Math.Clamp(encryptionFunctionVersion, 1, SharedUserSettings.encryptionFunctionVersion);

            return encryptionFunctionVersion switch
            {
                1 => EncryptionFunctionsV1.GenerateDummyHash,
                _ => EncryptionFunctionsV1.GenerateDummyHash, // Default should always point to newest version
            };
        }

        /// <summary>
        /// Sanitizes a potentially HTML-containing string, trims it, and optionally truncates its length.
        /// </summary>
        /// <param name="text">
        /// Input text that may contain HTML or unsafe content. Can be null or whitespace.
        /// </param>
        /// <param name="trimToLength">
        /// Maximum length of the returned string. If null, full sanitized text is returned.
        /// If less than or equal to zero, an empty string may be returned depending on <paramref name="allowNull"/>.
        /// </param>
        /// <param name="allowNull">
        /// If true, null is returned when the input is null/whitespace or becomes empty after sanitization.
        /// If false, an empty string is returned in such cases.
        /// </param>
        /// <returns>
        /// Sanitized, trimmed (and optionally truncated) string, or null/empty based on <paramref name="allowNull"/>.
        /// </returns>
        internal static string? SanitizeString(string? text, int? trimToLength, bool allowNull = true)
        {
            // Short-circuit for null/whitespace inputs to avoid unnecessary sanitization work.
            if (string.IsNullOrWhiteSpace(text))
                return allowNull ? null : string.Empty;

            // HtmlSanitizer neutralizes potentially dangerous HTML/script payloads while preserving readable content.
            string plainText = _htmlSanitizer.Sanitize(text);

            // Normalize surrounding whitespace to ensure consistent length and content checks.
            plainText = plainText.Trim();
            if (string.IsNullOrWhiteSpace(plainText))
                return allowNull ? null : string.Empty;

            // Ensure requested length is non-negative and never exceeds actual content length.
            // A non-positive length effectively collapses to an empty or null result depending on allowNull.
            if (trimToLength is not null)
                trimToLength = trimToLength > 0 ? Math.Min((int)trimToLength, plainText.Length) : 0;

            // Use range slicing to truncate content in a single, bounds-checked operation.
            return plainText[..(trimToLength ?? plainText.Length)];            
        }
    }
}
