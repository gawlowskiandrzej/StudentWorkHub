using FluentAssertions;
using Xunit;
using Moq;
using offer_manager.Controllers;
using shared_models.Dto;
using Offer_collector.Models;
using offer_manager.Models.WorkerService;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace offer_manager.Tests.Controllers
{
    public class WorkerControllerTests
    {
        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenBatchSizeIsInvalid()
        {
            var mockRedis = new Mock<IConnectionMultiplexer>();
            var mockDb = new Mock<IDatabase>();
            mockRedis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(mockDb.Object);
            
            var service = new WorkerService(mockRedis.Object);
            var controller = new WorkerController(service);

            var searchDto = new SearchDto { Keyword = "test" };

            var result = await controller.Post(searchDto, OfferSitesTypes.Aplikujpl, batchSize: -1, bathLimit: 1);

            result.Should().BeOfType<BadRequestObjectResult>("Negative batch size should not be allowed");
        }
    }
}
