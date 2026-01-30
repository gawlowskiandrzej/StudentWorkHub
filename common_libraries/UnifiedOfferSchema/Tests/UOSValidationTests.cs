using Xunit;
using UnifiedOfferSchema;
using System.Text.Json;
using FluentAssertions;

namespace common_libraries.Tests
{
    public class UOSValidationTests
    {
        [Fact]
        public void BuildFromString_MissingRequiredField_ShouldThrowUOSException()
        {
            var malformedJson = "{ \"id\": 1, \"source\": \"test\", \"url\": \"http://test.pl\" }";

            var act = () => UOSUtils.BuildFromString(malformedJson);

            act.Should().Throw<UOSException>().WithMessage("*jobTitle*");
        }

        [Fact]
        public void BuildFromString_InvalidNumericType_ShouldThrowUOSException()
        {
            var malformedJson = "{ \"id\": { \"nested\": 1 }, \"source\": \"test\", \"url\": \"http://test.pl\", \"jobTitle\": \"Test\" }";

            var act = () => UOSUtils.BuildFromString(malformedJson);

            act.Should().Throw<UOSException>();
        }

        [Fact]
        public void BuildFromString_EmptyString_ShouldThrowUOSException()
        {
            var act = () => UOSUtils.BuildFromString("");

            act.Should().Throw<UOSException>()
               .WithMessage("Input offer is empty.");
        }

        [Fact]
        public void MergeWith_ShouldPrioritizeNotEmptyFields()
        {
            var baseOffer = new UOS { JobTitle = "Base Title", Description = "Base Desc" };
            var mergeJson = "{ \"jobTitle\": \"New Title\", \"description\": \"\" }";

            var result = baseOffer.MergeWith(mergeJson);

            result.JobTitle.Should().Be("New Title");
            result.Description.Should().Be("Base Desc");
        }
    }
}
