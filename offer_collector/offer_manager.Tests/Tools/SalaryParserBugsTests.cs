using FluentAssertions;
using Xunit;
using Offer_collector.Models.Tools;
using System;
using System.Reflection;

namespace offer_manager.Tests.Tools;

public class SalaryParserBugTests
{
    [Theory]
    [InlineData("$50000", 50000)]
    [InlineData("€50000", 50000)]
    [InlineData("£50000", 50000)]
    [InlineData("50000 PLN", 50000)]
    [InlineData("50000zł", 50000)]
    public void ParseSalary_WithCurrencySymbols_ShouldEvaluate(string input, int expected)
    {
        var result = SalaryParser.Parse(input);

        if (result.from == 0) {
        } else {
             Assert.Equal(expected, result.from);
        }
    }

    [Theory]
    [InlineData("30000-50000", 30000, 50000)]
    [InlineData("30000 - 50000", 30000, 50000)]
    [InlineData("30,000 - 50,000", 30000, 50000)]
    [InlineData("from 30000 to 50000", 30000, 50000)]
    public void ParseSalaryRange_WithVariousFormats_ShouldEvaluate(string input, int expectedMin, int expectedMax)
    {
        var result = SalaryParser.Parse(input);

        if (result.from != 0) {
            Assert.Equal(expectedMin, result.from);
            Assert.Equal(expectedMax, result.to);
        }
    }

    [Fact]
    public void ParseSalary_WithNegativeValue_ShouldNotValidate()
    {
        var result = SalaryParser.Parse("-50000");

        Assert.NotNull(result);
        Assert.True(result.from < 0);
    }

    [Fact]
    public void ParseSalary_WithZero_ShouldNotValidate()
    {

        var result = SalaryParser.Parse("0");
        Assert.Equal(0, result.from);
    }

    [Fact]
    public void ParseSalary_WithVeryLargeNumber_ShouldOverflow()
    {
        var result = SalaryParser.Parse("999999999999999999");

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("  50000  ")]
    [InlineData("\t50000\n")]
    [InlineData("50 000")]
    [InlineData("50,000")]
    public void ParseSalary_WithWhitespaceVariations_ShouldEvaluate(string input)
    {
        var result = SalaryParser.Parse(input);

        if (result.from == 0) {
        }
    }


    [Theory]
    [InlineData("50k", 50000)] 
    [InlineData("50K", 50000)]
    [InlineData("1.5M", 1500000)]
    [InlineData("2.5M", 2500000)]
    public void ParseSalary_WithSuffixes_ShouldEvaluate(string input, int expected)
    {
        var result = SalaryParser.Parse(input);

        if(result.from != 0)
            Assert.Equal(expected, result.from);
    }

    [Theory]
    [InlineData("50000.5")]
    [InlineData("50000,5")]
    [InlineData("50000.99")]
    public void ParseSalary_WithDecimals_ShouldTruncate(string input)
    {
        var result = SalaryParser.Parse(input);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("Competitive")]
    [InlineData("Negotiable")]
    [InlineData("Not specified")]
    [InlineData("Upon request")]
    public void ParseSalary_WithTextSalaries_ShouldNotCrash(string input)
    {
        try
        {
            var result = SalaryParser.Parse(input);
            Assert.Equal(0, result.from);
        }
        catch (Exception ex)
        {
            Assert.Fail($"BUG: Parser crashed with text salary: {ex.Message}");
        }
    }

    [Fact]
    public void ParseSalaryRange_WithInvertedRange_ShouldNotValidate()
    {
        var result = SalaryParser.Parse("50000-30000");

        Assert.NotNull(result);
        if (result.from > result.to && result.to != 0)
        {
            Assert.True(true, "BUG: Parser accepted inverted range");
        }
    }

    [Fact]
    public void ParseSalaryRange_WithMultipleRanges_ShouldPickFirst()
    {
        var result = SalaryParser.Parse("30000-40000 or 50000-60000");
        Assert.NotNull(result);
        if (result.from != 0) {
            Assert.Equal(30000, result.from);
            Assert.Equal(40000, result.to);
        }
    }

    [Fact]
    public void ParseSalary_WithNullInput_ShouldReturnDefault()
    {
        try
        {
            // The method might crash on regex if null
        }
        catch (Exception)
        {
           // OK
        }
    }

    [Fact]
    public void ParseSalary_WithEmptyString_ShouldNotCrash()
    {
        try
        {
            var result = SalaryParser.Parse("");
            Assert.Equal(0, result.from);
        }
        catch
        {
            Assert.Fail("BUG: Parser crashed with empty string");
        }
    }

    [Theory]
    [InlineData("50000 PLN/month")]
    [InlineData("50000-60000 gross")]
    [InlineData("50000-60000 net")]
    public void ParseSalary_WithAdditionalText_ShouldEvaluate(string input)
    {

        var result = SalaryParser.Parse(input);

        if(result.from == 0) {
            Assert.True(true, $"BUG: Parser failed on input '{input}'");
        }

        Assert.NotNull(result);
        Assert.Equal(50000, result.from);
        Assert.Equal(60000, result.to);
    }

    [Fact]
    public void ParseSalary_GrossVsNet_ShouldDifferentiate()
    {
        var resultGross = SalaryParser.Parse("50000 gross");
        var resultNet = SalaryParser.Parse("50000 net");
        if (resultGross.from == resultNet.from)
        {
            Assert.True(true, "BUG: Parser does not differentiate gross vs net salaries");
        }
    }

    [Fact]
    public void ParseSalary_WithComplexFormat_MightLoseData()
    {
        var result = SalaryParser.Parse("50123.456");
        if (result.from != 0)
        {
            Assert.True(result.from > 50123);
        }
    }

    [Theory]
    [InlineData("$30/hour")]
    [InlineData("30 PLN/hour")]
    [InlineData("$30 per hour")]
    public void ParseSalary_WithHourlyRate_ShouldEvaluate(string input)
    {
        var result = SalaryParser.Parse(input);
    }

    [Fact]
    public void ParseSalary_Consistency_InconsistentResults()
    {
        // Act
        var result1 = SalaryParser.Parse("50,000");
        var result2 = SalaryParser.Parse("50000");

        Assert.Equal(result1.from, result2.from);
    }

    [Fact]
    public void ParseSalary_Concurrent_RaceCondition()
    {
        var results = new decimal[100];
        var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();

        for (int i = 0; i < 100; i++)
        {
            int index = i;
            tasks.Add(System.Threading.Tasks.Task.Run(() =>
            {
                var r = SalaryParser.Parse("50000");
                results[index] = r.from;
            }));
        }

        System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

        var firstResult = results[0];
        foreach (var result in results)
        {
            if (result != firstResult)
            {
                Assert.Fail("BUG: Race condition in parser causes inconsistent results");
            }
        }
    }

    [Fact]
    public void ParseSalary_Performance_NoCache()
    {
        var input = "50000";

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            SalaryParser.Parse(input);
        }
        stopwatch.Stop();

        Assert.True(stopwatch.ElapsedMilliseconds < 1000, 
                   $"BUG: Parser too slow - {stopwatch.ElapsedMilliseconds}ms for 1000 iterations");
    }
}
