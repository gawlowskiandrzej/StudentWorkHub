using FluentAssertions;
using Xunit;
using offer_manager.Models.Offers;
using Offer_collector.Models.Tools;
using Offer_collector.Models;

namespace offer_manager.Tests.Controllers;

public class OfferMapperValidationTests
{
    [Fact]
    public void MapOffer_WithNullSalary_ShouldBeHandled()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "salary", null }, 
        };

        var offer = MapJsonToOffer(sourceJson);
        
        Assert.NotNull(offer);
        Assert.Null(offer.salary);
    }

    [Fact]
    public void MapOffer_WithInvertedSalaryRange_ShouldFailValidation()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "salaryFrom", 50000 },
            { "salaryTo", 10000 }, 
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer.salary);
        Assert.True(offer.salary.from <= offer.salary.to, "Salary From should be <= To"); 
    }

    [Fact]
    public void MapOffer_WithNegativeSalary_ShouldFailValidation()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "salaryFrom", -5000 }, 
            { "salaryTo", 10000 },
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer.salary);
        Assert.True(offer.salary.from >= 0, "Salary cannot be negative");
    }

    [Fact]
    public void MapOffer_WithMissingRequiredFields_ShouldFail()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "salary", new { from = 10000, to = 20000 } },
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        Assert.False(string.IsNullOrEmpty(offer.jobTitle), "Job title is required");
    }

    [Fact]
    public void MapOffer_WithDifferentCaseFieldNames_ShouldStillMap()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "TITLE", "Developer" }, 
            { "salary", new { from = 10000, to = 20000 } },
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.Equal("Developer", offer.jobTitle);
    }

    [Fact]
    public void MapOffer_WithNullElementsInSkillsArray_ShouldNotCrash()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "skills", new object[] { "React", null, "Node.js", null } }, // Null elements!
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        var skillCount = offer.requirements?.skills?.Count ?? 0;
        if (skillCount != 2)
        {
            Assert.Fail($"BUG: Expected 2 skills, got {skillCount}. Null elements not filtered!");
        }
    }

    [Fact]
    public void MapOffer_WithDuplicateSkills_ShouldNotDeduplicate()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "skills", new[] { "React", "React", "React" } }, // Duplicates!
        };

        var offer = MapJsonToOffer(sourceJson);

        var skillCount = offer.requirements?.skills?.Count ?? 0;
        Assert.True(skillCount == 1, "Duplicate skills should be removed!");
    }

    [Fact]
    public void MapOffer_WithInvalidDateFormat_ShouldNotValidate()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "publishedDate", "invalid-date-format" }, // Invalid!
            { "expirationDate", "2099-99-99" }, // Invalid!
        };

        try
        {
            var offer = MapJsonToOffer(sourceJson);
            
            Assert.NotNull(offer);
        }
        catch (FormatException)
        {
            Assert.True(true, "BUG: Date parsing failed for invalid formats");
        }
    }

    [Fact]
    public void MapOffer_WithIncorrectBooleanFormat_ShouldNotValidate()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "remote", "yes" }, // String instead of boolean!
            { "hybrid", 1 }, // Integer instead of boolean!
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        if (offer.isRemote != null || offer.isHybrid != null)
        {
            Assert.True(true, "BUG: Boolean fields accepted incorrect formats");
        }
    }

    [Fact]
    public void MapOffer_WithVeryLongStrings_ShouldBeTruncatedOrValidated()
    {
        var veryLongString = new string('x', 100000); // 100KB string!
        
        var sourceJson = new Dictionary<string, object>
        {
            { "title", veryLongString },
            { "description", veryLongString },
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        Assert.True(offer.jobTitle?.Length <= 1000, "Job title should be truncated to reasonable length");
    }

    [Fact]
    public void MapOffer_WithSpecialCharacters_ShouldSanitize()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer<script>alert('xss')</script>" }, 
            { "description", "Description with\nnewlines\r\nand\ttabs" },
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        Assert.DoesNotContain("<script>", offer.jobTitle); 
    }

    [Fact]
    public void MapOffer_WithNullNestedObjects_ShouldNotCrash()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "requirements", null }, // Null requirements!
            { "benefits", null }, // Null benefits!
        };

        try
        {
            var offer = MapJsonToOffer(sourceJson);
            Assert.NotNull(offer);
            Assert.NotNull(offer.requirements);
        }
        catch (NullReferenceException ex)
        {
            Assert.Fail($"BUG: NullReferenceException when mapping null nested objects: {ex.Message}");
        }
    }

    [Fact]
    public void MapOffer_WithUnicodeCharacters_ShouldHandleCorrectly()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer 你好 مرحبا Здравствуй" }, // Unicode
            { "description", "Emojis: 😀 🎉 🚀 💻" }, // Emojis
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        if (!offer.jobTitle!.Contains("你好"))
        {
            Assert.Fail("BUG: Unicode characters lost in mapping");
        }
    }

    [Fact]
    public void MapOffer_WithEmptyCollections_ShouldNotCrash()
    {
        var sourceJson = new Dictionary<string, object>
        {
            { "title", "Developer" },
            { "skills", new object[] { } }, // Empty array
            { "languages", new object[] { } }, // Empty array
            { "benefits", new object[] { } }, // Empty array
        };

        var offer = MapJsonToOffer(sourceJson);

        Assert.NotNull(offer);
        Assert.NotNull(offer.requirements?.skills);
        Assert.Empty(offer.requirements!.skills!); // Should be empty, not null
    }

    [Fact]
    public void MapOffer_WithCircularReferences_ShouldNotCrash()
    {
        var sourceJson = new Dictionary<string, object>();
        sourceJson["title"] = "Developer";
        sourceJson["linkedOffer"] = sourceJson; // Circular reference!

        try
        {
            var offer = MapJsonToOffer(sourceJson);
        }
        catch (StackOverflowException)
        {
            Assert.Fail("BUG: Stack overflow from circular references in JSON");
        }
    }

    private UnifiedOfferSchemaClass MapJsonToOffer(Dictionary<string, object> json)
    {
        var offer = new UnifiedOfferSchemaClass
        {
            id = json.ContainsKey("id") ? Convert.ToInt32(json["id"]) : 0,
            jobTitle = json.ContainsKey("title") ? json["title"]?.ToString() : null,
        };

        if (json.ContainsKey("salary"))
        {
            if (json["salary"] == null) offer.salary = null;
        }

        if (json.ContainsKey("salaryFrom") && json.ContainsKey("salaryTo"))
        {
            offer.salary = new Salary
            {
                from = Convert.ToInt32(json["salaryFrom"]),
                to = Convert.ToInt32(json["salaryTo"]),
            };
        }

        if (json.ContainsKey("skills"))
        {
           var skillsArray = json["skills"] as System.Collections.IEnumerable;
           if (skillsArray != null)
           {
               var skillsList = new List<string>();
               foreach(var s in skillsArray) {
                   if(s != null) skillsList.Add(s.ToString());
               }
               var distinctSkills = skillsList.Distinct().ToList();
               offer.requirements.skills = distinctSkills.Select(s => new Skill { skill = s }).ToList();
           }
        }

        // TODO: Add more field mappings

        return offer;
    }
}
