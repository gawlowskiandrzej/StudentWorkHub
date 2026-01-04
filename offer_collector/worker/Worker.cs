using Newtonsoft.Json;
using Offer_collector.Models;
using OffersConnector;
using StackExchange.Redis;
using System.Diagnostics;
using worker.Models;
using worker.Models.DTO;
using worker.Models.Tools;

namespace worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly PgConnector _dbService;
    private readonly IDatabase _redisDb;

    public Worker(ILogger<Worker> logger, IConnectionMultiplexer redis, PgConnector dbService)
    {
        _logger = logger;
        _redis = redis;
        _dbService = dbService;
        _redisDb = _redis.GetDatabase();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await PreloadDbHashesToRedis(stoppingToken);

            _logger.LogInformation("Worker started...");
            while (!stoppingToken.IsCancellationRequested)
            {
                var taskData = await _redisDb.ListRightPopAsync("jobs_queue");
                if (taskData.IsNullOrEmpty)
                {
                    //_logger.LogInformation($"Worker is waiting for a job");
                    await Task.Delay(500, stoppingToken); // brak zadañ, czekamy
                    continue;
                }

                var jobTask = JsonConvert.DeserializeObject<JobTask>(taskData!);

                await _redisDb.StringSetAsync($"job:{jobTask?.JobId}", JsonConvert.SerializeObject(jobTask));

                if (jobTask != null)
                {
                    _logger.LogInformation($"Worker is now processing job {jobTask.JobId}");

                    var stopwatch = Stopwatch.StartNew(); // Start pomiaru czasu
                    await ProcessJob(jobTask, stoppingToken);
                    stopwatch.Stop(); // Zatrzymanie pomiaru czasu

                    _logger.LogInformation($"Job {jobTask.JobId} finished in {stopwatch.Elapsed.TotalSeconds:F2} seconds");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogInformation($"An error occured {e.Message}");
        }
       
    }
    async Task ProcessJob(JobTask task, CancellationToken cancellation)
    {
        Fetcher fetcher = new Fetcher(
            offerSitesTypes: (OfferSitesTypes)task.SiteTypeId,
            saveToLocalFile: false
        );

        var jobKey = $"job:{task.JobId}";

        var jobData = await _redisDb.StringGetAsync(jobKey);
        JobInfo? job = JsonConvert.DeserializeObject<JobInfo>(jobData!);
        job.Status = BathStatus.running;
        job.StartTime = DateTime.UtcNow;
        await _redisDb.StringSetAsync(jobKey, JsonConvert.SerializeObject(job));
        job.TotalBatches = 0;
        job.BathList = new List<string>();
        job.ErrorMessage = new List<string>();
        try
        {
            await foreach (var (offers, errors) in fetcher.FetchOffers(task.SearchFilters, cancellation, task.Offset, bathSize: task.BatchSize))
            {
                bool anyNewOffersFethed = false;
                if (offers.Count == 0) break;
                foreach (var offer in offers)
                {
                    string hash = Cryptography.ComputeUrlHash(offer.url);

                    bool isNew = await _redisDb.SetAddAsync("offers:db:index", hash);
                    if (!isNew) { anyNewOffersFethed = true; continue; }

                    job.BathList.Add(JsonConvert.SerializeObject(offer));
                    job.ErrorMessage.AddRange(errors);
                }
                if (job.BathList.Count >= task.BatchSize || (job.BathList.Count == 0 && anyNewOffersFethed == false))
                    break;
            }

            if (job.BathList.Count == 0)
                job.Status = BathStatus.completed;
            else
                job.Status = BathStatus.completed;
            job.EndTime = DateTime.UtcNow;
            await _redisDb.StringSetAsync(jobKey, JsonConvert.SerializeObject(job));
        }
        catch (Exception ex)
        {
            job.Status = BathStatus.error;
            job.ErrorMessage.Add(ex.Message);
        }
    }

    private async Task PreloadDbHashesToRedis(CancellationToken cancellationToken)
    {
        string lockKey = "offers:db:index:bootstrap:lock";
        string readyKey = "offers:db:index:ready";

        // próbujemy ustawiæ lock – tylko jeden worker wykona preload
        bool isLeader = await _redisDb.StringSetAsync(lockKey, Environment.MachineName, TimeSpan.FromMinutes(10), When.NotExists);
        if (!isLeader)
        {
            // inny worker ju¿ robi preload lub zrobi³ go wczeœniej
            _logger.LogInformation("Preload hashów DB ju¿ wykonany przez inny worker.");
            return;
        }

        _logger.LogInformation("Preloading hashów z bazy do Redis SET...");

        long lastId = 0;
        const int batchSize = 10000;

        while (!cancellationToken.IsCancellationRequested)
        {
            // pobieramy batch URLi z DB
            var rows = await _dbService.GetExternalOffers();

            if (!rows.Any()) break;

            foreach (var row in rows)
            {
                string hash = Cryptography.ComputeUrlHash(row.Url);
                await _redisDb.SetAddAsync("offers:db:index", hash);
            }
            break;
        }

        // oznaczamy, ¿e preload zakoñczony
        await _redisDb.StringSetAsync(readyKey, "1");

        _logger.LogInformation("Preload hashów DB zakoñczony.");
    }
}
