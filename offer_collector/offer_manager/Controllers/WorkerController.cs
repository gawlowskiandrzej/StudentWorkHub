using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Models.WorkerService;
using shared_models.Dto;
using StackExchange.Redis;
using worker.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace offer_manager.Controllers
{
    [Route("api/worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        // GET api/<WorkerController>/5
        [HttpGet("{jobId}")]
        public async Task<IActionResult> Get(string jobId)
        {
            JobInfo? job = await _workerService.GetJobAsync(jobId);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        // POST api/<WorkerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SearchDto searchFilters, OfferSitesTypes offerSites = OfferSitesTypes.Aplikujpl ,int batchSize = 5, int bathLimit = 1, int offset = 0)
        {
            string jobId = await _workerService.CreateJobAsync(searchFilters,  offerSites, batchSize, bathLimit,offset );
            if (string.IsNullOrEmpty(jobId))
                return NotFound();
            
            return Ok(new { jobId = jobId });
        }
    }
}
