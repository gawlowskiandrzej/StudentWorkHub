using System.Text.Json;

namespace offer_manager.Models.Users
{
    public sealed class GetWeightsRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class GetWeightsResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public JsonElement Weights { get; init; }
    }
}
