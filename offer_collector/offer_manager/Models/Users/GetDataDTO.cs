using System.Text.Json;

namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for fetching user profile data.
    /// </summary>
    public sealed class GetDataRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for retrieving user profile data.
    /// </summary>
    public sealed class GetDataResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// User data as raw JSON object. Shape/keys are described in the API documentation.
        /// </summary>
        public JsonElement UserData { get; init; }
    }
}
