namespace Offer_collector.Models.UrlBuilders
{
    public class SearchFilters
    {
        public string Keyword { get; set; } = "";
        public string Category { get; set; } = "";
        public string Localization { get; set; } = "";
        public int Distance { get; set; } = 0;
        public WorkTimeType? WorkType { get; set; }
        public string WorkTime { get; set; } = "";
        public EmploymentType? EmploymentType { get; set; }
        public decimal SalaryFrom { get; set; } = 0;
        public decimal SalaryTo { get; set; } = 0;
    }
}
