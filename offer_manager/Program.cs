using Microsoft.Extensions.Options;
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
            builder.Services.Configure<StaticSettings>(
                builder.Configuration.GetSection("StaticSettings")
            );
            string redisHostname = redisConfig.GetValue<string>("Host");
            int redisPort = redisConfig.GetValue<int>("Port");
            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<StaticSettings>>().Value
            );
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse($"{redisHostname}:{redisPort}");
                options.AllowAdmin = true;

                var redis = ConnectionMultiplexer.Connect(options);

                var server = redis.GetServer(redisHostname, redisPort);
                server.FlushDatabase();

                return redis;
            });


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
