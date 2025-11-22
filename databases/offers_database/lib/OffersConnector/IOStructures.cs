namespace OffersConnector
{
    internal class ExternalOfferUosInput(
        string? sourceName,
        string? sourceBaseUrl,
        string? queryString,
        string? jobTitle,
        string? companyName,
        string? companyLogoUrl,
        string? description,
        decimal? salaryFrom,
        decimal? salaryTo,
        string? currency,
        string? salaryPeriod,
        bool? isGross,
        string? buildingNumber,
        string? street,
        string? city,
        string? postalCode,
        double? latitude,
        double? longitude,
        bool? isRemote,
        bool? isHybrid,
        string? leadingCategory,
        List<string>? subCategories,
        List<string>? skills,
        List<short?>? skillsExperienceMonths,
        List<string?>? skillsExperienceLevels,
        List<string>? educationLevels,
        List<string>? languages,
        List<string>? languageLevels,
        List<string>? employmentTypes,
        List<string>? employmentSchedules,
        DateTimeOffset? published,
        DateTimeOffset? expires,
        List<string>? benefits,
        bool? isUrgent,
        bool? isForUkrainians,
        DateTimeOffset? offerLifespanExpiration
    )
    {
        public string? SourceName { get; } = sourceName;
        public string? SourceBaseUrl { get; } = sourceBaseUrl;
        public string? QueryString { get; } = queryString;
        public string? JobTitle { get; } = jobTitle;
        public string? CompanyName { get; } = companyName;
        public string? CompanyLogoUrl { get; } = companyLogoUrl;
        public string? Description { get; } = description;
        public decimal? SalaryFrom { get; } = salaryFrom;
        public decimal? SalaryTo { get; } = salaryTo;
        public string? Currency { get; } = currency;
        public string? SalaryPeriod { get; } = salaryPeriod;
        public bool? IsGross { get; } = isGross;
        public string? BuildingNumber { get; } = buildingNumber;
        public string? Street { get; } = street;
        public string? City { get; } = city;
        public string? PostalCode { get; } = postalCode;
        public double? Latitude { get; } = latitude;
        public double? Longitude { get; } = longitude;
        public bool? IsRemote { get; } = isRemote;
        public bool? IsHybrid { get; } = isHybrid;
        public string? LeadingCategory { get; } = leadingCategory;
        public List<string>? SubCategories { get; } = subCategories;
        public List<string>? Skills { get; } = skills;
        public List<short?>? SkillsExperienceMonths { get; } = skillsExperienceMonths;
        public List<string?>? SkillsExperienceLevels { get; } = skillsExperienceLevels;
        public List<string>? EducationLevels { get; } = educationLevels;
        public List<string>? Languages { get; } = languages;
        public List<string>? LanguageLevels { get; } = languageLevels;
        public List<string>? EmploymentTypes { get; } = employmentTypes;
        public List<string>? EmploymentSchedules { get; } = employmentSchedules;
        public DateTimeOffset? Published { get; } = published;
        public DateTimeOffset? Expires { get; } = expires;
        public List<string>? Benefits { get; } = benefits;
        public bool? IsUrgent { get; } = isUrgent;
        public bool? IsForUkrainians { get; } = isForUkrainians;
        public DateTimeOffset? OfferLifespanExpiration { get; } = offerLifespanExpiration;
    }

    public class BatchResult(int Idx, long? OfferId, string? Action, string? Error)
    {
        public int Idx { get; } = Idx;
        public long? OfferId { get; } = OfferId;
        public string? Action { get; } = Action;
        public string? Error { get; } = Error;
    }
}
