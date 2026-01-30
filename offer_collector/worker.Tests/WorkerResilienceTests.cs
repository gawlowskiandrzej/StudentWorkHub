using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using worker;
using worker.Models.DTO;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System;
using OffersConnector;

namespace offer_collector.Tests
{
    public class WorkerResilienceTests
    {
        [Fact]
        public async Task ExecuteAsync_WhenPoisonPillReceived_ShouldNotTerminateWorkerPermanently()
        {
            var loggerMock = new Mock<ILogger<Worker>>();
            var redisMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            redisMock.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbMock.Object);

            var queue = new System.Collections.Generic.Queue<RedisValue>();
            queue.Enqueue("INVALID_JSON_POISON_PILL");
            queue.Enqueue(JsonConvert.SerializeObject(new JobTask { JobId = Guid.NewGuid().ToString(), SiteTypeId = 1 }));

            dbMock.Setup(d => d.ListRightPopAsync("jobs_queue", It.IsAny<CommandFlags>()))
                  .Returns(() => Task.FromResult(queue.Count > 0 ? queue.Dequeue() : RedisValue.Null));

            var worker = new Worker(loggerMock.Object, redisMock.Object);

            using var cts = new CancellationTokenSource();
            var executeTask = worker.StartAsync(cts.Token);

            await Task.Delay(1500);
            cts.Cancel();
            await executeTask;

            dbMock.Verify(d => d.ListRightPopAsync("jobs_queue", It.IsAny<CommandFlags>()), Times.AtLeast(2));
        }

        [Fact]
        public async Task ExecuteAsync_WhenRedisFails_ShouldRetryAndNotDie()
        {
            var loggerMock = new Mock<ILogger<Worker>>();
            var redisMock = new Mock<IConnectionMultiplexer>();
            var dbMock = new Mock<IDatabase>();

            redisMock.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(dbMock.Object);

            var callCount = 0;
            dbMock.Setup(d => d.ListRightPopAsync("jobs_queue", It.IsAny<CommandFlags>()))
                  .Returns(() => {
                      callCount++;
                      if (callCount == 1) throw new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Mock failure");
                      return Task.FromResult(RedisValue.Null);
                  });

            var worker = new Worker(loggerMock.Object, redisMock.Object);

            using var cts = new CancellationTokenSource();
            var executeTask = worker.StartAsync(cts.Token);
            await Task.Delay(1000); 
            cts.Cancel();
            await executeTask;

            dbMock.Verify(d => d.ListRightPopAsync("jobs_queue", It.IsAny<CommandFlags>()), Times.AtLeast(2));
        }
    }
}
