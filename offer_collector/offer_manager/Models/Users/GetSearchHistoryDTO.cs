using System.Text.Json;

namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for fetching the last N search snapshots.
    /// </summary>
    public sealed class GetSearchHistoryRequest
    {
        /// <summary>
        /// JSON Web Token identifying the authenticated user.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;

        /// <summary>
        /// Maximum number of history entries to return (should be greater than 0).
        /// </summary>
        public int Limit { get; init; }
    }

    /// <summary>
    /// Response payload for retrieving search history.
    /// </summary>
    public sealed class GetSearchHistoryResponse
    {
        /// <summary>
        /// Error message (empty string indicates success).
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// Search history as raw JSON (array). Shape/keys are described in the API documentation.
        /// </summary>
        public JsonElement SearchHistory { get; init; }
    }
}
