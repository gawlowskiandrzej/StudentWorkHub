using Newtonsoft.Json;

namespace Offer_collector.Models.Tools
{
    public class DecimalDefaultConverter : JsonConverter<decimal>
    {
        public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return 0m; // domyślna wartość zamiast null
            return Convert.ToDecimal(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
