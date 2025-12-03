using Offer_collector.Models;
using Offer_collector.Models.UrlBuilders;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace offer_manager.Interfaces
{
    public interface IDatabaseService
    {
        Task<(bool, List<string>)> AddOffersToDatabaseAsync(List<string> aiOffers);
        Task<Dictionary<string, string>> GetSystemPromptParamsAsync();
        Task<FrozenSet<UOS?>> GetOffers(SearchFilters searchFilters);
    }
}
