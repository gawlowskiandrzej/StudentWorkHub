using FluentAssertions;
using offer_manager.Models.FilterService;
using shared_models;
using System.Collections.Frozen;
using UnifiedOfferSchema;
using Xunit;

namespace offer_manager.Tests.Services;

public class FilterServiceTests
{
    private readonly FilterService _sut = new FilterService();

    [Fact]
    public void GetDynamicFilters_HandlesDeepNullsGracefully()
    {
        var brokenOffer = new UOS
        {
            Requirements = new Requirements
            {
                Skills = null,
                Languages = new List<Languages> { new Languages { Language = "English", Level = "C1" } }
            }
        };

        var totallyNullReqs = new UOS
        {
            Requirements = null
        };

        var set = new[] { brokenOffer, totallyNullReqs }.ToFrozenSet();

        var result = _sut.GetDynamicFilters(set);

        result.ExperienceLevels.Should().BeEmpty();
        result.EducationNames.Should().BeEmpty();
        result.Languages.Should().Contain(l => l.Language == "English");
    }

    [Fact]
    public void GetDynamicFilters_HandlesNullOfferInSet()
    {
        var set = new UOS?[] { null, new UOS() }.ToFrozenSet();

        var result = _sut.GetDynamicFilters(set);

        result.Should().NotBeNull();
    }
}
