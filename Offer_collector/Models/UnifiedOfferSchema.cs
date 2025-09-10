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
        public List<string>? types { get; set; } // Future enum type umowa o prace, umowa zlecenie
        public List<string>? schedules { get; set; } // Future enum type full time, partTime
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
        public List<string>? skills { get; set; }
        public List<string>? experienceLevel { get; set; }
        public ushort experienceYears { get; set; }
        public List<string>? education { get; set; }
        public List<string>? languages { get; set; }
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
        public bool isUrgent { get; set; } = false;
        public bool isForUkrainians { get; set; } = true;
        public bool isManyvacancies { get; set; } = true;
        //TODO LISTA POMYSŁÓW UWAG
        // pole które będzie wskazywać na link do formularza z e-recruiter zeby można było odesłać użytkownika z html lub z pola json jak się da

        public string? leadingCategory { get; set; }
        public List<string>? categories { get; set; }
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
