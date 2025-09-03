// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Offer_collector.Models.JustJoinIt
{
    public class EmploymentType
    {
        public int from { get; set; }
        public int to { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public bool gross { get; set; }
        public double fromChf { get; set; }
        public double fromEur { get; set; }
        public double fromGbp { get; set; }
        public int fromPln { get; set; }
        public double fromUsd { get; set; }
        public double toChf { get; set; }
        public double toEur { get; set; }
        public double toGbp { get; set; }
        public int toPln { get; set; }
        public double toUsd { get; set; }
    }

    public class Multilocation
    {
        public string city { get; set; }
        public string slug { get; set; }
        public string street { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class JustJoinItSchema
    {
        public string guid { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public List<string> requiredSkills { get; set; }
        public object niceToHaveSkills { get; set; }
        public string workplaceType { get; set; }
        public string workingTime { get; set; }
        public string experienceLevel { get; set; }
        public List<EmploymentType> employmentTypes { get; set; }
        public int categoryId { get; set; }
        public List<Multilocation> multilocation { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool remoteInterview { get; set; }
        public string companyName { get; set; }
        public string companyLogoThumbUrl { get; set; }
        public DateTime publishedAt { get; set; }
        public DateTime expiredAt { get; set; }
        public bool openToHireUkrainians { get; set; }
        public List<Language?>? languages { get; set; }
    }
    public class Language
    {
        public string? code;
        public string? level;
    }

}