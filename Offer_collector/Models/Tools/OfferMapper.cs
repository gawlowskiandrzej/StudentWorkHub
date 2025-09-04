using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Interfaces;

namespace Offer_collector.Models.Tools
{
    public static class OfferMapper
    {
        public static T DeserializeJToken<T>(JToken? token) where T : new()
        {
            return token != null ? token.ToObject<T>() : new T();
        }
        public static T DeserializeJson<T>(string json) where T : new() => JsonConvert.DeserializeObject<T>(json);
        public static UnifiedOfferSchema ToUnifiedSchema<T>(IUnificatable offerObj) where T : new()
        {
            if (offerObj == null) return new UnifiedOfferSchema();

            return offerObj.UnifiedSchema();
        }
    }
}
