namespace Offer_collector.Models.PracujPl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string zipCode;
        public string street;
        public string buildingNumber;
        public string flatNumber;
        public string additionalInfo;
        public string district;
        public Coordinates coordinates;
    }

    public class Applying
    {
        public string referenceNumber;
        public string applyUrl;
        public ApplyProxy applyProxy;
        public bool? oneClickApply;
        public object contactPhone;
        public bool? remoteRecruitment;
        public object strefa;
        public object ats;
        public ERecruiter eRecruiter;
        public object letter;
        public FormBranding formBranding;
    }

    public class ApplyProxy
    {
        public string applyUrl;
        public bool? multiApplyingEnabled;
        public List<object> multiApplying;
    }

    public class Attributes
    {
        public string jobTitle;
        public string description;
        public bool? isArchive;
        public bool? isWithdrawn;
        public string offerAbsoluteUrl;
        public string displayEmployerName;
        public List<Category> categories;
        public LeadingCategory leadingCategory;
        public Applying applying;
        public List<Workplace> workplaces;
        public Employment employment;
        public InformationClause informationClause;
    }

    public class BackgroundImage
    {
        public string pracujPlBasicSizeUrl;
    }

    public class Banner
    {
        public string mobileBasicSizeUrl;
        public string pracujPlBasicSizeUrl;
    }

    public class Category
    {
        public int? id;
        public string name;
        public Parent parent;
    }

    public class Coordinates
    {
        public double? latitude;
        public double? longitude;
    }

    public class Country
    {
        public int? id;
        public string name;
        public string pracujPlName;
    }

    public class CustomItem
    {
        public string name;
        public Icon icon;
    }

    public class DesktopListingBanner
    {
        public bool? enabled;
        public Banner banner;
    }

    public class Employment
    {
        public List<PositionLevel> positionLevels;
        public bool? entirelyRemoteWork;
        public List<WorkSchedule> workSchedules;
        public List<TypesOfContract> typesOfContracts;
        public List<WorkMode> workModes;
    }

    public class ERecruiter
    {
        public bool? optionalCv;
        public string formUrl;
        public string formMode;
        public List<object> multiApplying;
    }

    public class FontColor
    {
        public string hex;
    }

    public class FormBranding
    {
        public bool? enabled;
        public Logo logo;
        public Banner banner;
    }

    public class Icon
    {
        public string rasterUrl;
        public string vectorUrl;
    }

    public class Id
    {
        public string type;
        public int? number;
    }

    public class InformationClause
    {
        public string shortText;
        public string longText;
    }

    public class InlandLocation
    {
        public Location location;
        public Address address;
    }

    public class Item
    {
        public string code;
        public string name;
        public Icon icon;
        public string pracujPlName;
        public string primaryTargetSiteName;
        public string mediaType;
        public string url;
        public string thumbnailUrl;
        public object grubberId;
    }

    public class JobOfferLanguage
    {
        public string isoCode;
    }

    public class Label
    {
        public string text;
        public string pracujPlText;
        public string primaryTargetSiteText;
    }

    public class LeadingCategory
    {
        public int? id;
        public string name;
        public Parent parent;
    }

    public class ListingLogo
    {
        public bool? enabled;
        public Logo logo;
    }

    public class Location
    {
        public int? id;
        public string name;
    }

    public class Logo
    {
        public string pracujPlBasicSizeUrl;
    }

    public class MobileListingBanner
    {
        public bool? enabled;
        public Banner banner;
    }

    public class Model
    {
        public string modelType;
        public string dictionaryName;
        public List<CustomItem> customItems;
        public List<Item> items;
        public List<string> bullets;
        public List<string> paragraphs;
    }

    public class OfferViewLogo
    {
        public string pracujPlBasicSizeUrl;
    }

    public class Parent
    {
        public int? id;
        public string name;
    }

    public class PositionLevel
    {
        public int? id;
        public string name;
        public string pracujPlName;
    }

    public class ProductAddons
    {
        public SuperOption superOption;
        public ListingLogo listingLogo;
        public MobileListingBanner mobileListingBanner;
        public RwdListingBanner rwdListingBanner;
        public SearchingInWholePoland searchingInWholePoland;
        public DesktopListingBanner desktopListingBanner;
    }

    public class PublicationDetails
    {
        public string jobOfferVersion;
        public DateTime? dateOfInitialPublicationUtc;
        public DateTime? lastPublishedUtc;
        public DateTime? expirationDateUtc;
        public DateTime? expirationDateTimeUtc;
        public string jobOfferUrlSegment;
        public bool? isActive;
    }

    public class Region
    {
        public int? id;
        public string name;
        public string pracujPlName;
    }

    public class PracujPlOfferDetails
    {
        public int? jobOfferWebId;
        public int? employerId;
        public JobOfferLanguage jobOfferLanguage;
        public PublicationDetails publicationDetails;
        public ProductAddons productAddons;
        public Attributes attributes;
        public List<Section> sections;
        public Style style;
        public string groupId;
        public string partitionKey;
        public List<TextSection> textSections;
        public List<object> primaryAttributes;
        public List<SecondaryAttribute> secondaryAttributes;
        public bool? isDirectlyFromEmployer;
        public int? appType;
        public string offerURLName;
    }

    public class RwdListingBanner
    {
        public bool? enabled;
        public Banner banner;
    }

    public class SearchingInWholePoland
    {
        public bool? enabled;
    }

    public class SecondaryAttribute
    {
        public string code;
        public Label label;
        public Model model;
    }

    public class Section
    {
        public string sectionType;
        public int? number;
        public string title;
        public object header;
        public List<SubSection> subSections;
        public Model model;
        public Id id;
    }

    public class Style
    {
        public List<BackgroundImage> backgroundImages;
        public ThemeColor themeColor;
        public OfferViewLogo offerViewLogo;
        public FontColor fontColor;
        public object sectionsBackground;
    }

    public class SubSection
    {
        public string sectionType;
        public int? number;
        public string title;
        public object header;
        public object subSections;
        public Model model;
        public Id id;
    }

    public class SuperOption
    {
        public bool? enabled;
    }

    public class TextSection
    {
        public string sectionType;
        public string plainText;
        public List<string> textElements;
    }

    public class ThemeColor
    {
        public string hex;
    }

    public class TypesOfContract
    {
        public int? id;
        public string name;
        public string pracujPlName;
        public object salary;
    }

    public class WorkMode
    {
        public string description;
        public string code;
        public string name;
        public string pracujPlName;
    }

    public class Workplace
    {
        public object abroadAddress;
        public bool? isAbroad;
        public Country country;
        public Region region;
        public InlandLocation inlandLocation;
        public string displayAddress;
    }

    public class WorkSchedule
    {
        public int? id;
        public string name;
        public string pracujPlName;
    }


}
