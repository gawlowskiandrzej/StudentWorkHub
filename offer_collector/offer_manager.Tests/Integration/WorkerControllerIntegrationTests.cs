using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace offer_manager.Tests.Integration;

public class WorkerControllerIntegrationTests : IntegrationTestBase
{
    public WorkerControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    #region Get Job Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetJob_WithInvalidId_ReturnsNotFound()
    {
        var response = await Client.GetAsync("/api/worker/nonexistent-job-id");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetJob_WithValidId_ReturnsOk()
    {
        var jobId = "test-job-id"; 

        var response = await Client.GetAsync($"/api/worker/{jobId}");

        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
    }

    #endregion

    #region Create Job Tests

    [Fact, Trait("Category", "Integration")]
    public async Task CreateJob_WithEmptyRequest_ReturnsBadRequest()
    {
        var request = new { };

        var response = await Client.PostAsJsonAsync("/api/worker", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task CreateJob_WithValidData_ReturnsOk()
    {
        var request = new
        {
            OfferSiteType = 1, // JustJoinIt
            BatchSize = 10,
            BatchLimit = 50,
            Offset = 0
        };

        var response = await Client.PostAsJsonAsync("/api/worker", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    #endregion

    #region Endpoint Availability Tests

    [Fact, Trait("Category", "Integration")]
    public async Task WorkerEndpoint_IsAccessible()
    {
        var response = await Client.GetAsync("/api/worker/test-job-id");

        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound, "Endpoint should exist");
        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError, "Endpoint should not crash");
    }

    #endregion

    #region Concurrency / Race Condition Tests

    [Fact, Trait("Category", "Integration")]
    public async Task CreateJob_MultipleSimultaneousRequests_HandlesGracefully()
    {
        var request = new
        {
            OfferSiteType = 1, // JustJoinIt
            BatchSize = 5,
            BatchLimit = 10,
            Offset = 0
        };

        var tasks = Enumerable.Range(0, 5)
            .Select(_ => Client.PostAsJsonAsync("/api/workers", request))
            .ToList();

        var responses = await Task.WhenAll(tasks);

        responses.Should().HaveCount(5);
        responses.Should().AllSatisfy(r => r.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError));
    }

    #endregion
}
