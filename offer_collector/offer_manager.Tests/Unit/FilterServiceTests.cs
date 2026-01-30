using Xunit;
using offer_manager.Models.FilterService;
using UnifiedOfferSchema;
using System.Collections.Generic;
using System.Collections.Frozen;
using FluentAssertions;

namespace offer_manager.Tests.Unit
{
    public class FilterServiceTests
    {
        [Fact]
        public void GetDynamicFilters_ShouldExtractEducationCorrectly()
        {
            var offers = new List<UOS?>
            {
                new UOS { Requirements = new Requirements { Education = new List<string> { "Higher" } } },
                new UOS { Requirements = new Requirements { Education = new List<string> { "Secondary" } } },
                new UOS { Requirements = new Requirements { Education = new List<string> { "Higher" } } },
                null
            }.ToFrozenSet();

            var service = new FilterService();

            var result = service.GetDynamicFilters(offers);

            result.EducationNames.Should().HaveCount(2);
            result.EducationNames.Should().Contain(new[] { "Higher", "Secondary" });
        }

        [Fact]
        public void GetDynamicFilters_WhenNoRequirements_ReturnsEmptyLists()
        {
            var offers = new List<UOS?> { new UOS() }.ToFrozenSet();
            var service = new FilterService();

            var result = service.GetDynamicFilters(offers);

            result.EducationNames.Should().BeEmpty();
            result.ExperienceLevels.Should().BeEmpty();
            result.Languages.Should().BeEmpty();
        }
    }
}
