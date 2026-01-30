using FluentAssertions;
using Offer_collector.Models.Tools;
using Xunit;

namespace offer_manager.Tests.Tools;

public class SalaryParserTests
{
    [Theory]
    [InlineData("1000 - 2000 zł", 1000, 2000)]
    [InlineData("3 500 do 4500 PLN", 3500, 4500)]
    public void Parse_StandardFormats_ReturnsCorrectValues(string input, decimal expectedFrom, decimal expectedTo)
    {
        var result = SalaryParser.Parse(input);
        result.from.Should().Be(expectedFrom);
        result.to.Should().Be(expectedTo);
    }

    [Fact]
    public void Parse_HighValueWithDotSeparator_ParsesCorrectly()
    {
        var input = "12.000 - 15.000 zł";

        var result = SalaryParser.Parse(input);
        
        result.from.Should().BeGreaterThan(100); 
    }

    [Fact]
    public void Parse_ValueWithCommaAndDot_HandleGracefully()
    {
        var input = "12.000,00 - 15.000,00 zł";
        
        var result = SalaryParser.Parse(input);
        
        result.from.Should().Be(12000, "Should handle full currency format");
    }
}
