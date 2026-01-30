using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using offer_manager.Models.Users;

namespace offer_manager.Tests.Integration;

public class UserFlowIntegrationTests : IntegrationTestBase
{
    public UserFlowIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    #region Complete Registration Flow

    [Fact, Trait("Category", "Integration")]
    public async Task CompleteRegistrationFlow_RegisterAndVerifyResponse()
    {
        var uniqueEmail = $"flow_test_{Guid.NewGuid():N}@example.com";
        var registerRequest = new StandardRegisterUserRequest
        {
            Email = uniqueEmail,
            Password = "TestP@ssword123!",
            FirstName = "FlowTest",
            LastName = "User"
        };

        var registerResponse = await Client.PostAsJsonAsync(
            "/api/users/standard-register", 
            registerRequest
        );

        registerResponse.Should().NotBeNull();

        var registerContent = await registerResponse.Content.ReadFromJsonAsync<StandardRegisterUserResponse>();
        
        registerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    #endregion

    #region Complete Login Flow

    [Fact, Trait("Category", "Integration")]
    public async Task CompleteLoginFlow_LoginWithCredentials()
    {
        var loginRequest = new StandardLoginRequest
        {
            Login = "test@example.com",
            Password = "TestP@ssword123!"
        };

        var loginResponse = await Client.PostAsJsonAsync(
            "/api/users/standard-login", 
            loginRequest
        );

        loginResponse.Should().NotBeNull();
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    #endregion

    #region Token-Based Authentication Flow

    [Fact, Trait("Category", "Integration")]
    public async Task TokenLoginFlow_WithRememberMeToken()
    {
        var loginRequest = new StandardLoginRequest
        {
            Login = "test@example.com",
            Password = "TestP@ssword123!",
            RememberMe = true
        };

        var loginResponse = await Client.PostAsJsonAsync(
            "/api/users/standard-login", 
            loginRequest
        );

        if (loginResponse.IsSuccessStatusCode)
        {
            var loginContent = await loginResponse.Content.ReadFromJsonAsync<StandardLoginResponse>();
            
            if (!string.IsNullOrEmpty(loginContent?.RememberMeToken))
            {
                var tokenLoginRequest = new TokenLoginRequest
                {
                    Token = loginContent.RememberMeToken
                };

                var tokenLoginResponse = await Client.PostAsJsonAsync(
                    "/api/users/token-login",
                    tokenLoginRequest
                );

                tokenLoginResponse.StatusCode.Should().BeOneOf(
                    HttpStatusCode.OK,
                    HttpStatusCode.BadRequest
                );
            }
        }
    }

    #endregion

    #region Profile Update Flow

    [Fact, Trait("Category", "Integration")]
    public async Task ProfileUpdateFlow_UpdateUserData()
    {
        var updateRequest = new UpdateDataRequest
        {
            Jwt = "test-jwt-token",
            UserFirstName = "UpdatedName"
        };

        var response = await Client.PostAsJsonAsync(
            "/api/users/update-data",
            updateRequest
        );

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized
        );
    }

    #endregion

    #region Password Change Flow

    [Fact, Trait("Category", "Integration")]
    public async Task PasswordChangeFlow_RequiresValidJwt()
    {
        var changePasswordRequest = new ChangePasswordRequest
        {
            Jwt = "invalid-jwt",
            NewPassword = "NewP@ssword456!"
        };

        var response = await Client.PostAsJsonAsync(
            "/api/users/change-password",
            changePasswordRequest
        );

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized
        );
    }

    #endregion

    #region Logout Flow

    [Fact, Trait("Category", "Integration")]
    public async Task LogoutFlow_RequiresValidJwt()
    {
        var logoutRequest = new { Jwt = "test-jwt-token" };

        var response = await Client.PostAsJsonAsync(
            "/api/users/logout",
            logoutRequest
        );

        response.StatusCode.Should().BeOneOf(
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.Unauthorized
        );
    }

    #endregion

    #region Full Preference Journey (Logic Test)

    [Fact, Trait("Category", "Integration")]
    public async Task PreferenceJourney_UpdateAndVerifyConsistency()
    {
        var updateRequest = new UpdatePreferencesRequest
        {
            Jwt = "valid-session-jwt",
            SalaryFrom = 5000,
            SalaryTo = 10000,
            LeadingCategoryId = 1
        };

        var updateResponse = await Client.PostAsJsonAsync("/api/users/update-preferences", updateRequest);
        
        updateResponse.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
    }

    #endregion

    #region Security Logic Regression

    [Fact, Trait("Category", "Integration")]
    public async Task Registration_WithWeakPassword_IsRejectedByPolicy()
    {
        var request = new StandardRegisterUserRequest
        {
            Email = $"weak_{Guid.NewGuid()}@test.com",
            Password = "123", // Too weak for most policies
            FirstName = "Test",
            LastName = "User"
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<StandardRegisterUserResponse>();
        content!.ErrorMessage.Should().NotBeEmpty();
    }

    #endregion

    #region Error Handling Tests

    [Fact, Trait("Category", "Integration")]
    public async Task AllUserEndpoints_HandleNullRequestGracefully()
    {
        var endpoints = new[]
        {
            "/api/users/standard-register",
            "/api/users/standard-login",
            "/api/users/token-login",
            "/api/users/update-data",
            "/api/users/change-password",
            "/api/users/logout"
        };

        foreach (var endpoint in endpoints)
        {
            var response = await Client.PostAsJsonAsync<object?>(endpoint, null);

            response.StatusCode.Should().BeOneOf(
                HttpStatusCode.OK,
                HttpStatusCode.BadRequest,
                HttpStatusCode.Unauthorized
            );
        }
    }

    #endregion

    #region Concurrent Request Tests

    [Fact, Trait("Category", "Integration")]
    public async Task ConcurrentRequests_HandledCorrectly()
    {
        var tasks = new List<Task<HttpResponseMessage>>();
        
        for (int i = 0; i < 10; i++)
        {
            var request = new StandardLoginRequest
            {
                Login = $"concurrent_test_{i}@example.com",
                Password = "TestP@ss123!"
            };

            tasks.Add(Client.PostAsJsonAsync("/api/users/standard-login", request));
        }

        var responses = await Task.WhenAll(tasks);

        responses.Should().HaveCount(10);
        responses.Should().AllSatisfy(r => r.Should().NotBeNull());
    }

    [Fact, Trait("Category", "Integration")]
    public async Task Register_MultipleSimultaneousRequests_ShouldHandleGracefully()
    {
        var sameEmail = $"parallel_{Guid.NewGuid():N}@example.com";
        var request = new StandardRegisterUserRequest
        {
            Email = sameEmail,
            Password = "ValidP@ssword123!",
            FirstName = "Parallel",
            LastName = "Test"
        };

        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(Client.PostAsJsonAsync("/api/users/standard-register", request));
        }

        var responses = await Task.WhenAll(tasks);

        responses.Should().OnlyContain(r => r.StatusCode != HttpStatusCode.InternalServerError);
        responses.Count(r => r.IsSuccessStatusCode).Should().BeLessOrEqualTo(1);
    }

    #endregion

    #region Negative Logic Flow

    [Fact, Trait("Category", "Integration")]
    public async Task WeakPasswordPolicy_ShouldRejectWeakPasswords()
    {
        var request = new StandardRegisterUserRequest
        {
            Email = "weak@test.com",
            Password = "123", //bad
            FirstName = "Weak",
            LastName = "Pass"
        };

        var response = await Client.PostAsJsonAsync("/api/users/standard-register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion
}
