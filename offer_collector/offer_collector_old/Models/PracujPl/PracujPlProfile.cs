using System;
using System.Collections.Generic;

namespace Offer_collector.Models.PracujPl
{
    public class AboutUs
    {
        public string description { get; set; } = string.Empty;
        public Media media { get; set; } = new Media();
        public string title { get; set; } = string.Empty;
    }

    public class Advantages
    {
        public List<Item> items { get; set; } = new();
        public string imageUrl { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
    }

    public class Benefit
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string iconUrl { get; set; } = string.Empty;
        public string vectorIconUrl { get; set; } = string.Empty;
        public string rasterIconUrl { get; set; } = string.Empty;
        public bool? premium { get; set; }
        public List<string> segments { get; set; } = new();
    }

    public class BenefitsComponent
    {
        public List<Item> items { get; set; } = new();
        public string title { get; set; } = string.Empty;
    }

    public class Branch
    {
        public string city { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string addressLine1 { get; set; } = string.Empty;
        public string addressLine2 { get; set; } = string.Empty;
        public string addressOfficialName { get; set; } = string.Empty;
        public string postalCode { get; set; } = string.Empty;
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class DailyRoutines
    {
        public List<string> items { get; set; } = new();
        public string type { get; set; } = string.Empty;
    }

    public class Department
    {
        public string name { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public string workingRulesTitle { get; set; } = string.Empty;
        public WorkingRules workingRules { get; set; } = new();
        public string dailyRoutinesTitle { get; set; } = string.Empty;
        public DailyRoutines dailyRoutines { get; set; } = new();
        public List<string> technologies { get; set; } = new();
        public Segments segments { get; set; } = new();
    }

    public class HeadOffice
    {
        public string city { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string addressLine1 { get; set; } = string.Empty;
        public string addressLine2 { get; set; } = string.Empty;
        public string addressOfficialName { get; set; } = string.Empty;
        public string postalCode { get; set; } = string.Empty;
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class JobOffersMetadata
    {
        public string title { get; set; } = string.Empty;
    }

    public class Locations
    {
        public HeadOffice headOffice { get; set; } = new();
        public List<Branch> branches { get; set; } = new();
        public Metadata metadata { get; set; } = new();
        public string title { get; set; } = string.Empty;
        public string headOfficeTitle { get; set; } = string.Empty;
        public string branchesTitle { get; set; } = string.Empty;
    }

    public class Media
    {
        public string type { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public object? thumbnailUrl { get; set; }
        public object? videoProvider { get; set; }
    }

    public class Metadata
    {
        public string locationType { get; set; } = string.Empty;
        public string branchesDescription { get; set; } = string.Empty;
    }

    public class Module
    {
        public string title { get; set; } = string.Empty;
        public List<Section> sections { get; set; } = new();
    }

    public class OurProduct
    {
        public string description { get; set; } = string.Empty;
        public Media media { get; set; } = new();
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
        public List<Item> items { get; set; } = new();
        public string subtitle { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
    }

    public class RelatedCompany
    {
        public int? id { get; set; }
        public string name { get; set; } = string.Empty;
    }

    public class PracujPlProfile
    {
        public List<Tag> tags { get; set; } = new();
        public string publicId { get; set; } = string.Empty;
        public string slug { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string shortName { get; set; } = string.Empty;
        public string logoUrl { get; set; } = string.Empty;
        public bool? publishToTheProtocol { get; set; }
        public List<Banner> banners { get; set; } = new();
        public List<string> availableLanguageVersions { get; set; } = new();
        public string currentLanguageVersion { get; set; } = string.Empty;
        public string cvUrl { get; set; } = string.Empty;
        public List<SocialLink> socialLinks { get; set; } = new();
        public Locations locations { get; set; } = new();
        public List<string> typesOfContracts { get; set; } = new();
        public List<string> trades { get; set; } = new();
        public string facts { get; set; } = string.Empty;
        public WorkInformations workInformations { get; set; } = new();
        public Recruitments recruitments { get; set; } = new();
        public List<Benefit> benefits { get; set; } = new();
        public BenefitsComponent benefitsComponent { get; set; } = new();
        public Advantages advantages { get; set; } = new();
        public AboutUs aboutUs { get; set; } = new();
        public List<RelatedCompany> relatedCompanies { get; set; } = new();
        public List<Module> modules { get; set; } = new();
        public Ratings ratings { get; set; } = new();
        public WatchEmployer watchEmployer { get; set; } = new();
        public RecommendEmployer recommendEmployer { get; set; } = new();
        public OurProduct ourProduct { get; set; } = new();
        public JobOffersMetadata jobOffersMetadata { get; set; } = new();
        public UserSegment userSegment { get; set; } = new();
        public Settings settings { get; set; } = new();
    }

    public class SectionsOrder
    {
        public string header { get; set; } = string.Empty;
        public List<string> sections { get; set; } = new();
    }

    public class Segments
    {
        public List<string> types { get; set; } = new();
    }

    public class Settings
    {
        public bool? basicView { get; set; }
        public bool? showSurvey { get; set; }
        public string bannerVersion { get; set; } = string.Empty;
    }

    public class SocialLink
    {
        public string socialLinkType { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
    }

    public class Tag
    {
        public string name { get; set; } = string.Empty;
        public bool? isActive { get; set; }
    }

    public class UserSegment
    {
        public string type { get; set; } = string.Empty;
        public List<SectionsOrder> sectionsOrder { get; set; } = new();
    }

    public class WatchEmployer
    {
        public bool? isExist { get; set; }
    }

    public class WorkInformations
    {
        public List<Department> departments { get; set; } = new();
        public string subtitle { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
    }

    public class WorkingRules
    {
        public List<string> items { get; set; } = new();
        public string type { get; set; } = string.Empty;
    }
}
