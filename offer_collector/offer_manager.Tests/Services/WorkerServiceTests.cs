using FluentAssertions;
using offer_manager.Models.WorkerService;
using Xunit;

namespace offer_manager.Tests.Services;

public class WorkerServiceTests
{
    [Fact]
    public void WorkerService_JobStates_CanBeSet()
    {
        var dto = new jobIdsDto();
        dto.JobIds.Should().NotBeNull();
        
        dto.JobIds.Add("test-job-1");
        dto.JobIds.Should().Contain("test-job-1");
    }
}
