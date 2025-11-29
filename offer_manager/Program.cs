using Offer_collector.Models.DatabaseService;
using offer_manager.Interfaces;
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

            // Add services to the container.

            builder.Services.AddControllers();

            var redisConfig = builder.Configuration.GetSection("RedisServer");
            string redisHostname = redisConfig.GetValue<string>("Host");
            int redisPort = redisConfig.GetValue<int>("Port");
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{redisHostname}:{redisPort}"));

            var dbConfig = builder.Configuration.GetSection("DatabaseSettings");
            string? dbHost = dbConfig.GetValue<string>("Host");
            int dbPort = dbConfig.GetValue<int>("Port");
            string? dbUsername = dbConfig.GetValue<string>("Username");
            string? dbPassword = dbConfig.GetValue<string>("Password");
            string? dbName = dbConfig.GetValue<string>("Database");

            if (dbHost is null)
                throw new InvalidOperationException("Database host is not configured.");
            if (dbUsername is null)
                throw new InvalidOperationException("Database username is not configured.");
            if (dbPassword is null)
                throw new InvalidOperationException("Database password is not configured.");
            if (dbName is null)
                throw new InvalidOperationException("Database name is not configured.");

            // Register PgConnector as a singleton
            builder.Services.AddSingleton<PgConnector>(new PgConnector(
                dbUsername,
                dbPassword,
                dbHost,
                dbPort,
                dbName
            ));

            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<IDatabaseService, DBService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
