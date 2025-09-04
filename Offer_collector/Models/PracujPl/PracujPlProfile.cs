using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer_collector.Models.PracujPl
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AboutUs
    {
        public string description { get; set; }
        public Media media { get; set; }
        public string title { get; set; }
    }

    public class Advantages
    {
        public List<Item> items { get; set; }
        public string imageUrl { get; set; }
        public string title { get; set; }
    }
    public class Benefit
    {
        public string name { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string vectorIconUrl { get; set; }
        public string rasterIconUrl { get; set; }
        public bool? premium { get; set; }
        public List<string> segments { get; set; }
    }

    public class BenefitsComponent
    {
        public List<Item> items { get; set; }
        public string title { get; set; }
    }

    public class Branch
    {
        public string city { get; set; }
        public string address { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressOfficialName { get; set; }
        public string postalCode { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class DailyRoutines
    {
        public List<string> items { get; set; }
        public string type { get; set; }
    }

    public class Department
    {
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string workingRulesTitle { get; set; }
        public WorkingRules workingRules { get; set; }
        public string dailyRoutinesTitle { get; set; }
        public DailyRoutines dailyRoutines { get; set; }
        public List<string> technologies { get; set; }
        public object developmentOpportunities { get; set; }
        public object departmentContractTypes { get; set; }
        public object departmentWorkOrganization { get; set; }
        public object salaryComponent { get; set; }
        public object departmentCustomerType { get; set; }
        public object departmentWorkSchedule { get; set; }
        public object departmentWorkMode { get; set; }
        public Segments segments { get; set; }
    }

    public class HeadOffice
    {
        public string city { get; set; }
        public string address { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressOfficialName { get; set; }
        public string postalCode { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
    public class JobOffersMetadata
    {
        public string title { get; set; }
    }

    public class Locations
    {
        public HeadOffice headOffice { get; set; }
        public List<Branch> branches { get; set; }
        public Metadata metadata { get; set; }
        public string title { get; set; }
        public string headOfficeTitle { get; set; }
        public string branchesTitle { get; set; }
    }

    public class Media
    {
        public string type { get; set; }
        public string url { get; set; }
        public object thumbnailUrl { get; set; }
        public object videoProvider { get; set; }
    }

    public class Metadata
    {
        public string locationType { get; set; }
        public string branchesDescription { get; set; }
    }

    public class Module
    {
        public string title { get; set; }
        public List<Section> sections { get; set; }
    }

    public class OurProduct
    {
        public string description { get; set; }
        public Media media { get; set; }
    }

    public class Ratings
    {
        public int? count { get; set; }
    }

    public class RecommendEmployer
    {
        public bool? isExist { get; set; }
    }

    public class Recruitments
    {
        public List<Item> items { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
    }

    public class RelatedCompany
    {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class PracujPlProfile
    {
        public List<Tag> tags { get; set; }
        public string publicId { get; set; }
        public string slug { get; set; }
        public string fullName { get; set; }
        public string shortName { get; set; }
        public string logoUrl { get; set; }
        public bool? publishToTheProtocol { get; set; }
        public List<Banner> banners { get; set; }
        public List<string> availableLanguageVersions { get; set; }
        public string currentLanguageVersion { get; set; }
        public string cvUrl { get; set; }
        public List<SocialLink> socialLinks { get; set; }
        public Locations locations { get; set; }
        public List<string> typesOfContracts { get; set; }
        public List<string> trades { get; set; }
        public string facts { get; set; }
        public WorkInformations workInformations { get; set; }
        public object internships { get; set; }
        public Recruitments recruitments { get; set; }
        public List<Benefit> benefits { get; set; }
        public BenefitsComponent benefitsComponent { get; set; }
        public Advantages advantages { get; set; }
        public AboutUs aboutUs { get; set; }
        public List<object> employees { get; set; }
        public List<RelatedCompany> relatedCompanies { get; set; }
        public List<Module> modules { get; set; }
        public Ratings ratings { get; set; }
        public WatchEmployer watchEmployer { get; set; }
        public RecommendEmployer recommendEmployer { get; set; }
        public object visualConfiguration { get; set; }
        public List<object> socialFeeds { get; set; }
        public object newsBar { get; set; }
        public List<object> virtualTours { get; set; }
        public object clients { get; set; }
        public OurProduct ourProduct { get; set; }
        public List<object> profileSettings { get; set; }
        public JobOffersMetadata jobOffersMetadata { get; set; }
        public object trainingSystem { get; set; }
        public object organizationTypes { get; set; }
        public object additionalBenefits { get; set; }
        public UserSegment userSegment { get; set; }
        public Settings settings { get; set; }
    }

    public class SectionsOrder
    {
        public string header { get; set; }
        public List<string> sections { get; set; }
    }

    public class Segments
    {
        public List<string> types { get; set; }
    }

    public class Settings
    {
        public bool? basicView { get; set; }
        public bool? showSurvey { get; set; }
        public string bannerVersion { get; set; }
    }

    public class SocialLink
    {
        public string socialLinkType { get; set; }
        public string url { get; set; }
    }

    public class Stage
    {
        public string name { get; set; }
        public string details { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public bool? isActive { get; set; }
    }

    public class UserSegment
    {
        public string type { get; set; }
        public object categoriesIds { get; set; }
        public object subcategoriesIds { get; set; }
        public List<SectionsOrder> sectionsOrder { get; set; }
    }

    public class WatchEmployer
    {
        public bool? isExist { get; set; }
    }

    public class WorkInformations
    {
        public List<Department> departments { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
    }

    public class WorkingRules
    {
        public List<string> items { get; set; }
        public string type { get; set; }
    }


}
