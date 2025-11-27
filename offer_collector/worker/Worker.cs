using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.UrlBuilders;
using StackExchange.Redis;
using System.Collections.Generic;
using worker.Models;
using worker.Models.DTO;

namespace worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _redisDb;

    public Worker(ILogger<Worker> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _redis = redis;
        _redisDb = _redis.GetDatabase();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started...");
        while (!stoppingToken.IsCancellationRequested)
        {
            var taskData = await _redisDb.ListRightPopAsync("jobs_queue");
            if (taskData.IsNullOrEmpty)
            {
                await Task.Delay(500, stoppingToken); // brak zadañ, czekamy
                continue;
            }

            var jobTask = JsonConvert.DeserializeObject<JobTask>(taskData!);

            await _redisDb.StringSetAsync($"job:{jobTask?.JobId}", JsonConvert.SerializeObject(jobTask));
            if (jobTask != null)
            {
                await ProcessJob(jobTask, stoppingToken);
            }
        }
    }
    async Task ProcessJob(JobTask task, CancellationToken cancellation)
    {
        Fetcher fetcher = new Fetcher(
            offerSitesTypes: (OfferSitesTypes)task.SiteTypeId,
            isUsingAi: false,
            saveToLocalFile: false
        );

        var jobKey = $"job:{task.JobId}";

        var jobData = await _redisDb.StringGetAsync(jobKey);
        JobInfo? job = JsonConvert.DeserializeObject<JobInfo>(jobData!);
        job.Status = "running";
        await _redisDb.StringSetAsync(jobKey, JsonConvert.SerializeObject(job));
        job.TotalBatches = 0;
        job.BathList = new List<List<string>>();
        job.ErrorMessage = new List<string>();
        try
        {
            await foreach (var (offers, errors) in fetcher.FetchOffers(task.SearchFilters, cancellation))
            {

                job.BathList.Add(offers);
                job.ErrorMessage.AddRange(errors);
                job.TotalBatches += 1;
                await _redisDb.StringSetAsync(jobKey, JsonConvert.SerializeObject(job));
                if (job.TotalBatches >= task.BatchLimit)
                    break;
            }

            job.Status = "completed";
            await _redisDb.StringSetAsync(jobKey, JsonConvert.SerializeObject(job));
        }
        catch (Exception ex)
        {
            job.Status = "error";
            job.ErrorMessage.AddRange(ex.Message);
        }
    }
}
