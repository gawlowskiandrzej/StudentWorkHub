using FluentAssertions;
using Xunit;
using shared_models.Dto;
using Offer_collector.Models.UrlBuilders;

namespace offer_manager.Tests.Unit
{
    public class SearchDtoTests
    {
        [Fact]
        public void ToSearchFilters_ShouldMapAllFields()
        {
            var dto = new SearchDto
            {
                Keyword = "Software Engineer",
                Category = "IT",
                Localization = "Warsaw",
                SalaryFrom = "10000",
                SalaryTo = "20000",
                EmploymentType = "B2B",
                SalaryPeriod = "Month",
                EmploymentSchedule = "Full-time"
            };

            var filters = dto.ToSearchFilters();

            filters.Keyword.Should().Be("Software Engineer");
            filters.Category.Should().Be("IT");
            filters.Localization.Should().Be("Warsaw");
            
            filters.SalaryFrom.Should().Be(10000m);
            filters.SalaryTo.Should().Be(20000m);
        }
    }
}
