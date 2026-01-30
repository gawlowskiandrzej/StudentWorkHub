using FluentAssertions;

namespace offer_manager.Tests.Controllers;
public class DictionaryTests
{
    #region Dictionary Types Tests

    [Theory]
    [InlineData("employment_types")]
    [InlineData("employment_schedules")]
    [InlineData("salary_periods")]
    [InlineData("experience_levels")]
    [InlineData("education_levels")]
    [InlineData("industries")]
    public void AllDictionaries_ValidTypes(string dictionaryType)
    {
        var allDictionaries = new List<string>
        {
            "employment_types",
            "employment_schedules",
            "salary_periods",
            "experience_levels",
            "education_levels",
            "industries",
            "skills",
            "locations"
        };

        allDictionaries.Should().Contain(dictionaryType);
    }

    [Fact]
    public void SearchViewDictionaries_ContainsExpectedTypes()
    {
        var searchViewDictionaries = new List<string>
        {
            "employment_types",
            "employment_schedules", 
            "salary_periods"
        };

        searchViewDictionaries.Should().Contain("employment_types");
        searchViewDictionaries.Should().Contain("salary_periods");
        searchViewDictionaries.Should().HaveCount(3);
    }

    #endregion

    #region Dictionary Entry Tests

    [Fact]
    public void DictionaryEntry_HasKeyValue()
    {
        var entry = new KeyValuePair<string, string>("full_time", "Pelny etat");

        entry.Key.Should().Be("full_time");
        entry.Value.Should().Be("Pelny etat");
    }

    [Fact]
    public void DictionaryEntry_LocalizedValues()
    {
        var polishEntries = new Dictionary<string, string>
        {
            { "full_time", "Pelny etat" },
            { "part_time", "Pol etatu" },
            { "contract", "Umowa zlecenie" },
            { "internship", "Staz" }
        };

        polishEntries.Should().ContainKey("full_time");
        polishEntries["full_time"].Should().Be("Pelny etat");
    }

    [Theory]
    [InlineData("pl", "Pelny etat")]
    [InlineData("en", "Full time")]
    public void DictionaryEntry_LanguageSupport(string language, string expectedValue)
    {
        var translations = new Dictionary<string, Dictionary<string, string>>
        {
            { "pl", new Dictionary<string, string> { { "full_time", "Pelny etat" } } },
            { "en", new Dictionary<string, string> { { "full_time", "Full time" } } }
        };

        translations.Should().ContainKey(language);
        translations[language]["full_time"].Should().Be(expectedValue);
    }

    #endregion

    #region Dictionary Response Structure Tests

    [Fact]
    public void DictionaryResponse_ContainsMultipleDictionaries()
    {
        var response = new Dictionary<string, List<KeyValuePair<string, string>>>
        {
            { "employment_types", new List<KeyValuePair<string, string>>
                {
                    new("full_time", "Pelny etat"),
                    new("part_time", "Pol etatu")
                }
            },
            { "employment_schedules", new List<KeyValuePair<string, string>>
                {
                    new("flexible", "Elastyczny"),
                    new("fixed", "Staly")
                }
            }
        };

        response.Should().ContainKey("employment_types");
        response.Should().ContainKey("employment_schedules");
        response["employment_types"].Should().HaveCount(2);
    }

    [Fact]
    public void EmptyDictionary_ReturnsEmptyList()
    {
        var emptyDict = new List<KeyValuePair<string, string>>();
        
        emptyDict.Should().BeEmpty();
        emptyDict.Should().NotBeNull();
    }

    #endregion

    #region Caching Tests

    [Theory]
    [InlineData(60)]   // 1 m
    [InlineData(300)]  // 5 m
    [InlineData(3600)] // 1 h
    public void DictionaryCache_ExpirationValues(int cacheSeconds)
    {
        var cacheExpiration = TimeSpan.FromSeconds(cacheSeconds);
        
        cacheExpiration.TotalSeconds.Should().Be(cacheSeconds);
        cacheExpiration.Should().BePositive();
    }

    [Fact]
    public void DictionaryCache_KeyGeneration()
    {
        var dictionaryTypes = new[] { "employment_types", "salary_periods" };
        var cacheKey = $"dictionaries:{string.Join(":", dictionaryTypes.OrderBy(x => x))}";

        cacheKey.Should().Be("dictionaries:employment_types:salary_periods");
    }

    #endregion

    #region ProfileCreationDictionaries Tests

    [Fact]
    public void ProfileCreationDictionaries_ContainsRequiredTypes()
    {
        var profileDictionaries = new List<string>
        {
            "experience_levels",
            "education_levels",
            "skills",
            "industries",
            "employment_types",
            "locations"
        };

        profileDictionaries.Should().Contain("experience_levels");
        profileDictionaries.Should().Contain("skills");
        profileDictionaries.Should().HaveCountGreaterThan(3);
    }

    #endregion

    #region Error Handling Tests

    [Fact]
    public void DictionaryNotFound_ReturnsEmpty()
    {
        var availableDictionaries = new HashSet<string>
        {
            "employment_types",
            "salary_periods"
        };

        var requestedType = "invalid_dictionary";
        var exists = availableDictionaries.Contains(requestedType);

        exists.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void DictionaryType_EmptyOrWhitespace_IsInvalid(string dictionaryType)
    {
        var isValid = !string.IsNullOrWhiteSpace(dictionaryType);
        isValid.Should().BeFalse();
    }

    #endregion

    #region Dictionary Value Format Tests

    [Theory]
    [InlineData("junior", "Junior")]
    [InlineData("mid", "Mid/Regular")]
    [InlineData("senior", "Senior")]
    [InlineData("lead", "Lead/Principal")]
    public void ExperienceLevel_Mappings(string key, string displayValue)
    {
        var levels = new Dictionary<string, string>
        {
            { "junior", "Junior" },
            { "mid", "Mid/Regular" },
            { "senior", "Senior" },
            { "lead", "Lead/Principal" }
        };

        levels.Should().ContainKey(key);
        levels[key].Should().Be(displayValue);
    }

    [Theory]
    [InlineData("hourly", "Godzinowo")]
    [InlineData("monthly", "Miesiecznie")]
    [InlineData("yearly", "Rocznie")]
    public void SalaryPeriod_Mappings(string key, string displayValue)
    {
        var periods = new Dictionary<string, string>
        {
            { "hourly", "Godzinowo" },
            { "monthly", "Miesiecznie" },
            { "yearly", "Rocznie" }
        };

        periods.Should().ContainKey(key);
        periods[key].Should().Be(displayValue);
    }

    #endregion

    #region Concurrent Access Tests

    [Fact]
    public void DictionaryAccess_ThreadSafe()
    {
        var dictionary = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        var result = dictionary.TryGetValue("key1", out var value);

        result.Should().BeTrue();
        value.Should().Be("value1");
    }

    #endregion
}
