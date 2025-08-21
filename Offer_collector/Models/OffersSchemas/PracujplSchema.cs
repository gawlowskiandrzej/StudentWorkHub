
public class PracujplSchema
{
    public List<string> technologies { get; set; }
    public string aboutProjectShortDescription { get; set; }
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
    public bool isRemoteWorkAllowed { get; set; }
    public List<Offer> offers { get; set; }
    public List<string> positionLevels { get; set; }
    public List<string> typesOfContract { get; set; }
    public List<string> workSchedules { get; set; }
    public List<string> workModes { get; set; }
    public List<PrimaryAttribute> primaryAttributes { get; set; }
    public object commonOfferId { get; set; }
    public int searchEngineRelevancyScore { get; set; }
    public object mobileBannerUri { get; set; }
    public object desktopBannerUri { get; set; }
    public object aiSummary { get; set; }
    public List<object> appliedProducts { get; set; }
}

public class Coordinates
{
    public double latitude { get; set; }
    public double longitude { get; set; }
}

public class Label
{
    public string text { get; set; }
    public string pracujPlText { get; set; }
    public string primaryTargetSiteText { get; set; }
}

public class Model
{
    public string modelType { get; set; }
    public bool flag { get; set; }
}

public class Offer
{
    public int partitionId { get; set; }
    public string offerAbsoluteUri { get; set; }
    public string displayWorkplace { get; set; }
    public bool isWholePoland { get; set; }
    public List<object> appliedProducts { get; set; }
    public Coordinates coordinates { get; set; }
    public object distanceInKilometers { get; set; }
}

public class PrimaryAttribute
{
    public string code { get; set; }
    public Label label { get; set; }
    public Model model { get; set; }
}
