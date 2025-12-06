using AngleSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Offer_collector.Models.AI;
using Offer_collector.Models.DatabaseService;
using offer_manager.Interfaces;
using offer_manager.Models.FilterService;
using offer_manager.Models.Others;
using offer_manager.Models.PaginationService;
using offer_manager.Models.WorkerService;
using OffersConnector;
using StackExchange.Redis;

namespace offer_manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var redisConfig = builder.Configuration.GetSection("RedisServer");
            var apiKeysConfig = builder.Configuration.GetSection("ApiKeys");
            var geminiKey = apiKeysConfig.GetValue<string>("GeminiKey");
            builder.Services.Configure<StaticSettings>(
                builder.Configuration.GetSection("StaticSettings")
            );
            string redisHostname = redisConfig.GetValue<string>("Host") ?? "";
            int redisPort = redisConfig.GetValue<int>("Port");

            DatabaseSettings dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? new DatabaseSettings();

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<StaticSettings>>().Value
            );
            builder.Services.AddSingleton(new AIProcessor(geminiKey: geminiKey ?? ""));

            builder.Services.AddSingleton(new DBService(dbSettings.Username, dbSettings.Password, dbSettings.Host, dbSettings.Port, dbSettings.Database));

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse($"{redisHostname}:{redisPort}");
                options.AllowAdmin = true;

                var redis = ConnectionMultiplexer.Connect(options);

                var server = redis.GetServer(redisHostname ?? "", redisPort);
                server.FlushDatabase();

                return redis;
            });

            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<PaginationService>();
            builder.Services.AddScoped<FilterService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            //app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
    }
}
