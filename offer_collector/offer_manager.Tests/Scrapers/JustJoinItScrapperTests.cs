using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Offer_collector.Models;
using Offer_collector.Models.OfferFetchers;
using worker.Models.Tools;
using Xunit;

namespace offer_manager.Tests.Scrapers;

internal class JustJoinItScrapperWrapper : JustJoinItScrapper
{
    public JustJoinItScrapperWrapper() : base(ClientType.httpClient) { }
}

public class JustJoinItScrapperTests
{
    private readonly JustJoinItScrapperWrapper _sut;
    private readonly string _nextDataJson;

    public JustJoinItScrapperTests()
    {
        HeadlessbrowserSettings.Host = "localhost";
        HeadlessbrowserSettings.Port = 3000;

        _sut = new JustJoinItScrapperWrapper();
        
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "../../../../../offers_example/JustjoinIt/JSON.json");
        if (!File.Exists(jsonPath))
             jsonPath = Path.Combine(AppContext.BaseDirectory, "offers_example/JustjoinIt/JSON.json");
             
        var rawOffer = File.ReadAllText(jsonPath);
        var singleOffer = JObject.Parse(rawOffer);
        var offersArray = new JArray(singleOffer);
        
        var queries = new JArray();
        queries.Add(new JObject()); // 0
        queries.Add(new JObject()); // 1
        queries.Add(new JObject(    // 2 (for GetOfferCount: queries[2].state.data.count)
             new JProperty("state", new JObject(
                new JProperty("data", new JObject(
                    new JProperty("count", offersArray.Count)
                ))
            ))
        )); 
        
        queries.Add(new JObject( // 3 (for GetOffersJson)
             new JProperty("state", new JObject(
                new JProperty("data", new JObject(
                    new JProperty("pages", new JArray(
                        new JObject(
                            new JProperty("data", offersArray)
                        )
                    ))
                ))
            ))
        ));

        var root = new JObject(
            new JProperty("state", new JObject(
                new JProperty("queries", queries)
            ))
        );
        
        _nextDataJson = root.ToString();
    }

    [Fact]
    public void GetOffersJson_GivenValidState_ReturnsOffers()
    {
        var result = _sut.GetOffersJson(_nextDataJson);

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0]["slug"].Should().NotBeNull();
    }

    [Fact]
    public void GetOffersJson_GivenInvalidJson_ThrowsException()
    {
        Action act = () => _sut.GetOffersJson("{ invalid json");

        act.Should().Throw<Newtonsoft.Json.JsonReaderException>();
    }

    [Fact]
    public void GetOffersJson_GivenJsonWithMissingPath_ReturnsEmptyList()
    {
        var emptyJson = new JObject(new JProperty("state", new JObject())).ToString();

        var result = _sut.GetOffersJson(emptyJson);

        result.Should().BeEmpty();
    }

    [Fact]
    public void GetOfferCount_GivenValidState_ReturnsCorrectCount()
    {
        var count = _sut.GetOfferCount(_nextDataJson);

        count.Should().Be(1);
    }
}
