using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Offer_collector.Models.Tools;

namespace Offer_collector.Models
{
    public class Company
    {
        public string name { get; set; } = "";
        public string logoUrl { get; set; } = "";
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Dates
    {
        [JsonProperty("published")]
        [JsonConverter(typeof(CustomDateTimeConverter2))]
        public DateTime? published { get; set; }

        [JsonProperty("expires")]
        [JsonConverter(typeof(CustomDateTimeConverter2))]
        public DateTime? expires { get; set; }
    }

    public class Employment
    {
        public List<string?>? types { get; set; } // Future enum type umowa o prace, umowa zlecenie
        public List<string?>? schedules { get; set; } // Future enum type full time, partTime
    }

    public class Location
    {
        public string? buildingNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? postalCode { get; set; }
        public Coordinates? coordinates { get; set; }
        public bool isRemote { get; set; }
        public bool isHybrid { get; set; }
    }
    public class LanguageSkill
    {
        public string? language { get; set; }
        public string? level{ get; set; }
    }
    public class Skill
    {
        public string? skill { get; set; }
        public int? experienceMonths { get; set; }
        public List<string> experienceLevel { get; set; } = new List<string>();
    }
    public class Requirements
    {
        public List<Skill> skills { get; set; } = new List<Skill>();
        public List<string> education { get; set; } = new List<string>();
        public List<LanguageSkill> languages { get; set; } = new List<LanguageSkill>();
    }
    public class Category
    {
        public string? leadingCategory { get; set; }
        public List<string?> subCategories { get; set; } = new List<string?>();
    }
    public class UnifiedOfferSchemaClass
    {
        public int id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OfferSitesTypes source { get; set; } // Pracujpl / olx ...
        public string? url { get; set; } // redirect url to source of offer
        public string? jobTitle { get; set; }
        public Company company { get; set; } = new Company();
        public string? description { get; set; }
        public Salary salary { get; set; } = new Salary();
        public Location location { get; set; } = new Location();
        public Category category { get; set; } = new Category();
        public Requirements requirements { get; set; } = new Requirements();
        public Employment employment { get; set; } = new Employment();
        public Dates? dates { get; set; }
        
        public List<string>? benefits { get; set; }
        public bool isUrgent { get; set; } = false;
        public bool isForUkrainians { get; set; } = true;
        //TODO LISTA POMYSŁÓW UWAG
        // pole które będzie wskazywać na link do formularza z e-recruiter zeby można było odesłać użytkownika z html lub z pola json jak się da
    }

    public class Salary
    {
        [JsonConverter(typeof(DecimalDefaultConverter))]
        public decimal from { get; set; }
        [JsonConverter(typeof(DecimalDefaultConverter))]
        public decimal to { get; set; }
        public string? currency { get; set; }
        public string? period { get; set; } // Future enum type montly/weekly/daily
        public string? type { get; set; } // future enum type gross/net
    }

    public class Output
    {
        public UnifiedOfferSchemaClass? unified;
        public string? aiGenerated;
    }
}
