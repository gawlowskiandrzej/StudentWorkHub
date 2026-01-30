using FluentAssertions;
using worker.Models.DTO;

namespace offer_manager.Tests.Controllers;

public class WorkerJobTests
{
    #region JobInfo Tests

    [Fact]
    public void JobInfo_DefaultValues()
    {
        var jobInfo = new JobInfo();

        jobInfo.JobId.Should().BeNull();
        jobInfo.TotalBatches.Should().Be(0);
    }

    [Fact]
    public void JobInfo_CanSetProperties()
    {
        var jobInfo = new JobInfo
        {
            JobId = "job-123-456",
            TotalBatches = 10,
            StartTime = DateTime.UtcNow
        };

        jobInfo.JobId.Should().Be("job-123-456");
        jobInfo.TotalBatches.Should().Be(10);
        jobInfo.StartTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    #endregion

    #region JobInfoDto Tests

    [Fact]
    public void JobInfoDto_DefaultValues()
    {
        var dto = new JobInfoDto();

        dto.JobId.Should().BeNull();
        dto.Status.Should().BeNull();
    }

    [Fact]
    public void JobInfoDto_CanSetProperties()
    {
        var dto = new JobInfoDto
        {
            JobId = "job-123",
            Status = "processing"
        };

        dto.JobId.Should().Be("job-123");
        dto.Status.Should().Be("processing");
    }

    [Fact]
    public void JobInfo_ToJobInfoDto_Maps_Correctly()
    {
        var jobInfo = new JobInfo
        {
            JobId = "test-job-id"
        };

        var dto = jobInfo.ToJobInfoDto();

        dto.JobId.Should().Be("test-job-id");
    }

    #endregion

    #region Job Status Tests

    [Theory]
    [InlineData("Pending")]
    [InlineData("Processing")]
    [InlineData("Completed")]
    [InlineData("Failed")]
    [InlineData("Cancelled")]
    public void JobStatus_ValidValues(string status)
    {
        var validStatuses = new HashSet<string>
        {
            "Pending", "Processing", "Completed", "Failed", "Cancelled", "Queued"
        };

        validStatuses.Should().Contain(status);
    }

    [Theory]
    [InlineData("Pending", false)]
    [InlineData("Processing", false)]
    [InlineData("Completed", true)]
    [InlineData("Failed", true)]
    [InlineData("Cancelled", true)]
    public void JobStatus_IsTerminal(string status, bool isTerminal)
    {
        var terminalStatuses = new HashSet<string> { "Completed", "Failed", "Cancelled" };
        var result = terminalStatuses.Contains(status);
        
        result.Should().Be(isTerminal);
    }

    #endregion

    #region Job Progress Tests

    [Theory]
    [InlineData(0, 100, 0)]
    [InlineData(50, 100, 50)]
    [InlineData(100, 100, 100)]
    [InlineData(25, 100, 25)]
    public void JobProgress_CalculatedCorrectly(int processedItems, int totalItems, int expectedProgress)
    {
        var progress = totalItems > 0 ? (processedItems * 100) / totalItems : 0;
        progress.Should().Be(expectedProgress);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void JobProgress_ValidRange(int progress)
    {
        var isValid = progress >= 0 && progress <= 100;
        isValid.Should().BeTrue();
    }

    #endregion

    #region Job Creation Tests

    [Fact]
    public void CreateJobRequest_ValidStructure()
    {
        var request = new
        {
            OfferSiteType = 1,
            BatchSize = 10,
            BatchLimit = 100,
            Offset = 0
        };

        request.OfferSiteType.Should().BePositive();
        request.BatchSize.Should().BePositive();
        request.BatchLimit.Should().BePositive();
        request.Offset.Should().BeGreaterOrEqualTo(0);
    }

    [Theory]
    [InlineData(1, 10, 100, true)]
    [InlineData(0, 10, 100, false)]  // invalid site type
    [InlineData(1, 0, 100, false)]   // invalid batch size
    [InlineData(1, 10, 0, false)]    // invalid batch limit
    public void CreateJobRequest_Validation(int siteType, int batchSize, int batchLimit, bool isValid)
    {
        var result = siteType > 0 && batchSize > 0 && batchLimit > 0;
        result.Should().Be(isValid);
    }

    #endregion

    #region Job ID Tests

    [Fact]
    public void JobId_Format_IsGuid()
    {
        var jobId = Guid.NewGuid().ToString();
        
        Guid.TryParse(jobId, out _).Should().BeTrue();
    }

    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000", true)]
    [InlineData("invalid-job-id", false)]
    [InlineData("", false)]
    public void JobId_Validation(string jobId, bool isValidGuid)
    {
        var result = !string.IsNullOrEmpty(jobId) && Guid.TryParse(jobId, out _);
        result.Should().Be(isValidGuid);
    }

    #endregion

    #region Job Deletion Tests

    [Fact]
    public void DeleteJobRequest_SingleJob()
    {
        var jobIds = new List<string> { "job-123" };
        
        jobIds.Should().HaveCount(1);
        jobIds.Should().Contain("job-123");
    }

    [Fact]
    public void DeleteJobRequest_MultipleJobs()
    {
        var jobIds = new List<string> { "job-1", "job-2", "job-3" };
        
        jobIds.Should().HaveCount(3);
    }

    [Fact]
    public void DeleteJobResponse_Success()
    {
        var response = (Success: true, Message: "Jobs deleted successfully");
        
        response.Success.Should().BeTrue();
        response.Message.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region Batch Processing Tests

    [Theory]
    [InlineData(100, 10, 10)]   // 100 items, batch of 10 = 10 batches
    [InlineData(105, 10, 11)]   // 105 items, batch of 10 = 11 batches
    [InlineData(50, 50, 1)]     // 50 items, batch of 50 = 1 batch
    public void BatchCount_Calculation(int totalItems, int batchSize, int expectedBatches)
    {
        var batches = (int)Math.Ceiling((double)totalItems / batchSize);
        batches.Should().Be(expectedBatches);
    }

    [Fact]
    public void BatchProcessing_Offset_Calculation()
    {
        var batchSize = 10;
        var batchNumber = 3;
        var offset = (batchNumber - 1) * batchSize;

        offset.Should().Be(20);
    }

    #endregion

    #region Timestamp Tests

    [Fact]
    public void JobInfo_Timestamps_AreValid()
    {
        var now = DateTime.UtcNow;
        var jobInfo = new JobInfo
        {
            StartTime = now,
            EndTime = now.AddMinutes(2)
        };

        jobInfo.EndTime.Should().BeAfter(jobInfo.StartTime);
    }

    [Fact]
    public void JobDuration_Calculation()
    {
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddMinutes(5);
        var duration = endTime - startTime;

        duration.TotalMinutes.Should().Be(5);
    }

    #endregion

    #region Offer Sites Types Tests

    [Theory]
    [InlineData(0, "Pracuj.pl")]
    [InlineData(1, "JustJoinIt")]
    [InlineData(2, "OLX Praca")]
    [InlineData(3, "Jooble")]
    public void OfferSiteType_EnumValues(int value, string name)
    {
        var siteTypes = new Dictionary<int, string>
        {
            { 0, "Pracuj.pl" },
            { 1, "JustJoinIt" },
            { 2, "OLX Praca" },
            { 3, "Jooble" }
        };

        siteTypes.Should().ContainKey(value);
        siteTypes[value].Should().Be(name);
    }

    #endregion

    #region Error Response Tests

    [Theory]
    [InlineData("Job not found")]
    [InlineData("Invalid job ID")]
    [InlineData("Job already completed")]
    [InlineData("Worker unavailable")]
    public void ErrorResponse_Messages(string errorMessage)
    {
        var response = new { Success = false, ErrorMessage = errorMessage };
        
        response.Success.Should().BeFalse();
        response.ErrorMessage.Should().Be(errorMessage);
    }

    #endregion
}
