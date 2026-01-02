using Offer_collector.Models;
using UnifiedOfferSchema;

namespace shared_models
{
    public class DynamicFilter
    {
        public List<Languages> Languages { get; set; } = new List<Languages>();
        public List<string> ExperienceLevels { get; set; } = new List<string>();
        public List<string> EducationNames { get; set; } = new List<string>();
    }
}
