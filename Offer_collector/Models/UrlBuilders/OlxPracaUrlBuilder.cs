using Offer_collector.Models.Tools;

namespace Offer_collector.Models.UrlBuilders
{
    internal class OlxPracaUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.olx.pl/praca";
        public OlxPracaUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(SearchFilters searchFilters,Dictionary<string, string> parameters)
        {
            if (!string.IsNullOrEmpty(searchFilters.Localization))
            {
                string clearLocalization = searchFilters.Localization.ToLower().RemoveDiacritics();
                baseUrl += $"/{clearLocalization}"; 
            }

            if (!string.IsNullOrEmpty(searchFilters.Keyword))
                baseUrl += $"/q-{searchFilters.Keyword}";

            if (searchFilters.EmploymentType != null)
            {
                parameters["search%5Bfilter_enum_agreement%5D%5B0%5D"] = searchFilters.EmploymentType switch
                {
                    EmploymentType.EmploymentContract => "part",
                    EmploymentType.SpecificTaskContract => "contract",
                    EmploymentType.MandateContract => "zlecenie",
                    EmploymentType.B2BContract => "b2b",
                    EmploymentType.StudentPractice => "practice",
                    _ => ""
                };
            }
            if (searchFilters.WorkType != null)
            {
                parameters["search%5Bfilter_enum_type%5D%5B0%5D"] = searchFilters.WorkType switch
                { 
                    WorkTimeType.FullTimeStandardHours => "fulltime",
                    WorkTimeType.PartTimeStandardHours => "parttime",
                    WorkTimeType.PartTimeNightWork => "parttime",
                    WorkTimeType.FlexibleWorkingHours => "halftime",
                    WorkTimeType.FullTimeShiftWork => "fulltime",
                    WorkTimeType.FullTimeNightWork => "fulltime",
                    _ => ""
                };
            }

            return $"{baseUrl}";
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["page"] = pageId.ToString();
            }

            return parameters;
        }
    }
}
