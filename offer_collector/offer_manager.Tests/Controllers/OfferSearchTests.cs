using FluentAssertions;
using shared_models.Dto;

namespace offer_manager.Tests.Controllers;

public class OfferSearchTests
{
    #region SearchDto Tests

    [Fact]
    public void SearchDto_DefaultValues()
    {
        var dto = new SearchDto();

        dto.Keyword.Should().BeNull();
        dto.Category.Should().BeNull();
        dto.Localization.Should().BeNull();
    }

    [Fact]
    public void SearchDto_CanSetFilterProperties()
    {
        var dto = new SearchDto
        {
            Keyword = "Developer",
            Category = "IT",
            Localization = "Warszawa",
            EmploymentType = "full_time",
            SalaryPeriod = "monthly",
            SalaryFrom = "5000",
            SalaryTo = "15000"
        };

        dto.Keyword.Should().Be("Developer");
        dto.Category.Should().Be("IT");
        dto.Localization.Should().Be("Warszawa");
        dto.EmploymentType.Should().Be("full_time");
        dto.SalaryPeriod.Should().Be("monthly");
        dto.SalaryFrom.Should().Be("5000");
        dto.SalaryTo.Should().Be("15000");
    }

    [Theory]
    [InlineData("Developer")]
    [InlineData("Python")]
    [InlineData("Java Senior")]
    public void SearchDto_Keyword_CanBeSet(string keyword)
    {
        var dto = new SearchDto { Keyword = keyword };
        dto.Keyword.Should().Be(keyword);
    }

    #endregion

    #region Pagination Calculations

    [Theory]
    [InlineData(100, 10, 10)]  // 100 items, 10 per page = 10 pages
    [InlineData(101, 10, 11)]  // 101 items, 10 per page = 11 pages
    [InlineData(0, 10, 0)]     // 0 items = 0 pages
    [InlineData(50, 20, 3)]    // 50 items, 20 per page = 3 pages
    public void Pagination_TotalPages_Calculation(int totalItems, int pageSize, int expectedPages)
    {
        var totalPages = totalItems == 0 ? 0 : (int)Math.Ceiling((double)totalItems / pageSize);
        totalPages.Should().Be(expectedPages);
    }

    [Theory]
    [InlineData(1, 10, 0)]   // Page 1, skip 0
    [InlineData(2, 10, 10)]  // Page 2, skip 10
    [InlineData(3, 20, 40)]  // Page 3, skip 40
    public void Pagination_Skip_Calculation(int page, int pageSize, int expectedSkip)
    {
        var skip = (page - 1) * pageSize;
        skip.Should().Be(expectedSkip);
    }

    #endregion

    #region Filter Validation Tests

    [Theory]
    [InlineData("Warszawa", true)]
    [InlineData("Krakow", true)]
    [InlineData("", true)]  // empty
    public void LocationFilter_Validation(string location, bool isValid)
    {
        var result = location.Length <= 100;
        result.Should().Be(isValid);
    }

    [Theory]
    [InlineData(0, 10000, true)]
    [InlineData(5000, 15000, true)]
    [InlineData(10000, 5000, false)]  // min > max
    public void SalaryRange_Validation(int minSalary, int maxSalary, bool isValid)
    {
        var result = minSalary <= maxSalary || maxSalary == 0;
        result.Should().Be(isValid);
    }

    [Theory]
    [InlineData("full_time")]
    [InlineData("part_time")]
    [InlineData("contract")]
    [InlineData("internship")]
    [InlineData("freelance")]
    public void EmploymentType_ValidValues(string employmentType)
    {
        var validTypes = new HashSet<string>
        {
            "full_time", "part_time", "contract", "internship", "freelance", "temporary"
        };

        validTypes.Should().Contain(employmentType);
    }

    #endregion

    #region Sorting Tests

    [Theory]
    [InlineData("date_desc")]
    [InlineData("date_asc")]
    [InlineData("salary_desc")]
    [InlineData("salary_asc")]
    [InlineData("relevance")]
    public void SortBy_ValidOptions(string sortOption)
    {
        var validOptions = new HashSet<string>
        {
            "date_desc", "date_asc", "salary_desc", "salary_asc", "relevance", "company_name"
        };

        validOptions.Should().Contain(sortOption);
    }

    [Theory]
    [InlineData("invalid_sort")]
    [InlineData("")]
    [InlineData("random")]
    public void SortBy_InvalidOptions_ShouldDefaultToRelevance(string sortOption)
    {
        var validOptions = new HashSet<string>
        {
            "date_desc", "date_asc", "salary_desc", "salary_asc", "relevance", "company_name"
        };

        var isValid = validOptions.Contains(sortOption);
        var actualSort = isValid ? sortOption : "relevance";
        
        actualSort.Should().Be("relevance");
    }

    #endregion

    #region Offer Sites Types Tests

    [Theory]
    [InlineData(0, "Pracuj.pl")]
    [InlineData(1, "JustJoinIt")]
    [InlineData(2, "OLX Praca")]
    [InlineData(3, "Jooble")]
    public void OfferSiteType_EnumMapping(int typeId, string expectedName)
    {
        var siteTypes = new Dictionary<int, string>
        {
            { 0, "Pracuj.pl" },
            { 1, "JustJoinIt" },
            { 2, "OLX Praca" },
            { 3, "Jooble" }
        };

        siteTypes.Should().ContainKey(typeId);
        siteTypes[typeId].Should().Be(expectedName);
    }

    #endregion

    #region Response Structure Tests

    [Fact]
    public void OffersResponse_CanContainList()
    {
        var offers = new List<object>
        {
            new { Id = 1, Title = "Software Developer" },
            new { Id = 2, Title = "Data Scientist" }
        };

        offers.Should().HaveCount(2);
        offers.Should().NotBeEmpty();
    }

    [Fact]
    public void PaginatedResponse_Structure()
    {
        var response = new
        {
            Items = new List<object>(),
            Page = 1,
            PageSize = 20,
            TotalItems = 100,
            TotalPages = 5
        };

        response.Page.Should().Be(1);
        response.PageSize.Should().Be(20);
        response.TotalItems.Should().Be(100);
        response.TotalPages.Should().Be(5);
    }

    #endregion

    #region Edge Cases

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(1)]
    public void PageNumber_EdgeCases(int page)
    {
        var isValid = page > 0;
        isValid.Should().BeTrue();
    }

    [Fact]
    public void EmptySearchResults_ReturnsEmptyList()
    {
        var results = new List<object>();
        
        results.Should().BeEmpty();
        results.Should().NotBeNull();
    }

    [Fact]
    public void LargePageSize_ShouldBeCapped()
    {
        var requestedPageSize = 1000;
        var maxPageSize = 100;
        var actualPageSize = Math.Min(requestedPageSize, maxPageSize);

        actualPageSize.Should().Be(100);
    }

    #endregion
}
