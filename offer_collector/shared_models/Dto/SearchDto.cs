using Offer_collector.Models.UrlBuilders;

namespace shared_models.Dto
{
    public class SearchDto
    {
        public string? Keyword { get; set; }
        public string? Category { get; set; }
        public string? Localization { get; set; }
        public string? EmploymentType { get; set; }
        public string? SalaryPeriod { get; set; }
        public string? EmploymentSchedule { get; set; }
        public string? SalaryFrom { get; set; }
        public string? SalaryTo { get; set; }

        public SearchFilters ToSearchFilters()
        {
            return new SearchFilters
            {
                Keyword = Keyword,
                Category = Category,
                Localization = Localization,
                //TODO
            };
        }
    }
}
