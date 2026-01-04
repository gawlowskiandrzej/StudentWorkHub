using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using shared_models.Dto;
using worker.Models.DTO;

namespace offer_manager.Interfaces
{
    public interface IWorkerService
    {
        Task<JobInfo?> GetJobAsync(string jobId);
        Task<string> CreateJobAsync(SearchDto filters,OfferSitesTypes offerSitetype, int batchSize, int batchLimit, int offset);
        (bool, string) DeleteJobAsync(List<string> jobIds);

    }
}
