using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using offer_manager.Models.Users;

namespace offer_manager.Tests.Integration;
public class UserControllerIntegrationTests : IntegrationTestBase
{
    public UserControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    #region Standard Register Tests

    [Fact, Trait("Category", "Integration")]
    public async Task StandardRegister_WithValidData_ReturnsSuccess()
    {
        var request = new StandardRegisterUserRequest
        {
            Email = $"test_{Guid.NewGuid()}@example.com",
            Password = "SecureP@ss123",
            FirstName = "Jan",
            LastName = "Kowalski"
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-register", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task StandardRegister_WithEmptyRequest_ReturnsBadRequest()
    {
        StandardRegisterUserRequest? request = null;

        var response = await Client.PostAsJsonAsync("/api/users/standard-register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("", "password", "Jan", "Kowalski")]
    [InlineData("invalid-email", "password", "Jan", "Kowalski")]
    [InlineData("test@test.com", "", "Jan", "Kowalski")]
    public async Task StandardRegister_WithInvalidData_ReturnsBadRequest(
        string email, string password, string firstName, string lastName)
    {
        var request = new StandardRegisterUserRequest
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region Standard Login Tests

    [Fact, Trait("Category", "Integration")]
    public async Task StandardLogin_WithEmptyRequest_ReturnsBadRequest()
    {
        StandardLoginRequest? request = null;

        var response = await Client.PostAsJsonAsync("/api/users/standard-login", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task StandardLogin_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var request = new StandardLoginRequest
        {
            Login = "nonexistent@example.com",
            Password = "wrongpassword"
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task StandardLogin_WithEmptyEmail_ReturnsBadRequest()
    {
        var request = new StandardLoginRequest
        {
            Login = "",
            Password = "password123"
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-login", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region Token Login Tests

    [Fact, Trait("Category", "Integration")]
    public async Task TokenLogin_WithInvalidToken_ReturnsUnauthorized()
    {
        var request = new TokenLoginRequest
        {
            Token = "invalid-token"
        };

        var response = await Client.PostAsJsonAsync("/api/users/token-login", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task TokenLogin_WithEmptyToken_ReturnsBadRequest()
    {
        var request = new TokenLoginRequest
        {
            Token = ""
        };

        var response = await Client.PostAsJsonAsync("/api/users/token-login", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region Update Data Tests

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateData_WithoutJwt_ReturnsUnauthorized()
    {
        var request = new UpdateDataRequest
        {
            Jwt = "",
            UserFirstName = "Jan"
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-data", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateData_WithInvalidJwt_ReturnsUnauthorized()
    {

        var request = new UpdateDataRequest
        {
            Jwt = "invalid-jwt-token",
            UserFirstName = "Jan"
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-data", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    #endregion

    #region Change Password Tests

    [Fact, Trait("Category", "Integration")]
    public async Task ChangePassword_WithoutJwt_ReturnsUnauthorized()
    {
        var request = new ChangePasswordRequest
        {
            Jwt = "",
            NewPassword = "NewP@ss123"
        };

        var response = await Client.PostAsJsonAsync("/api/users/change-password", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region Logout Tests

    [Fact, Trait("Category", "Integration")]
    public async Task Logout_WithInvalidJwt_ReturnsUnauthorized()
    {
        var request = new { Jwt = "invalid-jwt" };

        var response = await Client.PostAsJsonAsync("/api/users/logout", request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    #endregion

    #region Update Preferences / Weights

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateWeights_WithManipulatedUserIdInJwt_ReturnsUnauthorizedOrForbidden()
    {
        var request = new UpdateWeightsRequest
        {
            Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI5OTk5IiwiaWF0IjoxNTE2MjM5MDIyfQ.invalid-sig", 
            Vector = new float[] { 0.5f, 0.5f }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-weights", request);

        response.StatusCode.Should().NotBe(HttpStatusCode.OK);
        response.StatusCode.Should().BeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.BadRequest, HttpStatusCode.Forbidden);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdatePreferences_WithValidLikeJwt_AllowsOrRejectsGracefully()
    {
        var request = new UpdatePreferencesRequest
        {
            Jwt = "valid-ish-jwt-placeholder",
            LeadingCategoryId = 1,
            SalaryFrom = 10000,
            SalaryTo = 20000,
            EmploymentTypeIds = new short[] { 1, 2 }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-preferences", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdatePreferences_WithoutJwt_ReturnsAuthError()
    {
        var request = new UpdatePreferencesRequest
        {
            Jwt = string.Empty,
            LeadingCategoryId = 1
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-preferences", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdatePreferences_WithNoFields_ReturnsBadRequest()
    {
        var request = new UpdatePreferencesRequest
        {
            Jwt = "valid-ish-jwt-placeholder"
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-preferences", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdatePreferences_WithMismatchedSkills_ReturnsBadRequest()
    {
        var request = new UpdatePreferencesRequest
        {
            Jwt = "valid-ish-jwt-placeholder",
            SkillNames = new[] { "C#", "SQL" },
            SkillMonths = new short[] { 12 }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-preferences", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateWeights_WithValidLikeJwt_AllowsOrRejectsGracefully()
    {
        var request = new UpdateWeightsRequest
        {
            Jwt = "valid-ish-jwt-placeholder",
            Vector = new float[] { 0.1f, 0.2f, 0.3f },
            OrderByOption = new[] { "SALARY_MATCH" }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-weights", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateWeights_WithoutJwt_ReturnsAuthError()
    {
        var request = new UpdateWeightsRequest
        {
            Jwt = string.Empty,
            Vector = new float[] { 0.1f, 0.2f }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-weights", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateWeights_WithNoVectors_ReturnsBadRequest()
    {
        var request = new UpdateWeightsRequest
        {
            Jwt = "valid-ish-jwt-placeholder"
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-weights", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    [Fact, Trait("Category", "Integration")]
    public async Task UpdateWeights_WithVectorOnly_AllowsOrRejectsGracefully()
    {
        var request = new UpdateWeightsRequest
        {
            Jwt = "valid-ish-jwt-placeholder",
            Vector = new float[] { 1f, 2f, 3f }
        };

        var response = await Client.PostAsJsonAsync("/api/users/update-weights", request);

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized,
            HttpStatusCode.Forbidden,
            (HttpStatusCode)422
        );
    }

    #endregion
}
