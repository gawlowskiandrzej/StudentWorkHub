using Users;
using FluentAssertions;

namespace offer_manager.Tests.Controllers;

public class UserPasswordPolicyTests
{
    [Theory]
    [InlineData("short", false)]
    [InlineData("nocaps123!", false)]
    [InlineData("NOLOWER123!", false)]
    [InlineData("NoDigits!!!", false)]
    [InlineData("ValidP@ss123", true)]
    public void ValidatePassword_CompliesWithPolicy(string password, bool expectedValid)
    {
        var policy = new UserPasswordPolicy();

        var (isValid, error) = policy.ValidatePassword(password);

        isValid.Should().Be(expectedValid);
        if (expectedValid)
            error.Should().BeNullOrEmpty();
        else
            error.Should().NotBeNullOrEmpty();
    }
}
