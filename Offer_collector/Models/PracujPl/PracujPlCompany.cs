namespace Offer_collector.Models.PracujPl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressContract
    {
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string apartmentNumber { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public string voivodeship { get; set; }
    }

    public class GroupedJobOffersResult
    {
        public List<GroupedOffer> groupedOffers { get; set; }
        public int offersTotalCount { get; set; }
        public int groupedOffersTotalCount { get; set; }
    }

    public class GroupedOffer
    {
        public string groupId { get; set; }
        public string jobTitle { get; set; }
        public string companyName { get; set; }
        public string companyProfileAbsoluteUri { get; set; }
        public int companyId { get; set; }
        public string companyLogoUri { get; set; }
        public DateTime lastPublicated { get; set; }
        public DateTime expirationDate { get; set; }
        public string salaryDisplayText { get; set; }
        public string jobDescription { get; set; }
        public bool isSuperOffer { get; set; }
        public bool isFranchise { get; set; }
        public bool isOptionalCv { get; set; }
        public bool isOneClickApply { get; set; }
        public bool isJobiconCompany { get; set; }
        public List<Offer> offers { get; set; }
        public List<string> positionLevels { get; set; }
        public List<string> typesOfContract { get; set; }
        public List<string> workSchedules { get; set; }
        public List<string> workModes { get; set; }
        public List<object> primaryAttributes { get; set; }
        public object commonOfferId { get; set; }
        public int searchEngineRelevancyScore { get; set; }
        public object mobileBannerUri { get; set; }
        public object technologies { get; set; }
        public object aboutProjectShortDescription { get; set; }
        public object popular { get; set; }
    }

    public class Offer
    {
        public int partitionId { get; set; }
        public string offerAbsoluteUri { get; set; }
        public string displayWorkplace { get; set; }
    }

    public class PhoneContract
    {
        public string directionNumber { get; set; }
        public string number { get; set; }
    }

    public class PracujPlCompany
    {
        public int companyId { get; set; }
        public string fullName { get; set; }
        public string shortName { get; set; }
        public string nip { get; set; }
        public AddressContract addressContract { get; set; }
        public PhoneContract phoneContract { get; set; }
        public GroupedJobOffersResult groupedJobOffersResult { get; set; }
    }


}
