using System.Text.Json;

namespace offer_manager.Models.Users
{
    public sealed class GetPreferencesRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class GetPreferencesResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public JsonElement Preferences { get; init; }
    }
}
