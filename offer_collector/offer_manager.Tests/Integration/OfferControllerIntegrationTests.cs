using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using shared_models.Dto;
using offer_manager.Models.Offers.dtoObjects;
using offer_manager.Models.Offers.DtoObjects;
using offer_manager.Models.WorkerService;
using UnifiedOfferSchema;
using Newtonsoft.Json;

namespace offer_manager.Tests.Integration;
public class OfferControllerIntegrationTests : IntegrationTestBase
{
    public OfferControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    #region Database Search Logic Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetOffersFromDatabase_WithValidFilters_ReturnsFilteredResults()
    {
        var request = new SearchDto
        {
            Keyword = "Developer",
            Category = "IT",
            Localization = "Warszawa"
        };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-database", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<DatabaseOffersResponseDto>();
        content.Should().NotBeNull();
        content!.Error.Should().BeNullOrEmpty();
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetOffersFromDatabase_WithExtremePagination_HandlesGracefully()
    {
        var request = new SearchDto();
        int pageOffset = 1000000; // impossible page
        int perPage = 10;

        var response = await Client.PostAsJsonAsync($"/api/offers/offers-database?pageOffset={pageOffset}&perpage={perPage}", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<DatabaseOffersResponseDto>();
        content!.Pagination.Should().NotBeNull();
        content.Pagination!.Items.Should().BeEmpty();
    }

    #endregion

    #region Scrapped Offers Logic Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetScrappedOffers_WithInvalidJobIds_ReturnsNotFoundStatus()
    {
        var request = new jobIdsDto { JobIds = new List<string> { "invalid-id" } };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-scrapped", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<ScrappedOffersResponseDto>();
        content!.ScrappingStatus.jobInfos.Should().Contain(j => j.Status == "notfound");
    }

    [Fact, Trait("Category", "Integration")]
    public async Task CreateScrappers_GeneratesJobIdsForEachPortal()
    {
        var filters = new SearchDto { Keyword = "C#" };

        var response = await Client.PostAsJsonAsync("/api/offers/create-scrappers?batchLimit=1", filters);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<jobIdsDto>();
        content!.JobIds.Should().NotBeEmpty();
    }

    #endregion

    #region Cross-Component Logic (Bug Discovery)

    [Fact, Trait("Category", "Integration")]
    public async Task GetOffersFromDatabase_DuplicateFiltering_DoesNotLoseData()
    {
        var request = new SearchDto { 
            Keyword = "Java",
            EmploymentType = "NonExistentType"
        };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-database", request);
        
        var content = await response.Content.ReadFromJsonAsync<DatabaseOffersResponseDto>();
        
        content!.Pagination!.Items.Should().BeEmpty();
    }

    [Fact, Trait("Category", "Integration")]
    public async Task ScrappedOffers_DeduplicationByUrl_WorksAcrossMultipleJobs()
    {
        //2 jobs returning the same URL
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetScrappedOffers_WithAiEnrichment_FlowSucceeds()
    {
        var request = new jobIdsDto { JobIds = new List<string> { "job-1" } };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-scrapped?usedAi=true", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetScrappedOffers_WhenAiFails_StillReturnsRawOffers()
    {
        // if AI fails
    }

    #endregion

    #region Security and Input Resilience

    [Fact, Trait("Category", "Integration")]
    public async Task GetOffers_WithPolishCharacters_ShouldProcessCorrectly()
    {
        var request = new SearchDto { Keyword = "Inżynier Programista Łódź" };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-database", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetOffers_WithSqlInjectionLikePatterns_ShouldHandleSafely()
    {
        var request = new SearchDto { Keyword = "'; DROP TABLE Offers; --" };

        var response = await Client.PostAsJsonAsync("/api/offers/offers-database", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    #endregion
}
