namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for partially updating personal offer preferences.
    /// At least one optional field (besides JWT) must be provided.
    /// </summary>
    public sealed class UpdatePreferencesRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Leading category ID (offers dictionary ID); null means "do not update".
        /// </summary>
        public short? LeadingCategoryId { get; init; }

        /// <summary>
        /// Minimum salary preference; null means "do not update".
        /// </summary>
        public decimal? SalaryFrom { get; init; }

        /// <summary>
        /// Maximum salary preference; null means "do not update".
        /// </summary>
        public decimal? SalaryTo { get; init; }

        /// <summary>
        /// Employment type IDs (offers dictionary IDs); null means "do not update".
        /// </summary>
        public short[]? EmploymentTypeIds { get; init; }

        /// <summary>
        /// Language ID (offers dictionary ID). Must be sent together with <c>LanguageLevelId</c> when updating language.
        /// </summary>
        public short? LanguageId { get; init; }

        /// <summary>
        /// Language level ID (offers dictionary ID). Must be sent together with <c>LanguageId</c> when updating language.
        /// </summary>
        public short? LanguageLevelId { get; init; }

        /// <summary>
        /// Job status identifier/name (e.g. "actively_looking"); null means "do not update".
        /// </summary>
        public string? JobStatusName { get; init; }

        /// <summary>
        /// City name preference; null means "do not update".
        /// </summary>
        public string? CityName { get; init; }

        /// <summary>
        /// Work type names (e.g. "remote", "hybrid"); null means "do not update".
        /// </summary>
        public string[]? WorkTypeNames { get; init; }

        /// <summary>
        /// Skill names array. Must be sent together with <c>SkillMonths</c> and both arrays must have the same length.
        /// </summary>
        public string[]? SkillNames { get; init; }

        /// <summary>
        /// Skill experience months array. Must be sent together with <c>SkillNames</c> and both arrays must have the same length.
        /// </summary>
        public short[]? SkillMonths { get; init; }
    }

    /// <summary>
    /// Response payload for preferences update.
    /// </summary>
    public sealed class UpdatePreferencesResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Indicates whether <c>LeadingCategoryId</c> was updated.
        /// </summary>
        public bool LeadingCategoryIdUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>SalaryFrom</c> was updated.
        /// </summary>
        public bool SalaryFromUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>SalaryTo</c> was updated.
        /// </summary>
        public bool SalaryToUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>EmploymentTypeIds</c> was updated.
        /// </summary>
        public bool EmploymentTypeIdsUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>LanguageId</c> was updated.
        /// </summary>
        public bool LanguageIdUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>LanguageLevelId</c> was updated.
        /// </summary>
        public bool LanguageLevelIdUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>JobStatusName</c> was updated.
        /// </summary>
        public bool JobStatusNameUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>CityName</c> was updated.
        /// </summary>
        public bool CityNameUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>WorkTypeNames</c> was updated.
        /// </summary>
        public bool WorkTypeNamesUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>SkillNames</c> was updated.
        /// </summary>
        public bool SkillNamesUpdated { get; init; } = false;

        /// <summary>
        /// Indicates whether <c>SkillMonths</c> was updated.
        /// </summary>
        public bool SkillMonthsUpdated { get; init; } = false;
    }
}
