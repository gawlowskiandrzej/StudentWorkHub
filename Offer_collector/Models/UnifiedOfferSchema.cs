using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.PracujPl;
using System.Text.RegularExpressions;

namespace Offer_collector.Models
{
    public class Company
    {
        public string name { get; set; }
        public string logoUrl { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Dates
    {
        public DateTime? published { get; set; }
        public DateTime? expires { get; set; }
    }

    public class Employment
    {
        public List<string> types { get; set; } // Future enum type umowa o prace, umowa zlecenie
        public List<string> schedules { get; set; } // Future enum type full time, partTime
    }

    public class Location
    {
        public string? buildingNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? postalCode { get; set; }
        public Coordinates? coordinates { get; set; }
        public bool isRemote { get; set; }
        public bool isHybrid { get; set; }
    }

    public class Requirements
    {
        public List<string> skills { get; set; }
        public List<string> experienceLevel { get; set; }
        public ushort experienceYears { get; set; }
        public List<string> education { get; set; }
        public List<string> languages { get; set; }
    }

    public class UnifiedOfferSchema
    {
        public int id { get; set; }
        public OfferSitesTypes source { get; set; } // Pracujpl / olx ...
        public string url { get; set; } // redirect url to source of offer
        public string jobTitle { get; set; }
        public Company? company { get; set; }
        public string? description { get; set; }
        public Salary? salary { get; set; }
        public Location? location { get; set; }
        public Requirements? requirements { get; set; }
        public Employment? employment { get; set; }
        public Dates? dates { get; set; }
        public List<string>? benefits { get; set; }
        public bool isUrgent { get; set; }
        public bool isForUkrainians { get; set; }

        public UnifiedOfferSchema(PracujplSchema pracujPl, PracujPlCompany pracujCompany)
        {
            jobTitle = pracujPl.jobTitle;
            description = pracujPl.jobDescription;
            source = OfferSitesTypes.Pracujpl;
            url = pracujPl.offers.FirstOrDefault()?.offerAbsoluteUri ?? "";
            company = new Company()
            {
                logoUrl = pracujPl.companyLogoUri,
                name = pracujPl.companyName
            };
            GetSalaryFromString(pracujPl.salaryDisplayText);
            GetCompanyLocation(pracujCompany.addressContract, pracujPl.workModes.Contains("hybrydow"), pracujPl.isRemoteWorkAllowed);
            requirements = new Requirements
            {
                //education = 
            };

            // Getting salary from second request
            //salary = pracujPl.
        }
        public Salary GetSalaryFromString(string salaryString)
        {
            // Regex to match salary ranges, currency, net/gross, and period
            var regex = new Regex(@"(\d{1,3}(?:\s\d{3})*)–(\d{1,3}(?:\s\d{3})*)\s*(zł)\s*(netto|brutto)?(?:\s*\(\+ VAT\))?\s*/\s*(mies\.|godz\.)");


            var match = regex.Match(salaryString);
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
                salary = salaryRange;
            }
            return new Salary();
        }
        public Location GetCompanyLocation(AddressContract addr, bool isHybrid, bool isRemote)
        {

            return new Location
            {
                city = addr.city,
                postalCode = addr.postalCode,
                buildingNumber = String.IsNullOrEmpty(addr.apartmentNumber) ? addr.houseNumber : addr.apartmentNumber,
                street = addr.street,
                coordinates = null,
                isHybrid = isHybrid,
                isRemote = isRemote
            };
        }
        public UnifiedOfferSchema(JoobleSchema pracujPl)
        {

        }
        public UnifiedOfferSchema(JustJoinItSchema pracujPl)
        {

        }
        public UnifiedOfferSchema(OlxPracaSchema olxPraca)
        {

        }
    }

    public class Salary
    {
        public decimal from { get; set; }
        public decimal to { get; set; }
        public string? currency { get; set; }
        public string? period { get; set; } // Future enum type montly/weekly/daily
        public string? type { get; set; } // future enum type gross/net
    }
}
