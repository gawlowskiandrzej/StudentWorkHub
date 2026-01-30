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

            // mocked DBService
            var mockDbService = new Mock<DBService>("test", "test", "localhost", 5432, "test");
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
            
            // mocked User
            // Constructor args must be valid non-empty strings/ints to pass Argument checks in User constructor
            var mockUser = new Mock<User>("test_user", "test_pass", "127.0.0.1", 5432, "db_test");
            
            // State to simulate DB constraints
            var registeredEmails = new System.Collections.Concurrent.ConcurrentDictionary<string, byte>();

            // Setup Register to succeed
            mockUser.Setup(u => u.StandardRegisterAsync(
                It.IsAny<UserPasswordPolicy>(), 
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserPasswordPolicy _, string e, string _, string _, string _, CancellationToken _) => {
                    if (string.IsNullOrEmpty(e)) return (false, "Email empty");
                    if (!registeredEmails.TryAdd(e, 0)) return (false, "Email already exists");
                    return (true, "");
                });

            // Setup Auth to succeed unless "wrong" or "invalid" in password
            mockUser.Setup(u => u.StandardAuthAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string u, string p, bool? r, CancellationToken c) => {
                     var pass = (p ?? "").ToLower();
                     if (pass.Contains("wrong") || pass.Contains("invalid"))
                        return (false, "Invalid credentials", null, null);
                     return (true, "", "token_abc123", 1L);
                });

            // Also mock AuthWithTokenAsync if used
            mockUser.Setup(u => u.AuthWithTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string t, CancellationToken _) => {
                     if (string.IsNullOrEmpty(t) || t == "invalid_token" || t.Contains("invalid")) 
                        return (false, "Invalid token", null, null);
                    return (true, "", "new_token", 1L);
                });

            services.AddSingleton(mockUser.Object);
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
