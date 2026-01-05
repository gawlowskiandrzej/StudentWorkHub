using System.Text.Json;

namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for fetching the most recent N search history entries
    /// (globally, across all users).
    /// </summary>
    public sealed class GetLastSearchesRequest
    {
        /// <summary>
        /// Maximum number of entries to return. Values less than or equal to zero
        /// result in an empty response array.
        /// </summary>
        public int Limit { get; init; }
    }

    /// <summary>
    /// Response payload containing the most recent search history entries.
    /// </summary>
    public sealed class GetLastSearchesResponse
    {
        /// <summary>
        /// Error message. An empty string indicates success.
        /// </summary>
        public string ErrorMessage { get; init; } = string.Empty;

        /// <summary>
        /// The most recent searches as raw JSON (array). The structure is defined by the database function
        /// and documented in the API specification.
        /// </summary>
        public JsonElement LastSearches { get; init; }
    }
}
