// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Offer_collector.Interfaces;
using Offer_collector.Models;
namespace Offer_collector.Models.Jooble
{
    public class BottomBlock
    {
        public bool enabled { get; set; }
        public int lines { get; set; }
        public string width { get; set; }
        public int verticalSpacing { get; set; }
        public int number { get; set; }
    }

    public class Company
    {
        public bool isVerified { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public bool isContactsVerified { get; set; }
        public bool doesHaveHires { get; set; }
        public bool doesHaveManyHires { get; set; }
        public bool isActiveResponses { get; set; }
        public object logoUrl { get; set; }
    }

    public class Config
    {
        public Desktop desktop { get; set; }
        public Mobile mobile { get; set; }
    }

    public class Desktop
    {
        public bool hideForPremiumJobs { get; set; }
        public PageOptions pageOptions { get; set; }
        public TopBlock topBlock { get; set; }
        public BottomBlock bottomBlock { get; set; }
    }

    public class DteJobClickRemarketing
    {
        public string currency { get; set; }
        public string conversionLabel { get; set; }
        public string sendTo { get; set; }
        public string revenue { get; set; }
    }

    public class HighlightTag
    {
        public string name { get; set; }
        public string text { get; set; }
    }

    public class JoobleSchema : IUnificatable
    {
        public string componentName { get; set; }
        public Props props { get; set; }
        public string url { get; set; }
        public string uid { get; set; }
        public string dateCaption { get; set; }
        public DateTime? dateUpdated { get; set; }
        public string salary { get; set; }
        public object estimatedSalary { get; set; }
        public string content { get; set; }
        public string fullContent { get; set; }
        public string position { get; set; }
        public bool? isNew { get; set; }
        public bool? isPremium { get; set; }
        public bool? isEasyApply { get; set; }
        public bool? isRemoteJob { get; set; }
        public bool? isResumeRequired { get; set; }
        public bool? isAdvertLabel { get; set; }
        public bool? isFavorite { get; set; }
        public int? destination { get; set; }
        public Company company { get; set; }
        public Location location { get; set; }
        public string similarGroupId { get; set; }
        public string impressionId { get; set; }
        public object recommendId { get; set; }
        public object alreadyAppliedText { get; set; }
        public bool? hasFewApplies { get; set; }
        public bool? hasQuestions { get; set; }
        public string projectLogoUrl { get; set; }
        public object jobType { get; set; }
        public bool? isDeleted { get; set; }
        public string robots { get; set; }
        public List<Tag> tags { get; set; }
        public List<HighlightTag> highlightTags { get; set; }
        public bool? isDteJob { get; set; }
        public object serpClickValue { get; set; }
        public object matching { get; set; }
        public object fitlyJobCard { get; set; }
        public object appliesCount { get; set; }
        public int? regionId { get; set; }
        public UnifiedOfferSchema UnifiedSchema(string rawHtml = "")
        {
            throw new NotImplementedException();
        }
    }

    public class Job
    {
        public int page { get; set; }
        public List<JoobleSchema> items { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public object link { get; set; }
        public bool isWalkingDistanceFromAddress { get; set; }
        public bool isShiftJob { get; set; }
        public object coordinates { get; set; }
    }

    public class Mobile
    {
        public bool hideForPremiumJobs { get; set; }
        public PageOptions pageOptions { get; set; }
        public TopBlock topBlock { get; set; }
        public BottomBlock bottomBlock { get; set; }
    }

    public class PageOptions
    {
        public string pubId { get; set; }
        public string query { get; set; }
        public string channel { get; set; }
        public string adtest { get; set; }
        public string linkTarget { get; set; }
        public string ie { get; set; }
        public string oe { get; set; }
        public bool ivt { get; set; }
        public bool sellerRatings { get; set; }
        public bool siteLinks { get; set; }
        public bool longerHeadlines { get; set; }
        public bool clickToCall { get; set; }
        public bool detailedAttribution { get; set; }
        public string queryContext { get; set; }
        public string hl { get; set; }
        public string adsafe { get; set; }
        public string styleId { get; set; }
        public string rightHandAttribution { get; set; }
    }

    public class Props
    {
        public int page { get; set; }
        public Config config { get; set; }
        public string uniqueKey { get; set; }
        public string areaId { get; set; }
        public object position { get; set; }
        public int? id { get; set; }
    }

    public class JooblePagination
    {
        public int currentPage { get; set; }
        public int perPage { get; set; }
        public int jobsAmount { get; set; }
        public int jobsAmountWithoutRegion { get; set; }
        public string searchId { get; set; }
        public bool isLoadedJobs { get; set; }
        public bool isFailedLoadingJobs { get; set; }
        public bool isNoResultsForCurrentLocation { get; set; }
        public int numberOfPagesAtInit { get; set; }
        public List<Job> jobs { get; set; }
        public List<object> recommendedJobs { get; set; }
        public int totalCrawledSite { get; set; }
        public List<object> specifyingQueries { get; set; }
        public int jobsAmountWithoutFilters { get; set; }
        public DteJobClickRemarketing dteJobClickRemarketing { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public string text { get; set; }
        public string categoryName { get; set; }
    }

    public class TopBlock
    {
        public bool enabled { get; set; }
        public int lines { get; set; }
        public string width { get; set; }
        public int verticalSpacing { get; set; }
        public int number { get; set; }
    }

    public class JoobleSchemaWithPagination
    {
        public int currentPage { get; set; }
        public int maxElements { get; set; }
        public int perPage { get; set; }
        public List<JoobleSchema> joobleOffers { get; set; }

        public JoobleSchemaWithPagination(JooblePagination pagination, List<JoobleSchema> schemas)
        {
            currentPage = pagination.currentPage;
            maxElements = pagination.jobsAmount;
            perPage = pagination.perPage;
            joobleOffers = schemas;
        }
    }
}