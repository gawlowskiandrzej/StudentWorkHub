using FluentAssertions;
using OffersConnector;
using System.Collections.Generic;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class OffersConnectorResultParserTests
{
    [Fact]
    public void RestrictionsParser_WithNull_ReturnsEmpty()
    {
        var result = ResultParsers.RestrictionsParser(null);

        result.Should().NotBeNull();
        result!.Should().BeEmpty();
    }

    [Fact]
    public void RestrictionsParser_WithValues_BuildsPreambleAndLines()
    {
        var input = new Dictionary<string, List<string>>
        {
            { "employment_types", new List<string> { "B2B", "UoP" } },
            { "languages", new List<string> { "PL", "EN" } }
        };

        var result = ResultParsers.RestrictionsParser(input);

        result.Should().NotBeNull();
        result!.Count.Should().BeGreaterThan(1);
        result![0].Should().Contain("You must follow the rules");
        result.Should().Contain(line => line.Contains("employment_types"));
        result.Should().Contain(line => line.Contains("languages"));
    }
}
