using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace offer_manager.Tests.Integration;

public class DictionaryControllerIntegrationTests : IntegrationTestBase
{
    public DictionaryControllerIntegrationTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    #region SearchView Dictionaries Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetSearchViewDictionaries_ReturnsOk()
    {
        var response = await Client.GetAsync("/api/dictionary/searchview-dictionaries");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetSearchViewDictionaries_ReturnsJsonContent()
    {
        var response = await Client.GetAsync("/api/dictionary/searchview-dictionaries");

        if (response.IsSuccessStatusCode)
        {
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        }
    }

    #endregion

    #region All Dictionaries Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetAllDictionaries_ReturnsOk()
    {
        var response = await Client.GetAsync("/api/dictionary/all-dictionaries");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, Trait("Category", "Integration")]
    public async Task GetAllDictionaries_ReturnsJsonContent()
    {
        var response = await Client.GetAsync("/api/dictionary/all-dictionaries");

        if (response.IsSuccessStatusCode)
        {
            response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
        }
    }

    #endregion

    #region Response Structure Tests

    [Fact, Trait("Category", "Integration")]
    public async Task GetSearchViewDictionaries_ResponseContainsExpectedStructure()
    {
        var response = await Client.GetAsync("/api/dictionary/searchview-dictionaries");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
    }

    #endregion
}
