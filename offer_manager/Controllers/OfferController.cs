using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Interfaces;
using offer_manager.Models.FilterService;
using offer_manager.Models.PaginationService;
using offer_manager.Models.WorkerService;
using StackExchange.Redis;
using System.Collections.Frozen;
using System.ComponentModel;
using UnifiedOfferSchema;
using worker.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace offer_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        private readonly IDatabaseService _databaseService;
        private readonly PaginationService _pagination;
        private readonly FilterService _filterService;

        public OfferController(IWorkerService workerService, IDatabaseService databaseService, PaginationService pagination, FilterService filterService)
        {
            _workerService = workerService;
            _databaseService = databaseService;
            _pagination = pagination;
            _filterService = filterService;
        }

        [HttpGet("getOffersDatabase")]
        public async Task<IActionResult> GetOffersFromDatabase(SearchFilters searchFilter, int pageOffset = 0, int offerPerpage = -1)
        {
            FrozenSet<UOS?> dbOffers = await _databaseService.GetOffers(searchFilter);

            PaginationResponse paginationResponse = _pagination.CreatePagedResult(dbOffers.Where(o => o != null).Cast<UOS>(), pageOffset, offerPerpage);

            //dynamiczne filtry zwracane:

            // -language, language level
            // -experienceMonths, 
            // -experienceLevel
            // - education
            // - education level
            // - skill contains
            // - benefit
            // - isForUkrainians
            // - isUrgent

            return Ok(new
            {
                pagination = paginationResponse,
                dynamicFilters = _filterService.GetDynamicFilters(dbOffers)
            });
        }

        // GET api/<OfferController>/5
        [HttpGet("getOffersScrapped")]
        public async Task<IActionResult> GetScrappedOffers([FromBody] jobIdsDto jobIds, int batchId = -1)
        {

            var completedJobs = new List<JobInfo>();

            foreach (var id in jobIds.JobIds)
            {
                JobInfo? job = await _workerService.GetJobAsync(id);

                if (job == null)
                    return NotFound($"Job {id} not found.");

                if (job.Status != BathStatus.completed)
                    return Ok(new { status = "pending", waitingFor = id });

                if (batchId > 0)
                {
                    if (job.BathList == null || job.BathList.Count <= batchId)
                        return Ok(new { status = "pending", waitingForBatch = id, batch = batchId });
                }

                completedJobs.Add(job);
            }

            List<string> mergedOffers = new List<string>();

            foreach (var job in completedJobs)
            {
                if (batchId > -1)
                    mergedOffers.AddRange(job.BathList[batchId]);
                else
                    mergedOffers.AddRange(job.BathList.SelectMany(b => b));
            }



            return Ok(new
            {
               batch = mergedOffers
            });
        }

        // POST api/<OfferController>
        [HttpPost]
        public async Task<IActionResult> CreateScrappers([FromBody] SearchFilters filters, int batchSize = 5, int batchLimit = 1)
        {
            List<string>jobIds = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                jobIds.Add(await _workerService.CreateJobAsync(filters, (OfferSitesTypes)(i+1),batchSize, batchLimit));

                // zależnie od limitu batha 
            }
            
            return Ok(new 
            {
                jobIds = jobIds
            });
        }

        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
