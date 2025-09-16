using Newtonsoft.Json;

namespace Offer_collector.Models.JustJoinIt
{
    public class Address
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string addressCountry { get; set; }
        public string addressLocality { get; set; }
        public string addressRegion { get; set; }
        public string postalCode { get; set; }
        public string streetAddress { get; set; }
    }

    public class ApplicantLocationRequirements
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
    }

    public class BaseSalary
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string currency { get; set; }
        public Value value { get; set; }
    }

    public class HiringOrganization
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string sameAs { get; set; }
        public string logo { get; set; }
    }

    public class JobLocation
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public Address address { get; set; }
    }

    public class JustJoinItDescription
    {
        [JsonProperty("@context")]
        public string context { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }
        public DateTime? datePosted { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public DateTime? validThrough { get; set; }
        public string employmentType { get; set; }
        public BaseSalary baseSalary { get; set; }
        public HiringOrganization hiringOrganization { get; set; }
        public JobLocation jobLocation { get; set; }
        public ApplicantLocationRequirements applicantLocationRequirements { get; set; }
    }

    public class Value
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string unitText { get; set; }
        public int? minValue { get; set; }
        public int? maxValue { get; set; }
    }
}
