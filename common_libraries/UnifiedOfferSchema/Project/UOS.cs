#nullable enable
using System.Text.Json.Serialization;

namespace UnifiedOfferSchema
{
    /// <summary>
    /// Root data contract for the Unified Offer Schema (UOS).
    /// Maps JSON job-offer documents to strongly typed .NET classes using System.Text.Json.
    /// </summary>
    /// <remarks>
    /// Properties use <see cref="JsonPropertyNameAttribute"/> and <see cref="JsonRequiredAttribute"/> to control field names and requiredness.
    /// Numeric members marked with <see cref="JsonNumberHandlingAttribute"/> allow string input (AllowReadingFromString).
    /// Nullability is explicit via C# nullable context; consumers should validate business-required fields at runtime.
    /// This type is a schema/model only (no behavior); serialization errors are surfaced by callers <see cref="UOSUtils"/>.
    /// </remarks>
    public sealed class UOS
    {
        [JsonPropertyName("id")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        [JsonRequired]
        public long? Id { get; set; }

        [JsonPropertyName("source")]
        [JsonRequired]
        public string Source { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        [JsonRequired]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("jobTitle")]
        [JsonRequired]
        public string JobTitle { get; set; } = string.Empty;

        [JsonPropertyName("company")]
        [JsonRequired]
        public Company Company { get; set; } = new();

        [JsonPropertyName("description")]
        [JsonRequired]
        public string? Description { get; set; }

        [JsonPropertyName("salary")]
        [JsonRequired]
        public Salary Salary { get; set; } = new();

        [JsonPropertyName("location")]
        [JsonRequired]
        public Location Location { get; set; } = new();

        [JsonPropertyName("category")]
        [JsonRequired]
        public Category Category { get; set; } = new();

        [JsonPropertyName("requirements")]
        [JsonRequired]
        public Requirements Requirements { get; set; } = new();

        [JsonPropertyName("employment")]
        [JsonRequired]
        public Employment Employment { get; set; } = new();

        [JsonPropertyName("dates")]
        [JsonRequired]
        public Dates Dates { get; set; } = new();

        [JsonPropertyName("benefits")]
        [JsonRequired]
        public List<string>? Benefits { get; set; }

        [JsonPropertyName("isUrgent")]
        [JsonRequired]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("isForUkrainians")]
        [JsonRequired]
        public bool IsForUkrainians { get; set; }
    }

    public sealed class Company
    {
        [JsonPropertyName("name")]
        [JsonRequired]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("logoUrl")]
        [JsonRequired]
        public string? LogoUrl { get; set; }
    }

    public sealed class Salary
    {
        [JsonPropertyName("from")]
        [JsonRequired]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? From { get; set; }

        [JsonPropertyName("to")]
        [JsonRequired]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? To { get; set; }

        [JsonPropertyName("currency")]
        [JsonRequired]
        public string? Currency { get; set; }

        [JsonPropertyName("period")]
        [JsonRequired]
        public string? Period { get; set; }

        [JsonPropertyName("type")]
        [JsonRequired]
        public string? Type { get; set; }
    }

    public sealed class Location
    {
        [JsonPropertyName("buildingNumber")]
        [JsonRequired]
        public string? BuildingNumber { get; set; }

        [JsonPropertyName("street")]
        [JsonRequired]
        public string? Street { get; set; }

        [JsonPropertyName("city")]
        [JsonRequired]
        public string? City { get; set; }

        [JsonPropertyName("postalCode")]
        [JsonRequired]
        public string? PostalCode { get; set; }

        [JsonPropertyName("coordinates")]
        [JsonRequired]
        public Coordinates? Coordinates { get; set; }

        [JsonPropertyName("isRemote")]
        [JsonRequired]
        public bool? IsRemote { get; set; }

        [JsonPropertyName("isHybrid")]
        [JsonRequired]
        public bool? IsHybrid { get; set; }
    }

    public sealed class Coordinates
    {
        [JsonPropertyName("latitude")]
        [JsonRequired]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        [JsonRequired]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public double? Longitude { get; set; }
    }

    public sealed class Category
    {
        [JsonPropertyName("leadingCategory")]
        [JsonRequired]
        public string LeadingCategory { get; set; } = string.Empty;

        [JsonPropertyName("subCategories")]
        [JsonRequired]
        public List<string>? SubCategories { get; set; }
    }

    public sealed class Requirements
    {
        [JsonPropertyName("skills")]
        [JsonRequired]
        public List<Skills>? Skills { get; set; }

        [JsonPropertyName("education")]
        [JsonRequired]
        public List<string>? Education { get; set; }

        [JsonPropertyName("languages")]
        [JsonRequired]
        public List<Languages>? Languages { get; set; }
    }

    public sealed class Skills
    {
        [JsonPropertyName("skill")]
        [JsonRequired]
        public string Skill { get; set; } = string.Empty;

        [JsonPropertyName("experienceMonths")]
        [JsonRequired]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? ExperienceMonths { get; set; }

        [JsonPropertyName("experienceLevel")]
        [JsonRequired]
        public List<string>? ExperienceLevel { get; set; }
    }

    public sealed class Languages
    {
        [JsonPropertyName("language")]
        [JsonRequired]
        public string Language{ get; set; } = string.Empty;

        [JsonPropertyName("level")]
        [JsonRequired]
        public string Level { get; set; } = string.Empty;
    }

    public sealed class Employment
    {
        [JsonPropertyName("types")]
        [JsonRequired]
        public List<string> Types { get; set; } = [];

        [JsonPropertyName("schedules")]
        [JsonRequired]
        public List<string> Schedules { get; set; } = [];
    }

    public sealed class Dates
    {
        [JsonPropertyName("published")]
        [JsonRequired]
        public string Published { get; set; } = string.Empty;

        [JsonPropertyName("expires")]
        [JsonRequired]
        public string? Expires { get; set; }
    }
}
