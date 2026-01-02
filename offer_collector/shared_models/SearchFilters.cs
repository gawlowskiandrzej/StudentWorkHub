namespace Offer_collector.Models.UrlBuilders
{
    public class SearchFilters
    {
        public string? Keyword { get; set; }
        public string? Category { get; set; }
        public string? Localization { get; set; }
        public WorkTimeType? WorkType { get; set; }
        public SalaryPeriod? SalaryPeriod { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }

        public SearchFilters Copy()
        {
            return new SearchFilters
            {
                Keyword = this.Keyword,
                Category = this.Category,
                Localization = this.Localization,
                WorkType = this.WorkType,
                SalaryPeriod = this.SalaryPeriod,
                EmploymentType = this.EmploymentType,
                SalaryFrom = this.SalaryFrom,
                SalaryTo = this.SalaryTo
            };
        }
    }

}
