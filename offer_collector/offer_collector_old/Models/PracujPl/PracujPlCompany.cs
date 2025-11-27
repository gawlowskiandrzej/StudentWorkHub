namespace Offer_collector.Models.PracujPl
{
    public class AddressContract
    {
        public string street { get; set; } = string.Empty;
        public string houseNumber { get; set; } = string.Empty;
        public string apartmentNumber { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string postalCode { get; set; } = string.Empty;
        public string countryCode { get; set; } = string.Empty;
        public string voivodeship { get; set; } = string.Empty;
    }

    public class GroupedJobOffersResult
    {
        public List<GroupedOffer> groupedOffers { get; set; } = new();
        public int offersTotalCount { get; set; }
        public int groupedOffersTotalCount { get; set; }
    }

    public class GroupedOffer
    {
        public string groupId { get; set; } = string.Empty;
        public string jobTitle { get; set; } = string.Empty;
        public string companyName { get; set; } = string.Empty;
        public string companyProfileAbsoluteUri { get; set; } = string.Empty;
        public int? companyId { get; set; }
        public string companyLogoUri { get; set; } = string.Empty;
        public DateTime? lastPublicated { get; set; }
        public DateTime? expirationDate { get; set; }
        public string salaryDisplayText { get; set; } = string.Empty;
        public string jobDescription { get; set; } = string.Empty;
        public bool isSuperOffer { get; set; }
        public bool isFranchise { get; set; }
        public bool isOptionalCv { get; set; }
        public bool isOneClickApply { get; set; }
        public bool isJobiconCompany { get; set; }
        public List<Offer> offers { get; set; } = new();
        public List<string> positionLevels { get; set; } = new();
        public List<string> typesOfContract { get; set; } = new();
        public List<string> workSchedules { get; set; } = new();
        public List<string> workModes { get; set; } = new();
        public List<object> primaryAttributes { get; set; } = new();
        public object? commonOfferId { get; set; }
        public int? searchEngineRelevancyScore { get; set; }
        public object? mobileBannerUri { get; set; }
        public object? technologies { get; set; }
        public object? aboutProjectShortDescription { get; set; }
        public object? popular { get; set; }
    }

    public class Offer
    {
        public int? partitionId { get; set; }
        public string offerAbsoluteUri { get; set; } = string.Empty;
        public string displayWorkplace { get; set; } = string.Empty;
    }

    public class PhoneContract
    {
        public string directionNumber { get; set; } = string.Empty;
        public string number { get; set; } = string.Empty;
    }

    public class PracujPlCompany
    {
        public int? companyId { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string shortName { get; set; } = string.Empty;
        public string nip { get; set; } = string.Empty;
        public AddressContract addressContract { get; set; } = new();
        public PhoneContract phoneContract { get; set; } = new();
        public GroupedJobOffersResult groupedJobOffersResult { get; set; } = new();
    }
}
