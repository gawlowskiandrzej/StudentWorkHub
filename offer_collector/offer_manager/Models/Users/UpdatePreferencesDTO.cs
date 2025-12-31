namespace offer_manager.Models.Users
{
    public sealed class UpdatePreferencesRequest
    {
        public string Jwt { get; init; } = string.Empty;

        public short? LeadingCategoryId { get; init; }
        public decimal? SalaryFrom { get; init; }
        public decimal? SalaryTo { get; init; }
        public short[]? EmploymentTypeIds { get; init; }
        public short? LanguageId { get; init; }
        public short? LanguageLevelId { get; init; }
        public string? JobStatusName { get; init; }
        public string? CityName { get; init; }
        public string[]? WorkTypeNames { get; init; }
        public string[]? SkillNames { get; init; }
        public short[]? SkillMonths { get; init; }
    }
    public sealed class UpdatePreferencesResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;

        public bool LeadingCategoryIdUpdated { get; init; } = false;
        public bool SalaryFromUpdated { get; init; } = false;
        public bool SalaryToUpdated { get; init; } = false;
        public bool EmploymentTypeIdsUpdated { get; init; } = false;
        public bool LanguageIdUpdated { get; init; } = false;
        public bool LanguageLevelIdUpdated { get; init; } = false;
        public bool JobStatusNameUpdated { get; init; } = false;
        public bool CityNameUpdated { get; init; } = false;
        public bool WorkTypeNamesUpdated { get; init; } = false;
        public bool SkillNamesUpdated { get; init; } = false;
        public bool SkillMonthsUpdated { get; init; } = false;
    }

}
