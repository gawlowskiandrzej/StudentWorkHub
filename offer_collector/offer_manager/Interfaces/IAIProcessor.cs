using System.Collections.Generic;
using System.Threading.Tasks;

namespace offer_manager.Interfaces
{
    public interface IAIProcessor
    {
        Task<(List<string?>, List<string>)> ProcessUnifiedSchemas(List<string> offers, Dictionary<string, string> systemPromptParams);
    }
}
