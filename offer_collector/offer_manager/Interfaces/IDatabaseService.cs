using offer_manager.Models.Dictionaries;
using shared_models.Dto;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace offer_manager.Interfaces
{
    public interface IDatabaseService
    {
        Task<(bool,IEnumerable<long?>,List<string>)> AddOffersToDatabaseAsync(List<string> aiOffers);
        Task<Dictionary<string, string>> GetSystemPromptParamsAsync();
        Task<FrozenSet<UOS?>> GetOffers(SearchDto searchFilters);
        Task<IEnumerable<string>> GetUrlHashes();
        Task<DictionariesDto> GetDictionaries(List<string> dicNames);
        Task<object> GetDictionariesWithIds(List<string> dicNames);
    }
}
