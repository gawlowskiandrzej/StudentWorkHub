using FluentAssertions;
using Offer_collector.Models.Tools;
using Xunit;

namespace offer_manager.Tests.Tools
{
    public class ExtensionMethodsTests
    {
        [Fact]
        public void ReplaceAt_IndexOutOfBounds_ShouldHandleGracefullyOrThrowClearly()
        {
            string input = "Hello World";
            Action act = () => input.ReplaceAt(20, 5, "Test");

            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("*startIndex*");
        }

        [Fact]
        public void ReplaceAt_NegativeIndex_ShouldThrow()
        {
            string input = "Hello World";
            Action act = () => input.ReplaceAt(-1, 5, "Test");
            
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void RemoveDiacritics_HandlesNull_ReturnsNullOrEmpty()
        {
            string? input = null;

            string result = ExtensionMethods.RemoveDiacritics(input!);
            result.Should().BeNull();
        }
    }
}
