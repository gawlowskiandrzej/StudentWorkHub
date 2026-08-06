using shared_models.Dto;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace offer_manager.Interfaces
{
    public interface IDatabaseService
    {
        Task<(bool,IEnumerable<long?>,List<string>)> AddOffersToDatabaseAsync(List<string> aiOffers);
        Task<Dictionary<string, object>> GetSystemPromptParamsAsync();
        Task<FrozenSet<UOS?>> GetOffers(SearchDto searchFilters);
        Task<IEnumerable<string>> GetUrlHashes();
    }
}
