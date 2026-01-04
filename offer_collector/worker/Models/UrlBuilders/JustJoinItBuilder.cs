using Offer_collector.Models.Tools;

namespace Offer_collector.Models.UrlBuilders
{
    internal class JustJoinItBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://justjoin.it/job-offers";
        public static string baseUrlOfferDetail = "https://justjoin.it/job-offer/";

        public JustJoinItBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(SearchFilters searchFilters,Dictionary<string, string> parameters)
        {
            string url = baseUrl;
            if (!string.IsNullOrEmpty(searchFilters.Keyword))
                parameters["keyword"] = searchFilters.Keyword;
            if (!string.IsNullOrEmpty(searchFilters.Localization))
            {
                string clearLocalization = searchFilters.Localization.ToLower().RemoveDiacritics();
                url += $"/{clearLocalization}"; 
            }
            if (searchFilters.EmploymentType != null)
            {
                parameters["employment-type"] = searchFilters.EmploymentType switch
                {
                    EmploymentType.EmploymentContract => "permanent",
                    EmploymentType.SpecificTaskContract => "specific-task-contract",
                    EmploymentType.MandateContract => "mandate-contract",
                    EmploymentType.B2BContract => "b2b",
                    _ => "",
                };
            }
            if (searchFilters.WorkType != null)
            {
                parameters["working-hours"] = searchFilters.WorkType switch
                {
                    WorkTimeType.FullTimeStandardHours => "full-time",
                    WorkTimeType.FullTimeShiftWork => "full-time",
                    WorkTimeType.FullTimeNightWork => "full-time",
                    WorkTimeType.FullTimeWeekendWork => "full-time",
                    WorkTimeType.PartTimeStandardHours => "part-time",
                    WorkTimeType.PartTimeShiftWork => "part-time",
                    WorkTimeType.PartTimeNightWork => "part-time",
                    WorkTimeType.PartTimeWeekendWork => "part-time",
                    WorkTimeType.TaskBasedSystem => "freelance",
                    _ => "",
                };
            }

            return $"{url}";
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["pn"] = pageId.ToString();
            }

            return parameters;
        }
        protected override string BuildQuery(Dictionary<string, string> parameters)
        {
            return base.BuildQuery(parameters);
        }
    }
}
