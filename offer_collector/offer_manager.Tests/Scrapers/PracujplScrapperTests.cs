using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Offer_collector.Models;
using Offer_collector.Models.OfferFetchers;
using worker.Models.Tools;
using Xunit;

namespace offer_manager.Tests.Scrapers;

internal class PracujplScrapperWrapper : PracujplScrapper
{
    public PracujplScrapperWrapper() : base(ClientType.httpClient) { }
}

public class PracujplScrapperTests
{
    private readonly PracujplScrapperWrapper _sut;
    private readonly string _nextDataJson;

    public PracujplScrapperTests()
    {
        HeadlessbrowserSettings.Host = "localhost";
        HeadlessbrowserSettings.Port = 3000;

        _sut = new PracujplScrapperWrapper();
        
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "../../../../../offers_example/Pracuj.pl/JSon.json");
        if (!File.Exists(jsonPath))
             jsonPath = Path.Combine(AppContext.BaseDirectory, "offers_example/Pracuj.pl/JSon.json");
             
        var rawOffers = File.ReadAllText(jsonPath);
        
        var offersJsonArray = $"[{rawOffers}]"; 
        JArray jArray;
        try 
        {
            jArray = JArray.Parse(offersJsonArray);
        }
        catch
        {
             jArray = JArray.Parse(rawOffers);
        }
        
        var root = new JObject(
            new JProperty("props", new JObject(
                new JProperty("pageProps", new JObject(
                    new JProperty("dehydratedState", new JObject(
                        new JProperty("queries", new JArray(
                            new JObject(
                                new JProperty("state", new JObject(
                                    new JProperty("data", new JObject(
                                        new JProperty("groupedOffers", jArray),
                                        new JProperty("offersTotalCount", jArray.Count)
                                    ))
                                ))
                            )
                        ))
                    ))
                ))
            ))
        );
        
        _nextDataJson = root.ToString();
    }

    [Fact]
    public void GetOffersJToken_GivenValidNextData_ReturnsOffers()
    {
        var result = _sut.GetOffersJToken(_nextDataJson);

        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
        result[0]["jobTitle"].Should().NotBeNull();
    }

    [Fact]
    public void GetOffersJToken_GivenInvalidJson_ThrowsException()
    {
        Action act = () => _sut.GetOffersJToken("{ invalid json");

        act.Should().Throw<Newtonsoft.Json.JsonReaderException>();
    }

    [Fact]
    public void GetOffersJToken_GivenJsonWithMissingPath_ReturnsEmptyList()
    {
        var emptyJson = new JObject(new JProperty("props", new JObject())).ToString();

        var result = _sut.GetOffersJToken(emptyJson);

        result.Should().BeEmpty();
    }

    [Fact]
    public void GetOfferCount_GivenValidNextData_ReturnsCorrectCount()
    {
        var count = _sut.GetOfferCount(_nextDataJson);

        count.Should().BeGreaterThan(0);
    }

    #region Robustness Tests

    [Fact]
    public void GetOffersJToken_GivenExtremeLongJson_DoesNotCrash()
    {
        var hugeString = new string('A', 10 * 1024 * 1024); // 10MB string
        var invalidJson = "{\"data\": \"" + hugeString + "\"}";

        Action act = () => _sut.GetOffersJToken(invalidJson);

        act.Should().NotThrow<OutOfMemoryException>();
    }

    [Fact]
    public void GetOffersJToken_WithHtmlCharactersInJson_ParsesCorrectly()
    {
        var injectedJson = "{\"props\":{\"pageProps\":{\"dehydratedState\":{\"queries\":[{\"state\":{\"data\":{\"groupedOffers\":[{\"jobTitle\": \"<script>alert(1)</script>\"}]}}}]}}}}";
        
        var result = _sut.GetOffersJToken(injectedJson);

        result.Should().HaveCount(1);
        result[0]["jobTitle"]!.ToString().Should().Contain("<script>");
    }

    #endregion
}
