using System.Collections.Frozen;
using FluentAssertions;
using OffersConnector;
using Xunit;

namespace offer_manager.Tests.OffersConnector
{
    public class ResultParsersTests
    {
        [Fact]
        public void RestrictionsParser_NullInput_ReturnsEmptyList()
        {
            var result = ResultParsers.RestrictionsParser(null);
            result.Should().BeEmpty();
        }

        [Fact]
        public void RestrictionsParser_EmptyInput_ReturnsEmptyList()
        {
            var result = ResultParsers.RestrictionsParser(new Dictionary<string, List<string>>());
            result.Should().BeEmpty();
        }

        [Fact]
        public void RestrictionsParser_GeneratesCorrectInstructions()
        {
            var input = new Dictionary<string, List<string>>
            {
                { "ContractType", new List<string> { "B2B", "UoP" } }
            };

            var result = ResultParsers.RestrictionsParser(input);

            result.Should().NotBeEmpty();
            result[0].Should().Contain("You must follow the rules below with maximum strictness");
            result[1].Should().Contain("ContractType");
            result[1].Should().Contain("B2B");
            result[1].Should().Contain("UoP");
        }

        [Fact]
        public void RestrictionsParser_NullValuesInDictionary_ShouldHandleGracefully()
        {
            var input = new Dictionary<string, List<string>>
            {
                { "ContractType", null! } 
            };

            var result = ResultParsers.RestrictionsParser(input);
            
            result.Count.Should().Be(1);
        }
    }
}
