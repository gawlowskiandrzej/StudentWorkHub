using Offer_collector.Interfaces;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.JustJoinIt
{
    public class EmploymentType
    {
        public decimal? from { get; set; }
        public decimal? to { get; set; }
        public string? currency { get; set; }
        public string? type { get; set; }
        public string? unit { get; set; }
        public bool? gross { get; set; }
        public decimal? fromChf { get; set; }
        public decimal? fromEur { get; set; }
        public decimal? fromGbp { get; set; }
        public decimal? fromPln { get; set; }
        public decimal? fromUsd { get; set; }
        public decimal? toChf { get; set; }
        public decimal? toEur { get; set; }
        public decimal? toGbp { get; set; }
        public decimal? toPln { get; set; }
        public decimal? toUsd { get; set; }
    }

    public class Multilocation
    {
        public string city { get; set; }
        public string slug { get; set; }
        public string street { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class JustJoinItSchema : IUnificatable
    {
        public string? guid { get; set; }
        public string? slug { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public List<string>? requiredSkills { get; set; }
        public object? niceToHaveSkills { get; set; }
        public string? workplaceType { get; set; }
        public string? workingTime { get; set; }
        public string? experienceLevel { get; set; }
        public List<EmploymentType>? employmentTypes { get; set; }
        public int categoryId { get; set; }
        public List<Multilocation>? multilocation { get; set; }
        public string? city { get; set; }
        public string? street { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool remoteInterview { get; set; }
        public string? companyName { get; set; }
        public string? companyLogoThumbUrl { get; set; }
        public DateTime publishedAt { get; set; }
        public DateTime expiredAt { get; set; }
        public bool openToHireUkrainians { get; set; }
        public List<Language?>? languages { get; set; }

        public JustJoinItOfferDetails details { get; set; }  
        public string? detailsHtml{ get; set; }  

        public UnifiedOfferSchema UnifiedSchema(string rawHtml = "")
        {
            UnifiedOfferSchema unif = new UnifiedOfferSchema();
            unif.source = OfferSitesTypes.Justjoinit;
            unif.url = JustJoinItBuilder.baseUrlOfferDetail + slug;
            unif.jobTitle = title;
            unif.company = new Company
            {
                logoUrl = companyLogoThumbUrl,
                name = companyName
            };
            unif.description = description;

            // Requirements z Ai

            // TODO unifikacja wartości pól np. gdzieś może być Polska a tutaj pl
            List<LanguageSkill> languageList = new List<LanguageSkill>();

            foreach (Language lang in languages ?? new List<Language?>())
                languageList.Add(new LanguageSkill { level = lang?.level, language = lang?.code });

            // TODO make skill recoqnition
            unif.requirements = new Requirements
            {
                languages = languageList
            };
            EmploymentType justSalary = employmentTypes?.FirstOrDefault() ?? new EmploymentType();
            unif.salary = new Salary
            {
                currency = justSalary.currency,
                from = justSalary.from ?? 0,
                to = justSalary.to ?? 0,
                period = justSalary.unit,
                type = "brutto",
            };
            unif.location = new Location
            {
                city = city,
                street = street,
                isHybrid = workplaceType?.Contains("hybrid") ?? false,
                isRemote = workplaceType?.Contains("remote") ?? false,
                coordinates = new Coordinates
                {
                    latitude = latitude,
                    longitude = longitude
                }
            };
            unif.employment = new Employment
            {
                schedules = new List<string> { workingTime ?? "fulltime" },
                types = new List<string> { justSalary.type ?? "" }
            };
            unif.dates = new Dates
            {
                expires = expiredAt,
                published = publishedAt
            };
            unif.category = new Models.Category
            {
                subCategories = new List<string> { details.category?.name ?? "None" },
                leadingCategory = details.category?.name ?? "None",
            };
            // TODO check this field
            unif.isUrgent = false;
            unif.isForUkrainians = openToHireUkrainians;

            return unif;
        }
        
    }
    public class Language
    {
        public string? code;
        public string? level;
    }

}