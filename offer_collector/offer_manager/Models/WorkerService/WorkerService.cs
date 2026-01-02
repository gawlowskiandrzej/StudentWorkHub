using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Interfaces;
using StackExchange.Redis;
using worker.Models.DTO;

namespace offer_manager.Models.WorkerService
{
    public class WorkerService : IWorkerService
    {
        private readonly IConnectionMultiplexer _redis;

        public WorkerService()
        {
            
        }
        public WorkerService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public async Task<JobInfo?> GetJobAsync(string jobId)
        {
            IDatabase db = _redis.GetDatabase();
            RedisValue jobData = await db.StringGetAsync($"job:{jobId}");
            if (jobData.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<JobInfo>(jobData!);
        }

        public async Task<string> CreateJobAsync(SearchFilters filters, OfferSitesTypes offerSitetype, int batchSize, int batchLimit, int offset)
        {
            string jobId = Guid.NewGuid().ToString();
            IDatabase db = _redis.GetDatabase();

            JobTask jobTask = new JobTask
            {
                JobId = jobId,
                SiteTypeId = (int)offerSitetype,
                BatchSize = batchSize,
                BatchLimit = batchLimit,
                SearchFilters = filters,
                Offset = offset
            };

            await db.ListLeftPushAsync("jobs_queue", JsonConvert.SerializeObject(jobTask));

            JobInfo jobInfo = new JobInfo
            {
                JobId = jobId,
                Status = BathStatus.queued,
                TotalBatches = 0,
                BathList = new List<List<string>>(),
                ErrorMessage = new List<string>(),
            };

            await db.StringSetAsync($"job:{jobId}", JsonConvert.SerializeObject(jobInfo));

            return jobId;
        }

        public (bool, string) DeleteJobAsync(List<string> jobIds)
        {
            try
            {
                foreach (var jobId in jobIds)
                {
                    IDatabase db = _redis.GetDatabase();
                    db.KeyDelete($"job:{jobId}");
                }
                return (true, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
