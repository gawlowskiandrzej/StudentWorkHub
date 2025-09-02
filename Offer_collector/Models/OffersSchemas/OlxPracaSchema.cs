using System;

namespace OlxPraca
{
    public class Category
    {
        public int? id { get; set; }
        public string type { get; set; }
        public string _nodeId { get; set; }
    }

    public class Contact
    {
        public bool? chat { get; set; }
        public bool? courier { get; set; }
        public string name { get; set; }
        public bool? negotiation { get; set; }
        public bool? phone { get; set; }
    }

    public class Delivery
    {
        public Rock rock { get; set; }
    }

    public class Location
    {
        public string cityName { get; set; }
        public int? cityId { get; set; }
        public string cityNormalizedName { get; set; }
        public string regionName { get; set; }
        public int? regionId { get; set; }
        public string regionNormalizedName { get; set; }
        public string districtName { get; set; }
        public int? districtId { get; set; }
        public string pathName { get; set; }
    }

    public class Map
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int? radius { get; set; }
        public bool? show_detailed { get; set; }
        public int? zoom { get; set; }
    }

    public class Param
    {
        public string key { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public object normalizedValue { get; set; }
    }

    public class Partner
    {
        public string code { get; set; }
    }

    public class Promotion
    {
        public bool? highlighted { get; set; }
        public bool? top_ad { get; set; }
        public List<string> options { get; set; }
        public bool? premium_ad_page { get; set; }
        public bool? urgent { get; set; }
        public bool? b2c_ad_page { get; set; }
    }

    public class Rock
    {
        public bool? active { get; set; }
        public string mode { get; set; }
        public object offer_id { get; set; }
    }

    public class OlxPracaSchema
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
        public object pushupTime { get; set; }
        public DateTime? validToTime { get; set; }
        public bool? isActive { get; set; }
        public string status { get; set; }
        public List<Param> @params { get; set; }
        public string itemCondition { get; set; }
        public object price { get; set; }
        public object salary { get; set; }
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

    public class Safedeal
    {
        public List<object> allowed_quantity { get; set; }
        public int? weight_grams { get; set; }
    }

    public class Shop
    {
        public string subdomain { get; set; }
    }

    public class User
    {
        public int? id { get; set; }
        public string name { get; set; }
        public object photo { get; set; }
        public string logo { get; set; }
        public bool? otherAdsEnabled { get; set; }
        public object socialNetworkAccountType { get; set; }
        public bool? isOnline { get; set; }
        public DateTime? lastSeen { get; set; }
        public string about { get; set; }
        public string bannerDesktopURL { get; set; }
        public string logo_ad_page { get; set; }
        public string company_name { get; set; }
        public DateTime? created { get; set; }
        public object sellerType { get; set; }
        public string uuid { get; set; }
    }
}
