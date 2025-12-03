using Offer_collector.Interfaces;
using Offer_collector.Models;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using System.Text.RegularExpressions;

public class PracujplSchema : IUnificatable
{
    // Basic fields by pracuj
    public List<string>? technologies { get; set; }
    public string? aboutProjectShortDescription { get; set; }
    public string? groupId { get; set; }
    public string? jobTitle { get; set; }
    public string? companyName { get; set; }
    public string? companyProfileAbsoluteUri { get; set; }
    public int companyId { get; set; }
    public string? companyLogoUri { get; set; }
    public DateTime lastPublicated { get; set; }
    public DateTime expirationDate { get; set; }
    public string? salaryDisplayText { get; set; }
    public string? jobDescription { get; set; }
    public bool isSuperOffer { get; set; }
    public bool isFranchise { get; set; }
    public bool isOptionalCv { get; set; }
    public bool isOneClickApply { get; set; }
    public bool isJobiconCompany { get; set; }
    public bool isRemoteWorkAllowed { get; set; }
    public List<Offer>? offers { get; set; }
    public List<string>? positionLevels { get; set; }
    public List<string?>? typesOfContract { get; set; }
    public List<string?>? workSchedules { get; set; }
    public List<string>? workModes { get; set; }
    public List<PrimaryAttribute>? primaryAttributes { get; set; }
    public object? commonOfferId { get; set; }
    public int searchEngineRelevancyScore { get; set; }
    public object? mobileBannerUri { get; set; }
    public object? desktopBannerUri { get; set; }
    public object? aiSummary { get; set; }
    public List<object>? appliedProducts { get; set; }

    // Own added fields
    public PracujPlOfferDetails? details { get; set; }
    public PracujPlCompany? company { get; set; }
    public PracujPlProfile? profile { get; set; }

    public UnifiedOfferSchemaClass UnifiedSchema(string rawHtml = "")
    {
        UnifiedOfferSchemaClass s = new UnifiedOfferSchemaClass();
        s.jobTitle = jobTitle ?? "nazwa pracy pusta";

        if (!string.IsNullOrEmpty(aiSummary as string))
            s.description = aiSummary as string;
        else
            s.description = jobDescription;

        s.source = OfferSitesTypes.Pracujpl;

        s.url = offers?.FirstOrDefault()?.offerAbsoluteUri ?? "";

        s.company = new Offer_collector.Models.Company
        {
            logoUrl = companyLogoUri ?? "",
            name = companyName ?? "nazwa firmy pusta",
        };
        s.salary = GetSalaryFromString();

        s.location = company != null ? GetCompanyLocation() : GetCompanyLocationByProfile();

        s.requirements = GetRequirements() ?? new Requirements();

        s.employment = new Offer_collector.Models.Employment
        {
            schedules = workSchedules,
            types = typesOfContract,
        };

        s.dates = new Dates
        {
            expires = expirationDate,
            published = lastPublicated
        };
        s.category = new Offer_collector.Models.Category
        {
            subCategories = details?.attributes?.categories?.Select(_ => _.name).ToList() ?? new List<string?>(),
            leadingCategory = details?.attributes?.leadingCategory?.name
        };

        Offer_collector.Models.PracujPl.Model model = details?.sections?.Where(_ => _.sectionType.Contains("benefits")).FirstOrDefault()?.model ?? new Offer_collector.Models.PracujPl.Model();
        List<string> custBenefits = model.customItems?.Select(_ => _.name).ToList() ?? new List<string>();
        List<string> benefits = model.items?.Select(_ => _.name).ToList() ?? new List<string>();
        benefits.AddRange(custBenefits);
        if (benefits.Count > 0)
            s.benefits = benefits;

        s.isUrgent = primaryAttributes?.Count(_ =>!String.IsNullOrEmpty(_.code) && _.code.Contains("immediate-employment")) > 0;
        s.isForUkrainians = primaryAttributes?.Where(_ => !String.IsNullOrEmpty(_.code) && _.code.Contains("ukrainian-friendly")).Count() > 0;

        return s;
    }
    Requirements? GetRequirements() => RequirementsParser.ParseRequirements(details?.sections?.Where(_ => _.sectionType.Contains("requirements")).FirstOrDefault()?.subSections?.FirstOrDefault()?.model?.bullets ?? new List<string>());
    Salary GetSalaryFromString()
    {
        // Regex to match salary ranges, currency, net/gross, and period
        var regex = new Regex(@"(\d{1,3}(?:[ \u00A0]\d{3})*)–(\d{1,3}(?:[ \u00A0]\d{3})*)\s*(zł)\s*(netto|brutto)?(?:\s*\(\+\s*VAT\))?\s*/\s*(mies\.|godz\.)");

        var match = regex.Match(salaryDisplayText ?? "");
        if (match.Success)
        {
            var salaryRange = new Salary
            {
                from = decimal.Parse(match.Groups[1].Value.Replace(" ", "")),
                to = decimal.Parse(match.Groups[2].Value.Replace(" ", "")),
                currency = match.Groups[3].Value,
                type = match.Groups[4].Value == "brutto" ? "Brutto" : match.Groups[4].Value == "netto" ? "Netto" : "Unspecified",
                period = match.Groups[5].Value == "mies." ? "mies." : "godz."
            };

            return salaryRange;
        }
        return new Salary();
    }
    Offer_collector.Models.Location GetCompanyLocation()
    {
        return new Offer_collector.Models.Location
        {
            city = company?.addressContract?.city,
            postalCode = company?.addressContract?.postalCode,
            buildingNumber = String.IsNullOrEmpty(company?.addressContract.apartmentNumber) ? company?.addressContract.houseNumber : company.addressContract.apartmentNumber,
            street = company?.addressContract?.street,
            coordinates = offers?.FirstOrDefault()?.coordinates,
            isHybrid = workModes?.Contains("hybrydow") ?? false,
            isRemote = isRemoteWorkAllowed
        };
    }
    Offer_collector.Models.Location GetCompanyLocationByProfile()
    {
        return new Offer_collector.Models.Location
        {
            city = profile?.locations?.headOffice?.city,
            postalCode = profile?.locations?.headOffice?.postalCode,
            buildingNumber = String.IsNullOrEmpty(profile?.locations?.headOffice?.addressOfficialName) ? profile?.locations?.headOffice?.addressOfficialName : "",
            street = profile?.locations?.headOffice?.address,
            coordinates = new Offer_collector.Models.Coordinates
            {
                latitude = profile?.locations?.headOffice?.latitude ?? 0,
                longitude = profile?.locations?.headOffice?.longitude ?? 0
            },
            isHybrid = workModes?.Contains("hybrydow") ?? false,
            isRemote = isRemoteWorkAllowed
        };
    }
}

public class Coordinates
{
    public double latitude { get; set; }
    public double longitude { get; set; }
}

public class Label
{
    public string? text { get; set; }
    public string? pracujPlText { get; set; }
    public string? primaryTargetSiteText { get; set; }
}

public class Model
{
    public string? modelType { get; set; }
    public bool flag { get; set; }
}

public class Offer
{
    public int partitionId { get; set; }
    public string offerAbsoluteUri { get; set; } = "";
    public string displayWorkplace { get; set; } = "";
    public bool isWholePoland { get; set; }
    public List<object>? appliedProducts { get; set; }
    public Offer_collector.Models.Coordinates? coordinates { get; set; }
    public object? distanceInKilometers { get; set; }
}

public class PrimaryAttribute
{
    public string? code { get; set; }
    public Label? label { get; set; }
    public Model? model { get; set; }
}
