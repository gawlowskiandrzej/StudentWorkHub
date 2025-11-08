using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Interfaces;

namespace Offer_collector.Models.Tools
{
    public static class OfferMapper
    {
        public static T DeserializeJToken<T>(JToken? token) where T : new()
        {
            if (token == null || token.Type == JTokenType.Null)
                return new T();

            try
            {
                var result = token.ToObject<T>();
                return result != null ? result : new T();
            }
            catch
            {
                return new T();
            }
        }

        public static T DeserializeJson<T>(string? json) where T : new()
        {
            if (string.IsNullOrWhiteSpace(json))
                return new T();

            try
            {
                var result = JsonConvert.DeserializeObject<T>(json);
                return result != null ? result : new T();
            }
            catch
            {
                return new T();
            }
        }

        public static UnifiedOfferSchema ToUnifiedSchema<T>(IUnificatable? offerObj, string rawHtml = "")
            where T : new()
        {
            if (offerObj == null)
                return new UnifiedOfferSchema();

            try
            {
                var schema = offerObj.UnifiedSchema(rawHtml);
                return schema ?? new UnifiedOfferSchema();
            }
            catch
            {
                return new UnifiedOfferSchema();
            }
        }
    }
}
