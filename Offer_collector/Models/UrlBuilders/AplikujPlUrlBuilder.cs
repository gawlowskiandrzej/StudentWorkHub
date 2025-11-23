
using System;

namespace Offer_collector.Models.UrlBuilders
{
    internal class AplikujPlUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.aplikuj.pl";
        public AplikujPlUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(SearchFilters searchFilters,Dictionary<string, string> parameters)
        {
            string url = $"{baseUrl}/praca";
            if (!string.IsNullOrEmpty(searchFilters.Localization))
            {
                url += $"/{searchFilters.Localization}";
            }
            if (parameters.TryGetValue("page", out var pageValue) && int.TryParse(pageValue, out int pageNumber) && pageNumber > 1)
            {
                url += $"/strona-{pageNumber}";
            }
            if (!string.IsNullOrEmpty(searchFilters.Keyword))
                parameters["keyword"] = searchFilters.Keyword;
            if (searchFilters.EmploymentType != null)
            {
                    parameters["employment_type%5B0%5D"] = searchFilters.EmploymentType switch 
                    {
                        EmploymentType.EmploymentContract => "3",
                        EmploymentType.MandateContract => "2",
                        EmploymentType.SpecificTaskContract => "6",
                        EmploymentType.B2BContract => "2",
                        EmploymentType.PaidInternship => "5",
                        EmploymentType.UnpaidInternship => "5",
                        EmploymentType.StudentPractice => "5",
                        EmploymentType.Volunteering => "9",
                        _ => ""
                    };
            }
            if (searchFilters.WorkType != null)
            {       parameters["work_time%5B0%5D"] = searchFilters.WorkType switch 
                    {
                        WorkTimeType.FullTimeStandardHours => "1",
                        WorkTimeType.FullTimeShiftWork => "1",
                        WorkTimeType.FullTimeNightWork => "1",
                        WorkTimeType.FullTimeWeekendWork => "1",
                        WorkTimeType.PartTimeNightWork => "2",
                        WorkTimeType.PartTimeShiftWork => "2",
                        WorkTimeType.PartTimeStandardHours => "2",
                        WorkTimeType.PartTimeWeekendWork => "2",
                        WorkTimeType.FlexibleWorkingHours => "3",
                        _ => ""
                    };
            }
            return url;
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["page"] = pageId.ToString();
            }

            return parameters;
        }
        protected override string BuildQuery(Dictionary<string, string> parameters)
        {
            return base.BuildQuery(parameters);
        }
    }
}
