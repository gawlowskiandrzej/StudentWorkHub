using FluentAssertions;
using Newtonsoft.Json;
using worker.Models.DTO;
using Offer_collector.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class WorkerQueueTests
{
    [Fact]
    public void JobTask_Deserializes_FromEmptyJsonWithoutThrowing()
    {
        var json = "{}";

        var task = JsonConvert.DeserializeObject<JobTask>(json);

        task.Should().NotBeNull();
        task!.JobId.Should().BeNull();
        task.SearchFilters.Should().BeNull();
    }

    [Fact]
    public void JobTask_SerializesAndDeserializes_WithMinimalFields()
    {
        var task = new JobTask
        {
            JobId = "job-1",
            BatchSize = 0,
            BatchLimit = 0,
            Offset = 0,
            SiteTypeId = 1,
            SearchFilters = null!
        };

        var json = JsonConvert.SerializeObject(task);
        var roundtrip = JsonConvert.DeserializeObject<JobTask>(json);

        roundtrip.Should().NotBeNull();
        roundtrip!.JobId.Should().Be("job-1");
    }

    [Fact]
    public void JobInfo_ToDto_HandlesNullsGracefully()
    {
        var jobInfo = new JobInfo
        {
            JobId = null!,
            Status = BathStatus.pending,
            BathList = new List<string>(),
            ErrorMessage = new List<string>(),
            TotalBatches = 0,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow
        };

        var dto = jobInfo.ToJobInfoDto();

        dto.JobId.Should().BeNull();
        dto.Status.Should().Be("pending");
    }
}
