using System.Text.Json;

namespace offer_manager.Models.Users
{
    public sealed class GetSearchHistoryRequest
    {
        public string Jwt { get; init; } = string.Empty;
        public int Limit { get; init; }
    }

    public sealed class GetSearchHistoryResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public JsonElement SearchHistory { get; init; }
    }
}
