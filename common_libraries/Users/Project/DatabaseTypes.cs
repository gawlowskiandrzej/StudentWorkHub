namespace Users
{
    using System;
    using NpgsqlTypes;

    /// <summary>
    /// Represents job-offer preferences for a user.
    /// Includes preferred work types and user skills (skills are returned as parallel arrays aligned by index).
    /// </summary>
    /// <param name="LeadingCategoryId">
    /// Legacy study-field identifier (historically named "leading category").
    /// </param>
    /// <param name="SalaryFrom">Preferred salary range start.</param>
    /// <param name="SalaryTo">Preferred salary range end.</param>
    /// <param name="EmploymentTypes">
    /// Offer-related employment type identifiers managed by the application (e.g. "employment contract").
    /// </param>
    /// <param name="JobStatusId">
    /// User job-search status identifier (e.g. open to offers / do not notify).
    /// </param>
    /// <param name="Languages">Language identifiers managed by the application.</param>
    /// <param name="LanguageLevels">Language level identifiers aligned by index with <paramref name="Languages"/>.</param>
    /// <param name="CityId">Identifier of the city where the user lives or wants to search jobs.</param>
    /// <param name="WorkTypes">
    /// Preferred work types such as on-site/remote/hybrid.
    /// </param>
    /// <param name="SkillNames">Skill names aligned by index with <paramref name="SkillExperienceMonths"/> and <paramref name="SkillEntryDates"/>.</param>
    /// <param name="SkillExperienceMonths">Experience in months aligned by index with <paramref name="SkillNames"/>.</param>
    /// <param name="SkillEntryDates">Skill entry dates aligned by index with <paramref name="SkillNames"/>.</param>
    public sealed class UserPreferencesResult
    {
        [PgName("leading_category_id")] public short? LeadingCategoryId { get; init; }
        [PgName("salary_from")] public decimal? SalaryFrom { get; init; }
        [PgName("salary_to")] public decimal? SalaryTo { get; init; }

        [PgName("employment_type_ids")] public short[] EmploymentTypes { get; init; } = [];
        [PgName("job_status_id")] public short JobStatusId { get; init; }

        [PgName("language_ids")] public short[] Languages { get; init; } = [];
        [PgName("language_level_ids")] public short[] LanguageLevels { get; init; } = [];

        [PgName("city_id")] public long CityId { get; init; }

        [PgName("work_types")] public string[] WorkTypes { get; init; } = [];

        [PgName("skill_names")] public string[] SkillNames { get; init; } = [];
        [PgName("skill_experience_months")] public short[] SkillExperienceMonths { get; init; } = [];
        [PgName("skill_entry_dates")] public DateTime[] SkillEntryDates { get; init; } = [];
    }
}
