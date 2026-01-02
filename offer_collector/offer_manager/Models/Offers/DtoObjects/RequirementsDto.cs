using System.Text.Json.Serialization;
using UnifiedOfferSchema;

namespace offer_manager.Models.Offers.dtoObjects
{
    public class RequirementsDto
    {
        [JsonPropertyName("skills")]
        [JsonRequired]
        public List<Skills>? Skills { get; set; }

        [JsonPropertyName("education")]
        [JsonRequired]
        public List<string>? Education { get; set; }

        [JsonPropertyName("languages")]
        [JsonRequired]
        public List<Languages>? Languages { get; set; }
    }
}