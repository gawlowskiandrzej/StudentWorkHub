using Offer_collector.Models.DatabaseService;
using offer_manager.Interfaces;
using StackExchange.Redis;

namespace offer_manager.Models.Others
{
    public class RedisPreloadHostedService : BackgroundService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabaseService _dbService;
        private readonly ILogger<RedisPreloadHostedService> _logger;

        public RedisPreloadHostedService(
            IConnectionMultiplexer redis,
            DBService dbService,
            ILogger<RedisPreloadHostedService> logger)
        {
            _redis = redis;
            _dbService = dbService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var db = _redis.GetDatabase();

            string lockKey = "offers:db:index:bootstrap:lock";
            string readyKey = "offers:db:index:ready";

            bool isLeader = await db.StringSetAsync(
                lockKey,
                Environment.MachineName,
                TimeSpan.FromMinutes(10),
                When.NotExists);

            if (!isLeader)
            {
                _logger.LogInformation("Preload już wykonany przez inną instancję.");
                return;
            }

            _logger.LogInformation("Czyszczenie Redis i preload hashów...");

            var server = _redis.GetServer(_redis.GetEndPoints().First());
            await server.FlushDatabaseAsync();

            var rows = await _dbService.GetUrlHashes();

            foreach (var row in rows)
            {
                if (stoppingToken.IsCancellationRequested) break;

                await db.SetAddAsync("offers:db:index", row);
            }

            await db.StringSetAsync(readyKey, "1");

            _logger.LogInformation("Preload Redis zakończony.");
        }
    }

}
