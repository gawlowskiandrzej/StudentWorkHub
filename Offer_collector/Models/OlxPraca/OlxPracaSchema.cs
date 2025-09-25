using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Interfaces;
using Offer_collector.Models.JustJoinIt;
using System.Text.RegularExpressions;

namespace Offer_collector.Models.OlxPraca
{
    public class Category
    {
        public int? id { get; set; }
        public string? type { get; set; }
        public string? _nodeId { get; set; }
        public OlxPracaCategory? categoryDetails { get; set; }   
    }

    public class Contact
    {
        public bool? chat { get; set; }
        public bool? courier { get; set; }
        public string name { get; set; }
        public bool? negotiation { get; set; }
        public bool? phone { get; set; }
    }

    public class Delivery
    {
        public Rock rock { get; set; }
    }

    public class Location
    {
        public string cityName { get; set; }
        public int? cityId { get; set; }
        public string cityNormalizedName { get; set; }
        public string regionName { get; set; }
        public int? regionId { get; set; }
        public string regionNormalizedName { get; set; }
        public string districtName { get; set; }
        public int? districtId { get; set; }
        public string pathName { get; set; }
    }

    public class Map
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int? radius { get; set; }
        public bool? show_detailed { get; set; }
        public int? zoom { get; set; }
    }

    public class Param
    {
        public string? key { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? value { get; set; }
        public object? normalizedValue { get; set; }
    }

    public class Partner
    {
        public string? code { get; set; }
    }

    public class Promotion
    {
        public bool? highlighted { get; set; }
        public bool? top_ad { get; set; }
        public List<string>? options { get; set; }
        public bool? premium_ad_page { get; set; }
        public bool? urgent { get; set; }
        public bool? b2c_ad_page { get; set; }
    }

    public class Rock
    {
        public bool? active { get; set; }
        public string mode { get; set; }
        public object offer_id { get; set; }
    }

    public class OlxPracaSchema : IUnificatable
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Category category { get; set; }
        public Map map { get; set; }
        public bool? isBusiness { get; set; }
        public string url { get; set; }
        public bool? isHighlighted { get; set; }
        public bool? isPromoted { get; set; }
        public Promotion promotion { get; set; }
        public object externalUrl { get; set; }
        public Delivery delivery { get; set; }
        public DateTime? createdTime { get; set; }
        public DateTime? lastRefreshTime { get; set; }
        public object pushupTime { get; set; }
        public DateTime? validToTime { get; set; }
        public bool? isActive { get; set; }
        public string status { get; set; }
        public List<Param> @params { get; set; }
        public string itemCondition { get; set; }
        public object price { get; set; }
        public Salary salary { get; set; }
        public Partner partner { get; set; }
        public bool? isJob { get; set; }
        public List<object> photos { get; set; }
        public List<object> photosSet { get; set; }
        public Location location { get; set; }
        public string urlPath { get; set; }
        public Contact contact { get; set; }
        public User user { get; set; }
        public Shop shop { get; set; }
        public Safedeal safedeal { get; set; }
        public string searchReason { get; set; }
        public bool? isNewFavouriteAd { get; set; }

        public string htmlOfferDetail { get; set; }
       

        public UnifiedOfferSchema UnifiedSchema(string rawHtml = "")
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlOfferDetail);
            // TODO Dodać kategorię
            UnifiedOfferSchema un = new UnifiedOfferSchema();
            un.jobTitle = title;
            un.description = description;
            un.source = OfferSitesTypes.Olxpraca;
            un.url = url;
            // TODO Get name of company from html becuse of not valid company name
            un.company = new Company
            {
                name = doc.DocumentNode.SelectSingleNode("//*[@id=\"mainContent\"]/div[2]/main/aside/div[1]/div[2]/div/div/div/h4")?.InnerText ?? user.company_name,
                logoUrl = user.logo
            };
            un.salary = new Models.Salary
            {
                currency = salary?.currencySymbol ?? "zł",
                from = salary?.from == null ? 0 : (decimal)salary.from,
                to = salary?.from == null ? 0 : (decimal)salary.to,
                period = salary?.period ?? "",
                type = "Brutto"
            };

            Param? asd = @params.Where(_ => _.key.Contains("workplace")).FirstOrDefault();
            string workl = asd?.normalizedValue?.ToString() ?? "";
            List<string> workplaces = JsonConvert.DeserializeObject<List<string>>(workl) ?? new List<string>();

            un.location = new Models.Location
                {
                    city = location.cityName,
                    coordinates = new Coordinates
                    {
                        latitude = map.lat ?? 0,
                        longitude = map.lon ?? 0
                    },
                    postalCode = "",
                    buildingNumber = "",
                    isHybrid = workplaces.Any(_ => _ == "hybrid"),
                    isRemote = workplaces.Any(_ => _ == "remote_work_possibility")

            };
            un.requirements = ParseDescription(description);
            //if (un.requirements.experienceLevel?.Count == 0)
            //{
            //    List<string> experienceParam = @params.Where(_ => _.key.Contains("experience"))?.FirstOrDefault()?.normalizedValue as List<string> ?? new List<string>();
            //    if (experienceParam.FirstOrDefault() == "exp_yes")
            //        un.requirements.experienceLevel.Add("Wymagane");
            //}
            // TUTAJ
            List<string>workingSchedules = new List<string>();
            List<string>workingTypes = new List<string>();

            workingSchedules.Add(@params.Where(_ => _.key.Contains("type") && _.name.Contains("Wymiar pracy")).FirstOrDefault()?.normalizedValue?.ToString() ?? "");
            workingTypes = @params.Where(_ => _.key.Contains("agreement")).FirstOrDefault()?.normalizedValue as List<string> ?? new List<string>();
            un.employment = new Employment
            {
                schedules = workingSchedules,
                types = workingTypes
            };
            un.dates = new Dates
            {
                expires = validToTime,
                published = createdTime
            };
            //TODO benefity z description zparsować np. multisport, Prywatna opieka medyczna PZU Zdrowie, Elastyczne godziny pracy
            un.benefits = GetBenefits();

            // Nie znalazłem info więc domyślna wartość
            //un.isUrgent 

            // Nie znlazłem domyślnie true, wszędzie jest tłumaczenie na ukraiński więc imo true
            //un.isForUkrainians =

            //Nie znalazłem ewentualnie w jakiś sposób sparsować z opisu
            //un.isManyvacancies = false;

            un.leadingCategory = category.categoryDetails?.normalizedName;
            un.categories = new List<string> { un.leadingCategory ?? "" };

            


            return un;
        }
        List<string> GetBenefits()
        {
            List<string> benefits = new List<string>();

            if (description.ToLower().Contains("pzu"))
                benefits.Add("Opieka medyczna PZU");
            if (description.ToLower().Contains("multisport"))
                benefits.Add("Karta sportowa multisport");
            if (description.ToLower().Contains("atmosfer"))
                benefits.Add("Przyjazna atmosfera");
            if (description.ToLower().Contains("szkolen"))
                benefits.Add("Szkolenia");

            return benefits;
        }
        Requirements ParseDescription(string htmlDescription)
        {
            // TODO sprawdzić czy nie ma schematu dlatego wstawiłem tutaj html np. pierwszy strong to wykształcenie itp..
            // ewentualnie można użyć prostego modelu AI ewentualnie jakieś darmowe api (deepseek)
            var offer = new Requirements();
            var skills = new List<Skill>();
            var experienceLevel = new List<string>();
            var education = new List<string>();
            var languages = new List<LanguageSkill>();
            ushort experienceYears = 0;

            
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlDescription);
            string text = doc.DocumentNode.InnerText.ToLower();

            if (text.Contains("młodszy") || text.Contains("junior"))
                experienceLevel.Add("Junior");
            if (text.Contains("mid"))
                experienceLevel.Add("Mid");
            if (text.Contains("senior") || text.Contains("starszy"))
                experienceLevel.Add("Senior");

            if (experienceLevel.Contains("Junior") || text.Contains("staż"))
                experienceYears = 0;
            
            var matchExp = Regex.Match(text, @"(\d+)\s*(lat|rok|years)", RegexOptions.IgnoreCase);
            if (matchExp.Success)
                experienceYears = ushort.Parse(matchExp.Groups[1].Value);

            
            var langMap = new Dictionary<string, string> {
            { "angiel", "English" }, { "english", "English" },
            { "niemieck", "German" }, { "german", "German" },
            { "francus", "French" }, { "french", "French" },
            { "polsk", "Polish" }
        };
            foreach (var kv in langMap)
            {
                if (text.Contains(kv.Key))
                    languages.Add( new LanguageSkill { language = kv.Value, level = experienceYears.ToString() });
            }

            
            var skillKeywords = new[] {
            "seo", "sem", "link building", "google ads", "facebook ads",
            "excel", "arkusze google", "whitepress", "linkhouse",
            "clickup", "content marketing", "raport", "pozycjonowanie"
        };
            foreach (var skill in skillKeywords)
            {
                if (text.Contains(skill))
                    skills.Add(new Skill { name = skill, years = 0});
            }

            // --- 5. Edukacja (jeśli są słowa „studia”, „wykształcenie”) ---
            if (text.Contains("studia") || text.Contains("wykształcenie"))
                education.Add("Higher education");

            // --- 6. Ustaw wyniki ---
            offer.skills = skills.Count > 0 ? skills : null;
            offer.education = education.Count > 0 ? education : null;
            offer.languages = languages.Count > 0 ? languages : null;
            
            return offer;
        }
    }

    public class Safedeal
    {
        public List<object>? allowed_quantity { get; set; }
        public int? weight_grams { get; set; }
    }

    public class Shop
    {
        public string? subdomain { get; set; }
    }

    public class User
    {
        public long? id { get; set; }
        public string? name { get; set; }
        public object? photo { get; set; }
        public string? logo { get; set; }
        public bool? otherAdsEnabled { get; set; }
        public object? socialNetworkAccountType { get; set; }
        public bool? isOnline { get; set; }
        public DateTime? lastSeen { get; set; }
        public string? about { get; set; }
        public string? bannerDesktopURL { get; set; }
        public string? logo_ad_page { get; set; }
        public string? company_name { get; set; }
        public DateTime? created { get; set; }
        public object? sellerType { get; set; }
        public string? uuid { get; set; }
    }
}
