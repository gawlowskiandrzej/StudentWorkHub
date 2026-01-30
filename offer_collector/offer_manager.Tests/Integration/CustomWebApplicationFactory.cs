using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using offer_manager.Interfaces;
using offer_manager.Models.Dictionaries;
using offer_manager.Models.WorkerService;
using Offer_collector.Models.DatabaseService;
using StackExchange.Redis;
using Users;

namespace offer_manager.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // remove real database service
            services.RemoveAll<DBService>();
            
            // remove real Redis connection
            services.RemoveAll<IConnectionMultiplexer>();
            
            // remove real worker service
            services.RemoveAll<IWorkerService>();

            // remove real AI service
            services.RemoveAll<IAIProcessor>();
            
            // remove real User service
            services.RemoveAll<User>();

            // register TestUser fake
            services.AddSingleton<User>(_ => new TestUser("test_user", "test_pass", "127.0.0.1", 5432, "db_test"));

            // mocked IDatabaseService
            var mockDbService = new Mock<IDatabaseService>();
            mockDbService.Setup(d => d.GetDictionaries(It.IsAny<List<string>>()))
                .ReturnsAsync(new DictionariesDto 
                { 
                    EmploymentSchedules = new List<string>(), 
                    EmploymentType = new List<string>(), 
                    SalaryPeriods = new List<string>() 
                });
            mockDbService.Setup(d => d.GetDictionariesWithIds(It.IsAny<List<string>>()))
                .ReturnsAsync(new { });
            services.AddSingleton(mockDbService.Object);

            // mocked AIProcessor
            var mockAiProcessor = new Mock<IAIProcessor>();
            services.AddSingleton(mockAiProcessor.Object);

            // mocked Redis
            var mockRedis = new Mock<IConnectionMultiplexer>();
            var mockDatabase = new Mock<IDatabase>();
            mockRedis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                     .Returns(mockDatabase.Object);
            services.AddSingleton(mockRedis.Object);

            // mocked WorkerService
            var mockWorkerService = new Mock<IWorkerService>();
            services.AddScoped(_ => mockWorkerService.Object);
            services.AddScoped<WorkerService>(); // Register the concrete class too
            

        });
    }
}

public abstract class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory Factory;

    protected IntegrationTestBase(CustomWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }
}
