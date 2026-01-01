using System.Text.Json;

namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for fetching saved preferences.
    /// </summary>
    public sealed class GetPreferencesRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for retrieving saved preferences as JSON.
    /// </summary>
    public sealed class GetPreferencesResponse
    {
        /// <summary>
        /// Error message (empty string indicates success; may be non-empty when preferences are incomplete or on error).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Preferences as raw JSON object. Shape/keys are described in the API documentation.
        /// </summary>
        public JsonElement Preferences { get; init; }
    }
}
