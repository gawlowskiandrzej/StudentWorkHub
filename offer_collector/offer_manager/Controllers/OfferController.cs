using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.AI;
using Offer_collector.Models.DatabaseService;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Interfaces;
using offer_manager.Models.FilterService;
using offer_manager.Models.Offers.dtoObjects;
using offer_manager.Models.Offers.DtoObjects;
using offer_manager.Models.Others;
using offer_manager.Models.PaginationService;
using offer_manager.Models.WorkerService;
using shared_models.Dto;
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
        private readonly IMapper _mapper;
        private readonly PaginationService _pagination;
        private readonly FilterService _filterService;
        private readonly StaticSettings _statSettings;
        private readonly AIProcessor _aiService;

        public OfferController(IWorkerService workerService, PaginationService pagination, FilterService filterService, StaticSettings statSettings, AIProcessor aiService, DBService databaseService, IMapper mapper)
        {
            _workerService = workerService;
            _databaseService = databaseService;
            _mapper = mapper;
            _pagination = pagination;
            _filterService = filterService;
            _statSettings = statSettings;
            _aiService = aiService;
        }

        [HttpPost("offers-database")]
        public async Task<IActionResult> GetOffersFromDatabase(SearchDto searchFilter, int pageOffset = 0, int perpage = -1)
        {
            try
            {
                FrozenSet<UOS?> dbOffers = await _databaseService.GetOffers(searchFilter);

                var filteredOffers = dbOffers
                .Where(o => o != null)
                .Where(o => string.IsNullOrEmpty(searchFilter.EmploymentType) || o!.Employment.Types.Contains(searchFilter.EmploymentType))
                .Where(o => string.IsNullOrEmpty(searchFilter.SalaryPeriod) || o!.Salary.Period == searchFilter.SalaryPeriod)
                .Where(o => string.IsNullOrEmpty(searchFilter.EmploymentSchedule) || o!.Employment.Schedules.Contains(searchFilter.EmploymentSchedule)).Cast<UOS>();

                List<OfferDTO> offersDTO = _mapper.Map<List<OfferDTO>>(filteredOffers);

                PaginationResponse<OfferDTO> paginationResponse = _pagination.CreatePagedResult(offersDTO, pageOffset, perpage);

                return Ok(new DatabaseOffersResponseDto()
                {
                    Pagination = paginationResponse,
                    DynamicFilter = _filterService.GetDynamicFilters(dbOffers)
                });
            }
            catch (Exception e)
            {
                return BadRequest(new DatabaseOffersResponseDto()
                {
                   Error = new List<string> { e.Message },
                });
            }
            
        }

        // GET api/<OfferController>/5
        [HttpPost("offers-scrapped")]
        public async Task<IActionResult> GetScrappedOffers([FromBody] jobIdsDto jobIds, int batchId = -1, bool usedAi = false, bool addToDatabase = false)
        {

            try
            {
                var completedJobs = new List<JobInfo>();
                var pendingJobs = new List<JobInfoDto>();
                foreach (var id in jobIds.JobIds)
                {
                    JobInfo? job = await _workerService.GetJobAsync(id);
                    if (job == null)
                    {
                        pendingJobs.Add(new JobInfoDto { JobId = id, Status = BathStatus.notfound.ToString() });
                        continue;
                    }

                    if (job.Status != BathStatus.completed)
                    {
                        pendingJobs.Add(new JobInfoDto { JobId = id, Status = job.Status.ToString() });
                        continue;
                    }

                    completedJobs.Add(job);
                }

                if (pendingJobs.Count > 0)
                {
                    return Ok(new ScrappedOffersResponseDto {ScrappingStatus = new ScrappingStatus { jobInfos = pendingJobs } } );
                }

                List<string> mergedOffers = new List<string>();

                foreach (var job in completedJobs)
                {
                    mergedOffers.AddRange(job.BathList);
                }

                DateTime earliestStart = completedJobs
                .Min(j => j.StartTime);

                DateTime latestEnd = completedJobs
                    .Max(j => j.EndTime);

                TimeSpan totalDuration = latestEnd - earliestStart;
                IEnumerable<string> errors = completedJobs.Where(j => j.ErrorMessage != null && j.ErrorMessage.Count > 0)
                    .SelectMany(j => j.ErrorMessage!);

                List<UOS> unifiedOffers = mergedOffers.Select(_ => JsonConvert.DeserializeObject<UOS>(_) ?? new UOS()).GroupBy(_ => _.Url).Select(_ => _.First()).ToList();
                if (usedAi)
                {
                    (List<string?>, List<string>) aiResponse = await _aiService.ProcessUnifiedSchemas(mergedOffers, await _databaseService.GetSystemPromptParamsAsync());
                    mergedOffers = aiResponse.Item1.Where(_ => !string.IsNullOrEmpty(_)).Select(_ => _!).ToList();
                    errors = errors.Concat(aiResponse.Item2);
                }
                if (addToDatabase)
                {
                    (bool isSuccess, IEnumerable<long?> offerIds, List<string> dbErrors) = await _databaseService.AddOffersToDatabaseAsync(mergedOffers);
                    errors = errors.Concat(dbErrors);
                    unifiedOffers = mergedOffers
                    .Select((json, index) => {
                        var offer = JsonConvert.DeserializeObject<UOS>(json) ?? new UOS();
                        offer.Id = offerIds.ElementAtOrDefault(index);
                        return offer;
                    })
                    .ToList();
                }

                List<OfferDTO> responseOffers = _mapper.Map<List<OfferDTO>>(unifiedOffers);
                return Ok(new ScrappedOffersResponseDto()
                {
                    DatabaseOffersResponse = new DatabaseOffersResponseDto
                    {
                        Pagination = _pagination.CreatePagedResult(responseOffers, 0, -1),
                        DynamicFilter = _filterService.GetDynamicFilters(FrozenSet.ToFrozenSet<UOS?>(unifiedOffers))
                    },
                    ScrappingStatus = new ScrappingStatus { jobInfos = completedJobs.Select(_ => _.ToJobInfoDto()).ToList(), ScrappingDone = true, ScrappingDuration = totalDuration.TotalSeconds.ToString("F2") }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ScrappedOffersResponseDto()
                {
                    DatabaseOffersResponse = new DatabaseOffersResponseDto
                    {
                         Error = new List<string> { e.Message }
                    }
                });
            }
            
        }

        // POST api/<OfferController>
        [HttpPost("create-scrappers")]
        public async Task<IActionResult> CreateScrappers([FromBody] SearchDto filters, int batchSize = 5, int batchLimit = 1, int offset = 0)
        {
            List<string>jobIds = new List<string>();
            for (int i = 0; i < _statSettings.NumberOfAvailablePortals * batchLimit; i++)
            {
                jobIds.Add(await _workerService.CreateJobAsync(filters, (OfferSitesTypes)((i%_statSettings.NumberOfAvailablePortals) + 1), batchSize, batchLimit, offset+(i / _statSettings.NumberOfAvailablePortals)));

            }
            
            return Ok(new jobIdsDto
            {
                JobIds = jobIds
            });
        }
        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
