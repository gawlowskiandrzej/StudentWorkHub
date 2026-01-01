using System.Text.Json;

namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for fetching stored ranking weights.
    /// </summary>
    public sealed class GetWeightsRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for retrieving stored weights.
    /// </summary>
    public sealed class GetWeightsResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Stored weights as raw JSON. Shape/keys are described in the API documentation.
        /// </summary>
        public JsonElement Weights { get; init; }
    }
}
