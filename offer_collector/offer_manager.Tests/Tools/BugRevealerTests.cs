using FluentAssertions;
using Offer_collector.Models.Tools;
using worker.Models.Tools;
using Xunit;
using System.Text.RegularExpressions;

namespace offer_manager.Tests.Tools;

public class BugRevealerTests
{
    [Fact]
    public void ReplaceAt_WithIndexGreaterThanLength_ThrowsClearException()
    {
        string input = "abc";
        
        var action = () => input.ReplaceAt(10, 2, "X");
        
        action.Should().Throw<ArgumentOutOfRangeException>()
              .WithMessage("*Index must be within*");
    }

    [Fact]
    public void SalaryParser_MatchesMiddleOfNumber_Bug()
    {
        string input = "1000 - 2000 zł";
        
        var result = SalaryParser.Parse(input);
        
        result.from.Should().Be(1000M, "It should match the full number, not a partial match from the middle");
    }

    [Fact]
    public void SalaryParser_WhitespaceInThousands_Bug()
    {
        string input = "3 500 do 4500 PLN";
        var result = SalaryParser.Parse(input);
        
        result.from.Should().Be(3500M, "Should handle space as thousands separator");
    }

    [Fact]
    public void HeadlessBrowser_UninitializedSettings_ThrowsOnCtor()
    {
        HeadlessbrowserSettings.Host = null;
        HeadlessbrowserSettings.Port = 0;

        var action = () => new HeadlessBrowser();

        action.Should().Throw<UriFormatException>("Settings are not initialized");
    }
}
