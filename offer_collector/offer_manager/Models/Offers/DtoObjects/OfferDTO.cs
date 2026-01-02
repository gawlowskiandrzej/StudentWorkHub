using System.Text.Json.Serialization;

namespace offer_manager.Models.Offers.dtoObjects
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;

        public CompanyDto Company { get; set; } = new CompanyDto();
        public EmploymentDto Employment { get; set; } = new EmploymentDto();
        public SalaryDto Salary { get; set; } = new SalaryDto();
        public DatesDto Dates { get; set; } = new DatesDto();
        public LocationDto Location { get; set; } = new LocationDto();
        public CategoryDto Category { get; set; } = new CategoryDto();
        public RequirementsDto Requirements { get; set; } = new RequirementsDto();

        public List<string>? Benefits { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsForUkrainians { get; set; }
    }
}
