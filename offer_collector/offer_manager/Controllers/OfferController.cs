using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.AI;
using Offer_collector.Models.DatabaseService;
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
    [Route("api/offers")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        private readonly DBService _databaseService;
        private readonly PaginationService _pagination;
        private readonly FilterService _filterService;
        private readonly StaticSettings _statSettings;
        private readonly AIProcessor _aiService;

        public OfferController(IWorkerService workerService, PaginationService pagination, FilterService filterService, StaticSettings statSettings, AIProcessor aiService, DBService databaseService)
        {
            _workerService = workerService;
            _databaseService = databaseService;
            _pagination = pagination;
            _filterService = filterService;
            _statSettings = statSettings;
            _aiService = aiService;
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
        public async Task<IActionResult> GetScrappedOffers([FromBody] jobIdsDto jobIds, int batchId = -1, bool usedAi = false, bool addToDatabase = false)
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
            .Min(j => j.StartTime);

            DateTime latestEnd = completedJobs
                .Max(j => j.EndTime);

            TimeSpan totalDuration = latestEnd - earliestStart;
            IEnumerable<string> errors = completedJobs.Where(j => j.ErrorMessage != null && j.ErrorMessage.Count > 0)
                .SelectMany(j => j.ErrorMessage!);

            List<UnifiedOfferSchemaClass> unifiedOffers = mergedOffers.Select(_ => JsonConvert.DeserializeObject<UnifiedOfferSchemaClass>(_) ?? new UnifiedOfferSchemaClass()).GroupBy(_ => _.url).Select(_ => _.First()).ToList();
            mergedOffers = unifiedOffers.Select(_ => JsonConvert.SerializeObject(_)).ToList();

            if (usedAi)
            {
                (List<string?>, List<string>) aiResponse = await _aiService.ProcessUnifiedSchemas(mergedOffers, await _databaseService.GetSystemPromptParamsAsync());
                mergedOffers = aiResponse.Item1.Where(_ => !string.IsNullOrEmpty(_)).Select(_ => _!).ToList();
                errors = errors.Concat(aiResponse.Item2);
            }
            if (addToDatabase)
            {
                (bool isSuccess, List<string> dbErrors) = await _databaseService.AddOffersToDatabaseAsync(mergedOffers);
                errors = errors.Concat(dbErrors);
            }


            return Ok(new
            {
               batch = mergedOffers,
               errors = errors,
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
