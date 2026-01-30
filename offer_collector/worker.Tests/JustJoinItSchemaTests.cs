using FluentAssertions;
using Xunit;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models;
using System.Collections.Generic;

namespace offer_collector.Tests.Unit
{
    public class JustJoinItSchemaTests
    {
        [Fact]
        public void UnifiedSchema_ShouldBeCaseInsensitive_ForWorkplaceType()
        {
            var schema = new JustJoinItSchema
            {
                workplaceType = "Hybrid", 
                slug = "test-offer",
                title = "Engineer"
            };

            var unified = schema.UnifiedSchema();

            unified.location.isHybrid.Should().BeTrue("Hybrid workplace type should be recognized regardless of case");
        }

        [Fact]
        public void UnifiedSchema_ShouldNotSetSalaryToZero_WhenSalaryIsMissing()
        {
            var schema = new JustJoinItSchema
            {
                employmentTypes = new List<EmploymentType>(),
                slug = "test-offer",
                title = "Engineer"
            };

            var unified = schema.UnifiedSchema();

            unified.salary.from.Should().NotBe(0m, "Salary should not default to 0 if missing from source data");
        }
    }
}
