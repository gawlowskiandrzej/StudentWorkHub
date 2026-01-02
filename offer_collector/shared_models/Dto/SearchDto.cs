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
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
    }
}
