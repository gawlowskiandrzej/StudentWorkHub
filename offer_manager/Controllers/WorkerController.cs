using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models.UrlBuilders;
using StackExchange.Redis;
using worker.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace offer_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public WorkerController(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        // GET api/<WorkerController>/5
        [HttpGet("{jobId}")]
        public async Task<IActionResult> Get(string jobId)
        {
            IDatabase db = _redis.GetDatabase();
            RedisValue jobData = await db.StringGetAsync($"job:{jobId}");
            if (jobData.IsNullOrEmpty)
                return NotFound("Job not found");

            var job = JsonConvert.DeserializeObject<JobInfo>(jobData!);
            return Ok(job);

        }

        // POST api/<WorkerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SearchFilters searchFilters, int batchSize = 5, int bathLimit = 1 , bool useAi = false)
        {
            string jobId = Guid.NewGuid().ToString();

            var jobTask = new JobTask
            {
                JobId = jobId,
                SiteTypeId = 4,
                BatchSize = batchSize,
                BatchLimit = bathLimit,
                SearchFilters = searchFilters
            };
            IDatabase database = _redis.GetDatabase();
            await database.ListLeftPushAsync("jobs_queue", JsonConvert.SerializeObject(jobTask));

            var jobInfo = new JobInfo
            {
                JobId = jobId,
                Status = "queued",
                TotalBatches = 0,
                BathList = new List<List<string>>(),
                ErrorMessage = new List<string>(),
            };
            await database.StringSetAsync($"job:{jobId}", JsonConvert.SerializeObject(jobInfo));

            return Ok(new { jobId = jobId });
        }
    }
}
