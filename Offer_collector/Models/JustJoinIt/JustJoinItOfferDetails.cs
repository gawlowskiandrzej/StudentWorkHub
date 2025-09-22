namespace Offer_collector.Models.JustJoinIt
{
    public class Category
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public object parent_id { get; set; }
        public int? lft { get; set; }
        public int? rgt { get; set; }
        public int? depth { get; set; }
        public int? children_count { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Icon icon { get; set; }
        public string seo_slug { get; set; }
        public bool? @static { get; set; }
        public object redirect_to { get; set; }
        public bool? editable_in_offer { get; set; }
        public string button_width { get; set; }
        public int? order { get; set; }
        public bool? active { get; set; }
        public object deleted_at { get; set; }
        public object is_new_until { get; set; }
        public int? childrenCount { get; set; }
        public object parentId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }

    public class ExperienceLevel
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Icon
    {
        public string d { get; set; }
        public string x1 { get; set; }
        public string x2 { get; set; }
        public string y1 { get; set; }
        public string y2 { get; set; }
        public object width { get; set; }
        public object height { get; set; }
        public string colorTo { get; set; }
        public object viewBox { get; set; }
        public string clipRule { get; set; }
        public string fillRule { get; set; }
        public string colorFrom { get; set; }
        public string darkColorTo { get; set; }
        public string darkColorFrom { get; set; }
        public string gradientUnits { get; set; }
    }
    public class OfferParent
    {
        public string slug { get; set; }
    }

    public class RequiredSkill
    {
        public string name { get; set; }
        public int? level { get; set; }
    }

    public class JustJoinItOfferDetails
    {
        public string? slug { get; set; }
        public string? title { get; set; }
        public ExperienceLevel? experienceLevel { get; set; }
        public Category? category { get; set; }
        public string? companyName { get; set; }
        public string? companyUrl { get; set; }
        public string? body { get; set; }
        public string? city { get; set; }
        public string? street { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? companySize { get; set; }
        public string? informationClause { get; set; }
        public object? futureConsent { get; set; }
        public object? customConsent { get; set; }
        public string? companyLogoUrl { get; set; }
        public bool? remoteInterview { get; set; }
        public List<EmploymentType>? employmentTypes { get; set; }
        public WorkplaceType? workplaceType { get; set; }
        public List<RequiredSkill>? requiredSkills { get; set; }
        public List<object>? niceToHaveSkills { get; set; }
        public WorkingTime? workingTime { get; set; }
        public string? applyUrl { get; set; }
        public DateTime? publishedAt { get; set; }
        public string? coverImage { get; set; }
        public object? brandStorySlug { get; set; }
        public object? brandStoryCoverPhotoUrl { get; set; }
        public object? brandStoryShortDescription { get; set; }
        public bool? openToHireUkrainians { get; set; }
        public List<Multilocation>? multilocation { get; set; }
        public object? videoUrl { get; set; }
        public object? bannerUrl { get; set; }
        public bool? isOfferActive { get; set; }
        public DateTime? expiredAt { get; set; }
        public string? countryCode { get; set; }
        public OfferParent? offerParent { get; set; }
        public List<Language>? languages { get; set; }
        public string? guid { get; set; }
    }

    public class WorkingTime
    {
        public string? label { get; set; }
        public string? value { get; set; }
    }

    public class WorkplaceType
    {
        public string? label { get; set; }
        public string? value { get; set; }
    }
}
