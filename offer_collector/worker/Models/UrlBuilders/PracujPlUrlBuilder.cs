namespace Offer_collector.Models.UrlBuilders
{
    internal class PracujPlUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.pracuj.pl/praca";
        public PracujPlUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(SearchFilters searchFilters, Dictionary<string, string> parameters)
        {
            string url = baseUrl;
            if (searchFilters.EmploymentType != null)
            {
                parameters["tc"] = searchFilters.EmploymentType switch
                {
                    EmploymentType.EmploymentContract => "0",
                    EmploymentType.SpecificTaskContract => "1",
                    EmploymentType.MandateContract => "2",
                    EmploymentType.B2BContract => "3",
                    EmploymentType.StudentPractice => "7",
                    _ => "",
                };
            }
            if (searchFilters.WorkType != null)
            {
                parameters["ws"] = searchFilters.WorkType switch
                {
                    WorkTimeType.FullTimeStandardHours => "0",
                    WorkTimeType.FullTimeShiftWork => "0",
                    WorkTimeType.FullTimeNightWork => "0",
                    WorkTimeType.FullTimeWeekendWork => "0",
                    WorkTimeType.PartTimeStandardHours => "1",
                    WorkTimeType.PartTimeShiftWork => "1",
                    WorkTimeType.PartTimeNightWork => "1",
                    WorkTimeType.PartTimeWeekendWork => "1",
                    WorkTimeType.FlexibleWorkingHours => "2",
                    WorkTimeType.TaskBasedSystem => "2",
                    _ => "",
                };
            }

            if (!string.IsNullOrEmpty(searchFilters.Keyword))
                url += $"/{searchFilters.Keyword};kw";
            if (!string.IsNullOrEmpty(searchFilters.Localization))
                url += $"/{searchFilters.Localization};wp";

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
    }
}
