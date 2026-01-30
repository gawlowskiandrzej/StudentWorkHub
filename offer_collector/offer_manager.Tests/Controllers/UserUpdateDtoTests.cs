using FluentAssertions;
using offer_manager.Models.Users;

namespace offer_manager.Tests.Controllers;

public class UserUpdateDtoTests
{
    #region UpdateDataRequest Tests

    [Fact]
    public void UpdateDataRequest_DefaultValues()
    {
        var request = new UpdateDataRequest();

        request.Jwt.Should().BeEmpty();
        request.UserFirstName.Should().BeNull();
        request.UserSecondName.Should().BeNull();
        request.UserLastName.Should().BeNull();
        request.UserPhone.Should().BeNull();
    }

    [Fact]
    public void UpdateDataRequest_CanSetAllProperties()
    {
        var request = new UpdateDataRequest
        {
            Jwt = "jwt-token",
            UserFirstName = "Jurek",
            UserSecondName = "Ogórek",
            UserLastName = "Nowak",
            UserPhone = "+48123456789"
        };

        request.Jwt.Should().Be("jwt-token");
        request.UserFirstName.Should().Be("Jurek");
        request.UserSecondName.Should().Be("Ogórek");
        request.UserLastName.Should().Be("Nowak");
        request.UserPhone.Should().Be("+48123456789");
    }

    [Fact]
    public void UpdateDataRequest_PartialUpdate_OnlyChangedFields()
    {
        // Only updating first name - other fields should remain null
        var request = new UpdateDataRequest
        {
            Jwt = "jwt-token",
            UserFirstName = "Nowe Imie"
        };

        request.Jwt.Should().NotBeNullOrEmpty();
        request.UserFirstName.Should().Be("Nowe Imie");
        request.UserSecondName.Should().BeNull();
        request.UserLastName.Should().BeNull();
        request.UserPhone.Should().BeNull();
    }

    [Theory]
    [InlineData("+48123456789", true)]
    [InlineData("+1234567890", true)]
    [InlineData("+442071234567", true)]
    [InlineData("123456789", false)]
    [InlineData("048123456789", false)]
    [InlineData("+abc123456", false)]
    [InlineData("", false)]
    public void PhoneNumber_ValidationPattern(string phoneNumber, bool shouldBeValid)
    {
        var phoneRegex = new System.Text.RegularExpressions.Regex(
            @"^\+\d{1,15}$",
            System.Text.RegularExpressions.RegexOptions.Compiled
        );

        var isValid = !string.IsNullOrEmpty(phoneNumber) && phoneRegex.IsMatch(phoneNumber);
        isValid.Should().Be(shouldBeValid, $"Phone '{phoneNumber}' validation should be {shouldBeValid}");
    }

    #endregion

    #region UpdateDataResponse Tests

    [Fact]
    public void UpdateDataResponse_DefaultValues()
    {
        var response = new UpdateDataResponse();

        response.ErrorMessage.Should().BeEmpty();
        response.UserFirstNameUpdated.Should().BeFalse();
        response.UserSecondNameUpdated.Should().BeFalse();
        response.UserLastNameUpdated.Should().BeFalse();
        response.UserPhoneUpdated.Should().BeFalse();
    }

    [Fact]
    public void UpdateDataResponse_Success_AllFieldsUpdated()
    {
        var response = new UpdateDataResponse
        {
            ErrorMessage = string.Empty,
            UserFirstNameUpdated = true,
            UserLastNameUpdated = true,
            UserPhoneUpdated = true
        };

        response.ErrorMessage.Should().BeEmpty();
        response.UserFirstNameUpdated.Should().BeTrue();
        response.UserLastNameUpdated.Should().BeTrue();
        response.UserPhoneUpdated.Should().BeTrue();
    }

    [Fact]
    public void UpdateDataResponse_PartialSuccess()
    {
        var response = new UpdateDataResponse
        {
            ErrorMessage = string.Empty,
            UserFirstNameUpdated = true,
            UserLastNameUpdated = false
        };

        response.ErrorMessage.Should().BeEmpty();
        response.UserFirstNameUpdated.Should().BeTrue();
        response.UserLastNameUpdated.Should().BeFalse();
    }

    [Fact]
    public void UpdateDataResponse_Failure()
    {
        var response = new UpdateDataResponse
        {
            ErrorMessage = "Token wygasl."
        };

        response.ErrorMessage.Should().Be("Token wygasl.");
    }

    #endregion

    #region ChangePasswordRequest Tests

    [Fact]
    public void ChangePasswordRequest_DefaultValues()
    {
        var request = new ChangePasswordRequest();

        request.Jwt.Should().BeEmpty();
        request.NewPassword.Should().BeEmpty();
    }

    [Fact]
    public void ChangePasswordRequest_CanSetAllProperties()
    {
        var request = new ChangePasswordRequest
        {
            Jwt = "jwt-token",
            NewPassword = "NewP@ss456!"
        };

        request.Jwt.Should().Be("jwt-token");
        request.NewPassword.Should().Be("NewP@ss456!");
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("newpassword", true)]
    public void ChangePassword_RequiredFields(string newPassword, bool isValid)
    {
        var hasRequiredFields = !string.IsNullOrWhiteSpace(newPassword);
        hasRequiredFields.Should().Be(isValid);
    }

    #endregion

    #region ChangePasswordResponse Tests

    [Fact]
    public void ChangePasswordResponse_DefaultValues()
    {
        var response = new ChangePasswordResponse();

        response.ErrorMessage.Should().BeEmpty();
    }

    [Fact]
    public void ChangePasswordResponse_Success()
    {
        var response = new ChangePasswordResponse
        {
            ErrorMessage = string.Empty
        };

        response.ErrorMessage.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Nieprawidlowe stare haslo.")]
    [InlineData("Nowe haslo nie spelnia wymagan bezpieczenstwa.")]
    [InlineData("Token wygasl.")]
    public void ChangePasswordResponse_Errors(string errorMessage)
    {
        var response = new ChangePasswordResponse
        {
            ErrorMessage = errorMessage
        };

        response.ErrorMessage.Should().Be(errorMessage);
    }

    #endregion

    #region Name Validation Tests

    [Theory]
    [InlineData("Jan", true)]
    [InlineData("Jan-Pawel", true)]
    [InlineData("O'Connor", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void Name_Validation_Patterns(string name, bool isValid)
    {
        var result = !string.IsNullOrWhiteSpace(name);
        result.Should().Be(isValid);
    }

    [Fact]
    public void NullFields_MeanNoUpdate()
    {
        var request = new UpdateDataRequest
        {
            Jwt = "token",
            UserFirstName = null,  // should not update
            UserLastName = "Nowak" // should update
        };

        request.UserFirstName.Should().BeNull("Null means do not update");
        request.UserLastName.Should().NotBeNull("Non-null means update");
    }

    #endregion

    #region JWT Required Tests

    [Theory]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("valid-jwt", true)]
    public void Jwt_Required_ForUpdateOperations(string jwt, bool isValid)
    {
        var hasJwt = !string.IsNullOrWhiteSpace(jwt);
        hasJwt.Should().Be(isValid);
    }

    #endregion
}
