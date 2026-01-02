namespace offer_manager.Models.Offers.dtoObjects
{
    public class SalaryDto
    {
        public decimal? From { get; set; }
        public decimal? To { get; set; }
        public string? Currency { get; set; }
        public string? Period { get; set; }
        public string? Type { get; set; }
    }
}