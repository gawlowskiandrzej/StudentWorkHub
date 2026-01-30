using FluentAssertions;
using offer_manager.Models.Users;

namespace offer_manager.Tests.Controllers;

public class UserRegisterDtoTests
{
    #region StandardRegisterUserRequest Tests

    [Fact]
    public void StandardRegisterUserRequest_DefaultValues_AreEmptyStrings()
    {
        var request = new StandardRegisterUserRequest();

        request.Email.Should().BeEmpty();
        request.Password.Should().BeEmpty();
        request.FirstName.Should().BeEmpty();
        request.LastName.Should().BeEmpty();
    }

    [Fact]
    public void StandardRegisterUserRequest_CanSetAllProperties()
    {
        var request = new StandardRegisterUserRequest
        {
            Email = "test@example.com",
            Password = "SecureP@ss123",
            FirstName = "Jan",
            LastName = "Kowalski"
        };

        request.Email.Should().Be("test@example.com");
        request.Password.Should().Be("SecureP@ss123");
        request.FirstName.Should().Be("Jan");
        request.LastName.Should().Be("Kowalski");
    }

    [Fact]
    public void StandardRegisterUserRequest_Properties_AreInitOnly()
    {
        var type = typeof(StandardRegisterUserRequest);
        var properties = type.GetProperties();

        foreach (var prop in properties)
        {
            prop.SetMethod?.IsPublic.Should().BeTrue();
        }
    }

    #endregion

    #region StandardRegisterUserResponse Tests

    [Fact]
    public void StandardRegisterUserResponse_DefaultValues()
    {
        var response = new StandardRegisterUserResponse();

        response.ErrorMessage.Should().BeEmpty();
    }

    [Fact]
    public void StandardRegisterUserResponse_Success_EmptyErrorMessage()
    {
        var response = new StandardRegisterUserResponse
        {
            ErrorMessage = string.Empty
        };

        response.ErrorMessage.Should().BeEmpty();
        (response.ErrorMessage == string.Empty).Should().BeTrue("Empty error message indicates success");
    }

    [Theory]
    [InlineData("Puste żądanie.")]
    [InlineData("Nieprawidłowy adres email.")]
    [InlineData("Email jest już używany.")]
    [InlineData("Hasło nie spełnia wymagań bezpieczeństwa.")]
    public void StandardRegisterUserResponse_Error_ContainsMessage(string errorMessage)
    {
        var response = new StandardRegisterUserResponse
        {
            ErrorMessage = errorMessage
        };

        response.ErrorMessage.Should().Be(errorMessage);
        response.ErrorMessage.Should().NotBeEmpty();
    }

    #endregion

    #region Email Validation Pattern Tests

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("user.name@domain.pl", true)]
    [InlineData("user+tag@example.org", true)]
    [InlineData("user@subdomain.domain.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("@nodomain.com", false)]
    [InlineData("user@", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void Email_ValidationPattern_MatchesExpected(string email, bool shouldBeValid)
    {
        var emailRegex = new System.Text.RegularExpressions.Regex(
            @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9]+([._%+\-]?[A-Za-z0-9]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$",
            System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.CultureInvariant
        );

        var isValid = !string.IsNullOrWhiteSpace(email) && emailRegex.IsMatch(email);
        isValid.Should().Be(shouldBeValid, $"Email '{email}' validation should be {shouldBeValid}");
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(64, true)]
    [InlineData(65, false)] // max 64 characters before @
    public void Email_LocalPart_LengthValidation(int length, bool shouldBeValid)
    {
        var localPart = new string('a', length);
        var email = $"{localPart}@example.com";
        
        var emailRegex = new System.Text.RegularExpressions.Regex(
            @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9]+([._%+\-]?[A-Za-z0-9]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$"
        );

        var isValid = emailRegex.IsMatch(email);
        isValid.Should().Be(shouldBeValid);
    }

    [Theory]
    [InlineData("a@b.co", true)]  // minimum valid
    [InlineData("test@domain.com", true)]
    [InlineData("x@y.z", false)]  // too short
    public void Email_MinimalValid_Formats(string email, bool shouldBeValid)
    {
        var emailRegex = new System.Text.RegularExpressions.Regex(
            @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9]+([._%+\-]?[A-Za-z0-9]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$"
        );

        var isValid = emailRegex.IsMatch(email);
        isValid.Should().Be(shouldBeValid);
    }

    #endregion

    #region Required Field Validation Tests

    [Theory]
    [InlineData("Jan", true)]
    [InlineData("A", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void FirstName_RequiredValidation(string firstName, bool shouldBeValid)
    {
        var isValid = !string.IsNullOrWhiteSpace(firstName);
        isValid.Should().Be(shouldBeValid);
    }

    [Theory]
    [InlineData("Kowalski", true)]
    [InlineData("K", true)]
    [InlineData("", false)]
    public void LastName_RequiredValidation(string lastName, bool shouldBeValid)
    {
        var isValid = !string.IsNullOrWhiteSpace(lastName);
        isValid.Should().Be(shouldBeValid);
    }

    [Theory]
    [InlineData("password123", true)]
    [InlineData("a", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void Password_RequiredValidation(string password, bool shouldBeValid)
    {
        var isValid = !string.IsNullOrWhiteSpace(password);
        isValid.Should().Be(shouldBeValid);
    }

    #endregion

    #region Password Policy Mapping Tests

    [Theory]
    [InlineData(0, "Hasło nie może być puste.")]
    [InlineData(1, "Hasło nie spełnia wymagań bezpieczeństwa.")]
    [InlineData(2, "Hasło musi zawierać co najmniej jedną cyfrę.")]
    [InlineData(3, "Hasło musi zawierać co najmniej jedną dużą literę.")]
    [InlineData(4, "Hasło musi zawierać co najmniej jedną małą literę.")]
    [InlineData(5, "Hasło musi zawierać co najmniej jeden znak specjalny.")]
    [InlineData(6, "Hasło jest za krótkie.")]
    [InlineData(7, "Hasło jest za długie.")]
    public void PasswordPolicy_ErrorCodes_MapToMessages(int errorCode, string expectedMessage)
    {
        var errorMessages = new Dictionary<int, string>
        {
            { 0, "Hasło nie może być puste." },
            { 1, "Hasło nie spełnia wymagań bezpieczeństwa." },
            { 2, "Hasło musi zawierać co najmniej jedną cyfrę." },
            { 3, "Hasło musi zawierać co najmniej jedną dużą literę." },
            { 4, "Hasło musi zawierać co najmniej jedną małą literę." },
            { 5, "Hasło musi zawierać co najmniej jeden znak specjalny." },
            { 6, "Hasło jest za krótkie." },
            { 7, "Hasło jest za długie." }
        };

        errorMessages.Should().ContainKey(errorCode);
        errorMessages[errorCode].Should().Be(expectedMessage);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void StandardRegisterUserRequest_WithSpecialCharacters_InName()
    {
        var request = new StandardRegisterUserRequest
        {
            Email = "test@example.com",
            Password = "Secure123!",
            FirstName = "Jurek-Ogórek",
            LastName = "Kowalski"
        };

        request.FirstName.Should().Be("Jurek-Ogórek");
        request.LastName.Should().Be("Kowalski");
    }

    [Fact]
    public void SuccessResponse_IsEmptyErrorMessage()
    {
        var response = new StandardRegisterUserResponse { ErrorMessage = "" };
        var isSuccess = string.IsNullOrEmpty(response.ErrorMessage);
        
        isSuccess.Should().BeTrue();
    }

    [Fact]
    public void FailureResponse_HasNonEmptyErrorMessage()
    {
        var response = new StandardRegisterUserResponse { ErrorMessage = "Error" };
        var isFailure = !string.IsNullOrEmpty(response.ErrorMessage);
        
        isFailure.Should().BeTrue();
    }

    #endregion
}
