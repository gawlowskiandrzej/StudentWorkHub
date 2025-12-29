namespace offer_manager.Models.Users
{
    public sealed class InsertSearchHistoryRequest
    {
        public string Jwt { get; init; } = string.Empty;

        public string? Keywords { get; init; }
        public int? Distance { get; init; }
        public bool? IsRemote { get; init; }
        public bool? IsHybrid { get; init; }
        public short? LeadingCategoryId { get; init; }
        public int? CityId { get; init; }
        public decimal? SalaryFrom { get; init; }
        public decimal? SalaryTo { get; init; }
        public short? SalaryPeriodId { get; init; }
        public short? SalaryCurrencyId { get; init; }
        public short[]? EmploymentScheduleIds { get; init; }
        public short[]? EmploymentTypeIds { get; init; }
    }

    public sealed class InsertSearchHistoryResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
