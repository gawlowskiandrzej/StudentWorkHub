using Offer_collector.Interfaces;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.AplikujPl
{
    public class OfferListHeader
    {
        public string title;
        public string company;
        public string location;
        public string? salary;
        public string? employmentType;
        public bool? remoteOption;
        public string? companyLogoUrl;
        public string link;
        public string? dateAdded;
        public bool? recomended;
    }
    public class InfoFeatures
    {
        public string typeofWork; // phisical work
        public bool isRemote;
        public bool isforUkrainians;
        public bool isUrgent;
        public string typeofContract;
        public string logoUrl;
        public string description;
    }
    public class Salary
    {
        public decimal from;
        public decimal to;
        public string currency;
        public string type; // Brutto/netto
        public string period;
    }
    public class Company
    {
        public string company;
        public string companyLink;
        public string companyLogo;
    }
    public class Dates
    {
        public DateTime expirationDate;
        public DateTime publishionDate;
    }
    public class Localization
    {
        public string postalcode;
        public string city;
    }
    public class OfferDetails
    {
        public Dates dates;
        public Company company;
        public List<string> responsibilities;
        public List<string> requirements;
        public List<string> benefits;
        public Localization localization;
        public InfoFeatures infoFeatures;
        public string category;
        public Salary salary;
    }
    //TODO skipować zagraniczne offerty, puste localization sprawdzić co tam się dzieje w details dodać możliwość obsługi map google
    internal class AplikujplSchema : IUnificatable
    {
        public OfferListHeader header;
        public OfferDetails details;
        public UnifiedOfferSchema UnifiedSchema(string rawHtml = "")
        {
            UnifiedOfferSchema s = new UnifiedOfferSchema();

            s.jobTitle = header.title;
            s.description = details.infoFeatures.description;
            s.source = OfferSitesTypes.Aplikujpl;
            s.url = $"{AplikujPlUrlBuilder.baseUrl}{header.link}";
            s.company = new Offer_collector.Models.Company
            {
                logoUrl = header.companyLogoUrl ?? "",
                name = header.company
            };
            s.salary = new Models.Salary
            {
                currency = details.salary?.currency,
                from = details.salary?.from ?? 0,
                to = details.salary?.to ?? 0,
                type = details.salary?.type,
                period = details.salary?.period
            };
            s.location = new Location
            {
                city = header.location,
                isRemote = details.infoFeatures.isRemote,
                isHybrid = false,
            };
            
            s.requirements = new Requirements
            {
                skills = GetSkills(),
            };

            s.employment = new Employment
            {
                schedules = new List<string?> { details.infoFeatures?.typeofWork},
                types = new List<string?> { details.infoFeatures?.typeofContract}
            };
            s.dates = new Models.Dates
            {
                expires = details.dates.expirationDate,
                published = details.dates.publishionDate
            };
            s.benefits = details.benefits;
            s.category = new Category
            {
                leadingCategory = details.category,
                subCategories = new List<string> { details.category }
            };
            s.isForUkrainians = details.infoFeatures?.isforUkrainians ?? false;
            // TODO check this field
            s.isUrgent = false;

            return s;
        }
        List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            foreach (string skill in details.requirements ?? new List<string>())
            {
                skills.Add(new Skill
                {
                    skill = skill,
                });
            }
            return skills;
        }
    }
    
}
