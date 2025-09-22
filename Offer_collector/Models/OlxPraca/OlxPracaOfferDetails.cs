namespace Offer_collector.Models.OlxPraca
{
    public class PriceFormatConfig
    {
        public string decimalSeparator { get; set; }
        public string thousandsSeparator { get; set; }
    }

    public class OlxPracaOfferDetails
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Category category { get; set; }
        public Map map { get; set; }
        public bool? isBusiness { get; set; }
        public string url { get; set; }
        public bool? isHighlighted { get; set; }
        public bool? isPromoted { get; set; }
        public Promotion promotion { get; set; }
        public object externalUrl { get; set; }
        public Delivery delivery { get; set; }
        public DateTime? createdTime { get; set; }
        public DateTime? lastRefreshTime { get; set; }
        public DateTime? pushupTime { get; set; }
        public DateTime? validToTime { get; set; }
        public bool? isActive { get; set; }
        public string status { get; set; }
        public List<Param> @params { get; set; }
        public string itemCondition { get; set; }
        public object price { get; set; }
        public Salary salary { get; set; }
        public Partner partner { get; set; }
        public bool? isJob { get; set; }
        public List<object> photos { get; set; }
        public List<object> photosSet { get; set; }
        public Location location { get; set; }
        public string urlPath { get; set; }
        public Contact contact { get; set; }
        public User user { get; set; }
        public Shop shop { get; set; }
        public Safedeal safedeal { get; set; }
        public string searchReason { get; set; }
        public bool? isNewFavouriteAd { get; set; }
    }
    public class Salary
    {
        public double? from { get; set; }
        public double? to { get; set; }
        public string currencyCode { get; set; }
        public string currencySymbol { get; set; }
        public string period { get; set; }
        public PriceFormatConfig priceFormatConfig { get; set; }
        public bool? negotiable { get; set; }
        public string label { get; set; }
    }
}
