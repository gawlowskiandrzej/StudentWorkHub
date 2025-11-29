using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using worker.Models.DTO;

namespace offer_manager.Interfaces
{
    public interface IWorkerService
    {
        Task<JobInfo?> GetJobAsync(string jobId);
        Task<string> CreateJobAsync(SearchFilters filters,OfferSitesTypes offerSitetype, int batchSize, int batchLimit);
        (bool, string) DeleteJobAsync(List<string> jobIds);

    }
}
