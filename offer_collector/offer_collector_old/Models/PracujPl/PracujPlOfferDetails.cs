#nullable enable
namespace Offer_collector.Models.PracujPl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string? zipCode { get; set; }
        public string? street { get; set; }
        public string? buildingNumber { get; set; }
        public string? flatNumber { get; set; }
        public string? additionalInfo { get; set; }
        public string? district { get; set; }
        public Coordinates? coordinates { get; set; }
    }

    public class Applying
    {
        public string? referenceNumber { get; set; }
        public string? applyUrl { get; set; }
        public ApplyProxy? applyProxy { get; set; }
        public bool? oneClickApply { get; set; }
        public object? contactPhone { get; set; }
        public bool? remoteRecruitment { get; set; }
        public object? strefa { get; set; }
        public object? ats { get; set; }
        public ERecruiter? eRecruiter { get; set; }
        public object? letter { get; set; }
        public FormBranding? formBranding { get; set; }
    }

    public class ApplyProxy
    {
        public string? applyUrl { get; set; }
        public bool? multiApplyingEnabled { get; set; }
        public List<object>? multiApplying { get; set; }
    }

    public class Attributes
    {
        public string? jobTitle { get; set; }
        public string? description { get; set; }
        public bool? isArchive { get; set; }
        public bool? isWithdrawn { get; set; }
        public string? offerAbsoluteUrl { get; set; }
        public string? displayEmployerName { get; set; }
        public List<Category>? categories { get; set; }
        public LeadingCategory? leadingCategory { get; set; }
        public Applying? applying { get; set; }
        public List<Workplace>? workplaces { get; set; }
        public Employment? employment { get; set; }
        public InformationClause? informationClause { get; set; }
    }

    public class BackgroundImage
    {
        public string? pracujPlBasicSizeUrl { get; set; }
    }

    public class Banner
    {
        public string? mobileBasicSizeUrl { get; set; }
        public string? pracujPlBasicSizeUrl { get; set; }
    }

    public class Category
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public Parent? parent { get; set; }
    }

    public class Coordinates
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class Country
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
    }

    public class CustomItem
    {
        public string name { get; set; } = "";
        public Icon? icon { get; set; }
    }

    public class DesktopListingBanner
    {
        public bool? enabled { get; set; }
        public Banner? banner { get; set; }
    }

    public class Employment
    {
        public List<PositionLevel>? positionLevels { get; set; }
        public bool? entirelyRemoteWork { get; set; }
        public List<WorkSchedule>? workSchedules { get; set; }
        public List<TypesOfContract>? typesOfContracts { get; set; }
        public List<WorkMode>? workModes { get; set; }
    }

    public class ERecruiter
    {
        public bool? optionalCv { get; set; }
        public string? formUrl { get; set; }
        public string? formMode { get; set; }
        public List<object>? multiApplying { get; set; }
    }

    public class FontColor
    {
        public string? hex { get; set; }
    }

    public class FormBranding
    {
        public bool? enabled { get; set; }
        public Logo? logo { get; set; }
        public Banner? banner { get; set; }
    }

    public class Icon
    {
        public string? rasterUrl { get; set; }
        public string? vectorUrl { get; set; }
    }

    public class Id
    {
        public string? type { get; set; }
        public int? number { get; set; }
    }

    public class InformationClause
    {
        public string? shortText { get; set; }
        public string? longText { get; set; }
    }

    public class InlandLocation
    {
        public Location? location { get; set; }
        public Address? address { get; set; }
    }

    public class Item
    {
        public string? code { get; set; }
        public string name { get; set; } = "";
        public Icon? icon { get; set; }
        public string? pracujPlName { get; set; }
        public string? primaryTargetSiteName { get; set; }
        public string? mediaType { get; set; }
        public string? url { get; set; }
        public string? thumbnailUrl { get; set; }
        public object? grubberId { get; set; }
    }

    public class JobOfferLanguage
    {
        public string? isoCode { get; set; }
    }

    public class Label
    {
        public string? text { get; set; }
        public string? pracujPlText { get; set; }
        public string? primaryTargetSiteText { get; set; }
    }

    public class LeadingCategory
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public Parent? parent { get; set; }
    }

    public class ListingLogo
    {
        public bool? enabled { get; set; }
        public Logo? logo { get; set; }
    }

    public class Location
    {
        public int? id { get; set; }
        public string? name { get; set; }
    }

    public class Logo
    {
        public string? pracujPlBasicSizeUrl { get; set; }
    }

    public class MobileListingBanner
    {
        public bool? enabled { get; set; }
        public Banner? banner { get; set; }
    }

    public class Model
    {
        public string? modelType { get; set; }
        public string? dictionaryName { get; set; }
        public List<CustomItem>? customItems { get; set; }
        public List<Item>? items { get; set; }
        public List<string>? bullets { get; set; }
        public List<string>? paragraphs { get; set; }
    }

    public class OfferViewLogo
    {
        public string? pracujPlBasicSizeUrl { get; set; }
    }

    public class Parent
    {
        public int? id { get; set; }
        public string? name { get; set; }
    }

    public class PositionLevel
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
    }

    public class ProductAddons
    {
        public SuperOption? superOption { get; set; }
        public ListingLogo? listingLogo { get; set; }
        public MobileListingBanner? mobileListingBanner { get; set; }
        public RwdListingBanner? rwdListingBanner { get; set; }
        public SearchingInWholePoland? searchingInWholePoland { get; set; }
        public DesktopListingBanner? desktopListingBanner { get; set; }
    }

    public class PublicationDetails
    {
        public string? jobOfferVersion { get; set; }
        public DateTime? dateOfInitialPublicationUtc { get; set; }
        public DateTime? lastPublishedUtc { get; set; }
        public DateTime? expirationDateUtc { get; set; }
        public DateTime? expirationDateTimeUtc { get; set; }
        public string? jobOfferUrlSegment { get; set; }
        public bool? isActive { get; set; }
    }

    public class Region
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
    }

    public class PracujPlOfferDetails
    {
        public int? jobOfferWebId { get; set; }
        public int? employerId { get; set; }
        public JobOfferLanguage? jobOfferLanguage { get; set; }
        public PublicationDetails? publicationDetails { get; set; }
        public ProductAddons? productAddons { get; set; }
        public Attributes? attributes { get; set; }
        public List<Section>? sections { get; set; }
        public Style? style { get; set; }
        public string? groupId { get; set; }
        public string? partitionKey { get; set; }
        public List<TextSection>? textSections { get; set; }
        public List<object>? primaryAttributes { get; set; }
        public List<SecondaryAttribute>? secondaryAttributes { get; set; }
        public bool? isDirectlyFromEmployer { get; set; }
        public int? appType { get; set; }
        public string? offerURLName { get; set; }
    }

    public class RwdListingBanner
    {
        public bool? enabled { get; set; }
        public Banner? banner { get; set; }
    }

    public class SearchingInWholePoland
    {
        public bool? enabled { get; set; }
    }

    public class SecondaryAttribute
    {
        public string? code { get; set; }
        public Label? label { get; set; }
        public Model? model { get; set; }
    }

    public class Section
    {
        public string sectionType { get; set; } = "";
        public int? number { get; set; }
        public string? title { get; set; }
        public object? header { get; set; }
        public List<SubSection>? subSections { get; set; }
        public Model? model { get; set; }
        public Id? id { get; set; }
    }

    public class Style
    {
        public List<BackgroundImage>? backgroundImages { get; set; }
        public ThemeColor? themeColor { get; set; }
        public OfferViewLogo? offerViewLogo { get; set; }
        public FontColor? fontColor { get; set; }
        public object? sectionsBackground { get; set; }
    }

    public class SubSection
    {
        public string? sectionType { get; set; }
        public int? number { get; set; }
        public string? title { get; set; }
        public object? header { get; set; }
        public object? subSections { get; set; }
        public Model? model { get; set; }
        public Id? id { get; set; }
    }

    public class SuperOption
    {
        public bool? enabled { get; set; }
    }

    public class TextSection
    {
        public string? sectionType { get; set; }
        public string? plainText { get; set; }
        public List<string>? textElements { get; set; }
    }

    public class ThemeColor
    {
        public string? hex { get; set; }
    }

    public class TypesOfContract
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
        public object? salary { get; set; }
    }

    public class WorkMode
    {
        public string? description { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
    }

    public class Workplace
    {
        public object? abroadAddress { get; set; }
        public bool? isAbroad { get; set; }
        public Country? country { get; set; }
        public Region? region { get; set; }
        public InlandLocation? inlandLocation { get; set; }
        public string? displayAddress { get; set; }
    }

    public class WorkSchedule
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? pracujPlName { get; set; }
    }
}
