using Offer_collector.Interfaces;

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
        public string city;
        public string province; // województwo
        public string typeofwork; // phisical work
        public string placeofwork; // praca stacjonarna
        public bool isnotrequireExperience;
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
        public Salary salary;
    }
    internal class AplikujplSchema : IUnificatable
    {
        OfferListHeader header;
        OfferDetails details;
        public UnifiedOfferSchema UnifiedSchema(string rawHtml = "")
        {
            throw new NotImplementedException();
        }
    }
}
