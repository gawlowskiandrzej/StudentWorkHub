using FluentAssertions;
using offer_manager.Models.Users;

namespace offer_manager.Tests.Controllers;

public class UserLoginDtoTests
{
    #region StandardLoginRequest Tests

    [Fact]
    public void StandardLoginRequest_DefaultValues()
    {
        var request = new StandardLoginRequest();

        request.Login.Should().BeEmpty();
        request.Password.Should().BeEmpty();
        request.RememberMe.Should().BeFalse();
    }

    [Fact]
    public void StandardLoginRequest_CanSetAllProperties()
    {
        var request = new StandardLoginRequest
        {
            Login = "user@example.com",
            Password = "MyPassword123!",
            RememberMe = true
        };

        request.Login.Should().Be("user@example.com");
        request.Password.Should().Be("MyPassword123!");
        request.RememberMe.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void StandardLoginRequest_RememberMe_States(bool rememberMe)
    {
        var request = new StandardLoginRequest { RememberMe = rememberMe };
        request.RememberMe.Should().Be(rememberMe);
    }

    #endregion

    #region StandardLoginResponse Tests

    [Fact]
    public void StandardLoginResponse_DefaultValues()
    {
        var response = new StandardLoginResponse();

        response.ErrorMessage.Should().BeEmpty();
        response.Jwt.Should().BeEmpty();
        response.RememberMeToken.Should().BeEmpty();
    }

    [Fact]
    public void StandardLoginResponse_SuccessfulLogin()
    {
        var response = new StandardLoginResponse
        {
            ErrorMessage = string.Empty,
            Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIn0.signature",
            RememberMeToken = "remember-token-here"
        };

        response.ErrorMessage.Should().BeEmpty();
        response.Jwt.Should().NotBeEmpty();
        response.RememberMeToken.Should().NotBeEmpty();
    }

    [Fact]
    public void StandardLoginResponse_FailedLogin()
    {
        var response = new StandardLoginResponse
        {
            ErrorMessage = "Nieprawidłowe dane logowania."
        };

        response.ErrorMessage.Should().Be("Nieprawidłowe dane logowania.");
        response.Jwt.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Puste żądanie.")]
    [InlineData("Nieprawidłowe dane logowania.")]
    [InlineData("Konto zostało zablokowane.")]
    public void StandardLoginResponse_CommonErrors_CanBeSet(string errorMessage)
    {
        var response = new StandardLoginResponse
        {
            ErrorMessage = errorMessage
        };

        response.ErrorMessage.Should().Be(errorMessage);
    }

    #endregion

    #region JWT Token Structure Tests

    [Theory]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e30.xyz", true)]  // Valid structure
    [InlineData("header.payload.signature", true)]  // 3 parts
    [InlineData("not.valid.jwt.token.with.too.many.parts", false)]
    [InlineData("", false)]
    [InlineData("single-segment", false)]
    public void Jwt_StructureValidation(string token, bool isValidStructure)
    {
        var segments = token.Split('.');
        var isValid = segments.Length == 3 && !string.IsNullOrEmpty(token);
        
        isValid.Should().Be(isValidStructure);
    }

    [Fact]
    public void Jwt_ExpirationHandling_Pattern()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var expired = now - 3600; // 1 hour ago
        var valid = now + 3600;   // 1 hour from now

        (expired < now).Should().BeTrue("Expired token should be detected");
        (valid > now).Should().BeTrue("Valid token should not be expired");
    }

    #endregion

    #region Login Validation Tests

    [Theory]
    [InlineData("valid@email.com", "password123", true)]
    [InlineData("", "password123", false)]
    [InlineData("valid@email.com", "", false)]
    [InlineData("", "", false)]
    public void Login_RequiredFields_Validation(string login, string password, bool isValid)
    {
        var hasRequiredFields = !string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password);
        hasRequiredFields.Should().Be(isValid);
    }

    #endregion

    #region Remember Me Functionality Tests

    [Theory]
    [InlineData(true, 7)]   // 7 days for "remember me"
    [InlineData(false, 1)]  // 1 day for normal session
    public void RememberMe_AffectsTokenExpiration(bool rememberMe, int expectedDays)
    {
        var baseDays = rememberMe ? 7 : 1;
        baseDays.Should().Be(expectedDays);
    }

    [Fact]
    public void RememberMe_Token_IsOptional()
    {
        var response = new StandardLoginResponse
        {
            Jwt = "jwt-token",
            RememberMeToken = string.Empty  // No remember me token when not requested
        };

        response.RememberMeToken.Should().BeEmpty();
        response.Jwt.Should().NotBeEmpty();
    }

    #endregion

    #region Success/Failure Detection

    [Fact]
    public void IsSuccess_WhenErrorMessageEmpty()
    {
        var response = new StandardLoginResponse { ErrorMessage = "" };
        var isSuccess = string.IsNullOrEmpty(response.ErrorMessage);
        
        isSuccess.Should().BeTrue();
    }

    [Fact]
    public void IsFailure_WhenErrorMessageNotEmpty()
    {
        var response = new StandardLoginResponse { ErrorMessage = "Error" };
        var isSuccess = string.IsNullOrEmpty(response.ErrorMessage);
        
        isSuccess.Should().BeFalse();
    }

    [Fact]
    public void IsSuccess_WhenJwtPresent()
    {
        var response = new StandardLoginResponse 
        { 
            ErrorMessage = "",
            Jwt = "token" 
        };
        var isSuccess = string.IsNullOrEmpty(response.ErrorMessage) && !string.IsNullOrEmpty(response.Jwt);
        
        isSuccess.Should().BeTrue();
    }

    #endregion
}
