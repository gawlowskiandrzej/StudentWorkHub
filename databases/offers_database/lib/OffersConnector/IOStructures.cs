namespace OffersConnector
{
    /// <summary>
    /// Immutable container for a single external offer mapped to the unified offer schema and prepared for persistence.
    /// </summary>
    /// <param name="sourceName">Logical name of the offer source.</param>
    /// <param name="sourceBaseUrl">Base URL of the external source, used to build canonical links.</param>
    /// <param name="queryString">Query or search expression that produced the offer.</param>
    /// <param name="jobTitle">Normalized job title shown to candidates.</param>
    /// <param name="companyName">Name of the employer or recruiting company.</param>
    /// <param name="companyLogoUrl">Public URL of the company logo, if provided by the source.</param>
    /// <param name="description">Sanitized offer description.</param>
    /// <param name="salaryFrom">Lower bound of the salary range in the given currency.</param>
    /// <param name="salaryTo">Upper bound of the salary range in the given currency.</param>
    /// <param name="currency">ISO-like currency code used for salary fields.</param>
    /// <param name="salaryPeriod">Salary period label (e.g. monthly, yearly) standardized upstream.</param>
    /// <param name="isGross">Indicates whether the salary values are gross (before tax).</param>
    /// <param name="buildingNumber">Building number part of the physical address, if any.</param>
    /// <param name="street">Street name part of the physical address.</param>
    /// <param name="city">City or locality of the job location.</param>
    /// <param name="postalCode">Postal or ZIP code of the job location.</param>
    /// <param name="latitude">Latitude of the job location, when geocoded.</param>
    /// <param name="longitude">Longitude of the job location, when geocoded.</param>
    /// <param name="isRemote">Flag indicating that the position can be performed fully remotely.</param>
    /// <param name="isHybrid">Flag indicating that the position is offered in a hybrid (remote/on-site) model.</param>
    /// <param name="leadingCategory">Primary job category used for classification.</param>
    /// <param name="subCategories">Additional job categories or tags used for faceting.</param>
    /// <param name="skills">List of normalized skills names required or mentioned in the offer.</param>
    /// <param name="skillsExperienceMonths">
    /// Per-skill experience requirement in months; expected to be positionally aligned with <paramref name="skills"/>.
    /// </param>
    /// <param name="skillsExperienceLevels">
    /// Per-skill experience level; expected to be positionally aligned with <paramref name="skills"/>.
    /// </param>
    /// <param name="educationLevels">List of standardized education levels relevant for the offer.</param>
    /// <param name="languages">Languages required or preferred for the role.</param>
    /// <param name="languageLevels">
    /// Proficiency levels for each language; expected to be positionally aligned with <paramref name="languages"/>.
    /// </param>
    /// <param name="employmentTypes">Employment type labels (e.g. B2B, full-time).</param>
    /// <param name="employmentSchedules">Employment schedule labels (e.g. shift work, flexible hours).</param>
    /// <param name="published">Timestamp when the offer became visible to candidates.</param>
    /// <param name="expires">Expiration timestamp of the offer as defined by the source system.</param>
    /// <param name="benefits">List of benefit names associated with the offer.</param>
    /// <param name="isUrgent">Marks the offer as urgent or prioritized in the UI.</param>
    /// <param name="isForUkrainians">Indicates that the offer is explicitly targeted at Ukrainian candidates.</param>
    /// <param name="offerLifespanExpiration">
    /// Internally computed timestamp when the offer should be automatically treated as expired.
    /// </param>
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
        List<string?>? languageLevels,
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
        public List<string?>? LanguageLevels { get; } = languageLevels;
        public List<string>? EmploymentTypes { get; } = employmentTypes;
        public List<string>? EmploymentSchedules { get; } = employmentSchedules;
        public DateTimeOffset? Published { get; } = published;
        public DateTimeOffset? Expires { get; } = expires;
        public List<string>? Benefits { get; } = benefits;
        public bool? IsUrgent { get; } = isUrgent;
        public bool? IsForUkrainians { get; } = isForUkrainians;
        public DateTimeOffset? OfferLifespanExpiration { get; } = offerLifespanExpiration;
    }

    /// <summary>
    /// Represents the outcome of processing a single item within a batch operation.
    /// </summary>
    /// <param name="Idx">Zero-based index of the item in the original input batch.</param>
    /// <param name="OfferId">Identifier of the offer affected in the database, if any.</param>
    /// <param name="Action">Short action label describing what happened (e.g. "insert", "update").</param>
    /// <param name="Error">Error message when processing failed; null when the operation succeeded.</param>
    public class BatchResult(int Idx, long? OfferId, string? Action, string? Error)
    {
        public int Idx { get; } = Idx;
        public long? OfferId { get; } = OfferId;
        public string? Action { get; } = Action;
        public string? Error { get; } = Error;
    }
}
