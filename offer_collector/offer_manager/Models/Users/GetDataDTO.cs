using System.Text.Json;

namespace offer_manager.Models.Users
{
    public sealed class GetDataRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class GetDataResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public JsonElement UserData { get; init; }
    }
}
