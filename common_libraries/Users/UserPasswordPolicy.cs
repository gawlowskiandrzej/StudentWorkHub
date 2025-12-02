using System.Collections.Frozen;

namespace Users
{
    /// <summary>
    /// Defines password complexity requirements for user accounts.
    /// </summary>
    public readonly struct UserPasswordPolicy
    {
        /// <summary>
        /// Minimum allowed password length.
        /// </summary>
        public int MinLength { get; }

        /// <summary>
        /// Maximum allowed password length.
        /// </summary>
        public int MaxLength { get; }

        /// <summary>
        /// Whether at least one uppercase letter is required.
        /// </summary>
        public bool RequireUppercase { get; }

        /// <summary>
        /// Whether at least one lowercase letter is required.
        /// </summary>
        public bool RequireLowercase { get; }

        /// <summary>
        /// Whether at least one digit is required.
        /// </summary>
        public bool RequireDigit { get; }

        /// <summary>
        /// Whether at least one special character is required.
        /// </summary>
        public bool RequireNonAlphanumeric { get; }

        /// <summary>
        /// Minimum number of distinct characters required.
        /// </summary>
        public int RequiredUniqueChars { get; }

        /// <summary>
        /// Allowed special characters when validating non-alphanumeric characters.
        /// </summary>
        public string AllowedSpecialCharacters { get; }

        /// <summary>
        /// Set of known passwords that must be rejected during validation.
        /// This list is loaded once during construction and then treated as read-only.
        /// </summary>
        private FrozenSet<string> KnownPasswordsList { get; }

        /// <summary>
        /// Creates a password policy with normalization of provided values.
        /// </summary>
        /// <param name="minLength">Minimum length; values &lt;= 0 fall back to 12.</param>
        /// <param name="maxLength">Maximum length; values &lt;= 0 fall back to 128.</param>
        /// <param name="requireUppercase">Require at least one uppercase letter.</param>
        /// <param name="requireLowercase">Require at least one lowercase letter.</param>
        /// <param name="requireDigit">Require at least one digit.</param>
        /// <param name="requireNonAlphanumeric">Require at least one special character.</param>
        /// <param name="requiredUniqueChars">Minimum distinct characters; values &lt;= 0 fall back to 4.</param>
        /// <param name="allowedSpecialCharacters">Set of allowed special characters; null becomes empty.</param>
        /// <param name="knownPasswordsListPath">
        /// Optional path to a file containing one known password per line; when provided,
        /// any password present in this list will be rejected during validation.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Thrown when <paramref name="knownPasswordsListPath"/> is not null or whitespace
        /// and the file does not exist.
        /// </exception>
        public UserPasswordPolicy(
            int minLength = 12,
            int maxLength = 128,
            bool requireUppercase = false,
            bool requireLowercase = false,
            bool requireDigit = false,
            bool requireNonAlphanumeric = false,
            int requiredUniqueChars = 4,
            string? allowedSpecialCharacters = "!@#$%^&*()-_=+[]{};:,.<>?",
            string? knownPasswordsListPath = null)
        {
            // Normalize configuration values to safe defaults.
            minLength = minLength > 0 ? minLength : 12;
            maxLength = maxLength > 0 ? maxLength : 128;
            int tmpMin = Math.Min(minLength, maxLength);
            maxLength = Math.Max(maxLength, tmpMin);
            minLength = tmpMin;
            requiredUniqueChars = requiredUniqueChars > 0 ? requiredUniqueChars : 4;
            allowedSpecialCharacters = allowedSpecialCharacters is not null ? new string([.. allowedSpecialCharacters.Distinct()]) : string.Empty;

            KnownPasswordsList = FrozenSet<string>.Empty;
            if (!string.IsNullOrWhiteSpace(knownPasswordsListPath))
            {
                if (!File.Exists(knownPasswordsListPath))
                    throw new FileNotFoundException($"File {knownPasswordsListPath} doesn't exist");

                KnownPasswordsList = File
                                    .ReadAllLines(knownPasswordsListPath)
                                    .Select(line => line.Trim())
                                    .Where(line => !string.IsNullOrEmpty(line))
                                    .ToFrozenSet();
            }

            MinLength = minLength;
            MaxLength = maxLength;
            RequireUppercase = requireUppercase;
            RequireLowercase = requireLowercase;
            RequireDigit = requireDigit;
            RequireNonAlphanumeric = requireNonAlphanumeric;
            RequiredUniqueChars = requiredUniqueChars;
            AllowedSpecialCharacters = allowedSpecialCharacters;
        }

        /// <summary>
        /// Validates a password against the current policy.
        /// </summary>
        /// <param name="password">Password candidate to validate.</param>
        /// <returns>
        /// (IsValid, Error) where Error contains details when password is invalid.
        /// </returns>
        public (bool IsValid, string Error) ValidatePassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "nullPassword, expected: non-null");

            int length = password.Length;
            if (length < MinLength)
                return (false, $"minLength, expected: {MinLength}, got {length}");

            if (length > MaxLength)
                return (false, $"maxLength, expected: {MaxLength}, got {length}");

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasNonAlphanumeric = false;

            // Reject passwords that are explicitly marked as known (e.g., leaked/common passwords).
            if (KnownPasswordsList.Contains(password))
                return (false, "knownPassword, this password is already known.");

            var seenCharacters = new HashSet<char>();
            int uniqueChars = 0;
            foreach (char c in password)
            {
                // Reject whitespace control characters (e.g., tab, newline).
                // A regular space character ' ' is allowed to make passphrases possible.
                if (char.IsWhiteSpace(c))
                {
                    if (c != ' ')
                        return (false, "invalidCharacter, whitespace is not allowed in password");

                    if (seenCharacters.Add(c))
                        uniqueChars++;

                    continue;
                }


                if (char.IsUpper(c))
                    hasUpper = true;

                if (char.IsLower(c))
                    hasLower = true;

                if (char.IsDigit(c))
                    hasDigit = true;

                if (!char.IsLetterOrDigit(c))
                {
                    hasNonAlphanumeric = true;

                    // Enforce allowed special characters when configured.
                    if (!string.IsNullOrEmpty(AllowedSpecialCharacters) &&
                        !AllowedSpecialCharacters.Contains(c))
                    {
                        return (false, "invalidCharacter, unexpected special character used");
                    }
                }

                if (seenCharacters.Add(c))
                    uniqueChars++;
            }

            if (RequireUppercase && !hasUpper)
                return (false, "uppercase, expected: at least 1 uppercase character");

            if (RequireLowercase && !hasLower)
                return (false, "lowercase, expected: at least 1 lowercase character");

            if (RequireDigit && !hasDigit)
                return (false, "digit, expected: at least 1 digit character");

            if (RequireNonAlphanumeric && !hasNonAlphanumeric)
                return (false, "nonAlphanumeric, expected: at least 1 special character");

            // Enforce diversity of characters to reduce simple repetition patterns.
            if (uniqueChars < RequiredUniqueChars)
                return (false, $"uniqueChars, expected: {RequiredUniqueChars}, got {uniqueChars}");

            return (true, string.Empty);
        }
    }
}
