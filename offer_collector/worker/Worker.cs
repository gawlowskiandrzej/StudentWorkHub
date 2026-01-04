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
           // await PreloadDbHashesToRedis(stoppingToken);

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
        _logger.LogInformation($"Worker is now processing job {task.JobId} on site {((OfferSitesTypes)task.SiteTypeId)}");
        try
        {
            int duplicated = 0;
            await foreach (var (offers, errors) in fetcher.FetchOffers(task.SearchFilters, cancellation, task.Offset, bathSize: task.BatchSize))
            {
                bool anyNewOffersFethed = false;
                
                if (offers.Count == 0) break;
                foreach (var offer in offers)
                {
                    string hash = offer.url;

                    bool isNew = await _redisDb.SetAddAsync("offers:db:index", hash);
                    if (!isNew) { anyNewOffersFethed = true; duplicated++; continue; }

                    offer.description += $".&.{task.SearchFilters.Keyword}.&.";
                    job.BathList.Add(JsonConvert.SerializeObject(offer));
                    job.ErrorMessage.AddRange(errors);
                }
                if (job.BathList.Count >= task.BatchSize || (job.BathList.Count == 0 && anyNewOffersFethed == false) || (fetcher.scrapper?.maxOfferCount == duplicated))
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
}
