using System.IO;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.Tools;
using Offer_collector.Models;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class OfferMapperRealJsonTests
{
    private static string ResolvePath(params string[] segments)
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", Path.Combine(segments)));
    }

    [Fact]
    public void JustJoinItFixture_MapsToUnifiedSchema_WithKeyFields()
    {
        var jsonPath = ResolvePath("offers_example", "JustjoinIt", "JSON.json");
        var json = File.ReadAllText(jsonPath);

        var schema = OfferMapper.DeserializeJson<JustJoinItSchema>(json);
        var unified = OfferMapper.ToUnifiedSchema<JustJoinItSchema>(schema);

        unified.Should().NotBeNull();
        unified.company.name.Should().NotBeNull();
        unified.location.city.Should().NotBeNull();
    }

    [Fact]
    public void PracujFixture_FirstEntryMapsToUnifiedSchema_WithLocationAndSalary()
    {
        var jsonPath = ResolvePath("offers_example", "Pracuj.pl", "JSon.json");
        var raw = File.ReadAllText(jsonPath);
        var asArray = JArray.Parse($"[{raw}]");

        var firstToken = asArray.First ?? throw new InvalidOperationException("Fixture is empty");
        var schema = OfferMapper.DeserializeJToken<PracujplSchema>(firstToken);
        var unified = OfferMapper.ToUnifiedSchema<PracujplSchema>(schema);

        unified.Should().NotBeNull();
        unified.company.name.Should().NotBeNull();
    }
}
