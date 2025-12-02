using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Interfaces;
using offer_manager.Models.FilterService;
using offer_manager.Models.Others;
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
        private readonly StaticSettings _statSettings;

        public OfferController(IWorkerService workerService, IDatabaseService databaseService, PaginationService pagination, FilterService filterService, StaticSettings statSettings)
        {
            _workerService = workerService;
            _databaseService = databaseService;
            _pagination = pagination;
            _filterService = filterService;
            _statSettings = statSettings;
        }

        [HttpGet("getOffersDatabase")]
        public async Task<IActionResult> GetOffersFromDatabase(SearchFilters searchFilter, int pageOffset = 0, int offerPerpage = -1)
        {
            FrozenSet<UOS?> dbOffers = await _databaseService.GetOffers(searchFilter);

            PaginationResponse paginationResponse = _pagination.CreatePagedResult(dbOffers.Where(o => o != null).Cast<UOS>(), pageOffset, offerPerpage);

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
            var pendingJobs = new List<object>();

            foreach (var id in jobIds.JobIds)
            {
                JobInfo? job = await _workerService.GetJobAsync(id);

                if (job == null)
                {
                    pendingJobs.Add(new { id, reason = "not_found" });
                    continue;
                }

                if (job.Status != BathStatus.completed)
                {
                    pendingJobs.Add(new { id, status = job.Status.ToString() });
                    continue;
                }

                if (batchId > 0)
                {
                    if (job.BathList == null || job.BathList.Count <= batchId)
                    {
                        pendingJobs.Add(new
                        {
                            id,
                            reason = "batch_not_ready",
                            batch = batchId
                        });
                        continue;
                    }
                }

                completedJobs.Add(job);
            }

            if (pendingJobs.Count > 0)
            {
                return Ok(new
                {
                    status = "pending",
                    waitingFor = pendingJobs
                });
            }

            List<string> mergedOffers = new List<string>();

            foreach (var job in completedJobs)
            {
                if (batchId > -1)
                    mergedOffers.AddRange(job.BathList[batchId]);
                else
                    mergedOffers.AddRange(job.BathList.SelectMany(b => b));
            }

            DateTime earliestStart = completedJobs
            .Where(j => j.StartTime != null)
            .Min(j => j.StartTime);

            DateTime latestEnd = completedJobs
                .Where(j => j.EndTime != null)
                .Max(j => j.EndTime);

            TimeSpan totalDuration = latestEnd - earliestStart;


            return Ok(new
            {
               batch = mergedOffers,
               dynamicFilter = _filterService.GetDynamicFilters(FrozenSet.ToFrozenSet<UOS?>(mergedOffers.Select(o => JsonConvert.DeserializeObject<UOS>(o)))),
                scrappingDuration = totalDuration.TotalSeconds.ToString("F2")
            });
        }

        // POST api/<OfferController>
        [HttpPost]
        public async Task<IActionResult> CreateScrappers([FromBody] SearchFilters filters, int batchSize = 5, int batchLimit = 1, int offset = 0)
        {
            List<string>jobIds = new List<string>();
            for (int i = 0; i < _statSettings.NumberOfAvailablePortals * batchLimit; i++)
            {
                jobIds.Add(await _workerService.CreateJobAsync(filters, (OfferSitesTypes)((i%_statSettings.NumberOfAvailablePortals) + 1), batchSize, batchLimit, offset+(i / _statSettings.NumberOfAvailablePortals)));

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
