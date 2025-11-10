

namespace Offer_collector.Models
{
    internal class AiProcessedOffer
    {
        public UnifiedOfferSchemaClass Offer { get; set; } = new UnifiedOfferSchemaClass();
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
