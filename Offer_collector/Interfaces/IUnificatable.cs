using Offer_collector.Models;

namespace Offer_collector.Interfaces
{
    public interface IUnificatable
    {
         UnifiedOfferSchemaClass UnifiedSchema(string rawHtml = "");
    }
}
