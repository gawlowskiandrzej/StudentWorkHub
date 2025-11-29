namespace Offer_collector.Models.UrlBuilders
{
    public class SearchFilters
    {
        public string Keyword { get; set; } = "pracownik budowy";
        public string Category { get; set; } = "";
        public string Localization { get; set; } = "";
        public WorkTimeType? WorkType { get; set; } = WorkTimeType.FullTimeStandardHours;
        public string WorkTime { get; set; } = "";
        public EmploymentType? EmploymentType { get; set; } = Models.EmploymentType.EmploymentContract;
        public decimal SalaryFrom { get; set; } = 0;
        public decimal SalaryTo { get; set; } = 0;

        public SearchFilters Copy()
        {
            return new SearchFilters
            {
                Keyword = this.Keyword,
                Category = this.Category,
                Localization = this.Localization,
                WorkType = this.WorkType,
                WorkTime = this.WorkTime,
                EmploymentType = this.EmploymentType,
                SalaryFrom = this.SalaryFrom,
                SalaryTo = this.SalaryTo
            };
        }
    }

}
