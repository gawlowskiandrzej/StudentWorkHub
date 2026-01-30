using FluentAssertions;
using Xunit;
using Offer_collector.Models.UrlBuilders;
using Offer_collector.Models;

namespace offer_manager.Tests.Unit
{
    public class SearchFiltersTests
    {
        [Fact]
        public void Copy_ShouldBeTrueDeepCopy()
        {
            var original = new SearchFilters
            {
                Keyword = "original",
                SalaryFrom = 5000,
                SalaryTo = 10000,
                EmploymentType = EmploymentType.B2BContract,
                SalaryPeriod = SalaryPeriod.None
            };

            var copy = original.Copy();

            copy.Should().BeEquivalentTo(original);
            copy.Should().NotBeSameAs(original);
        }
    }
}
