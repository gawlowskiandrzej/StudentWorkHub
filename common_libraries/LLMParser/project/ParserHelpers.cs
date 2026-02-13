using System.Collections.Frozen;
using System.Text;
using System.Text.Json;

namespace LLMParser
{
    /// <summary>
    /// Helper utilities for parser operations, including API calls and error handling.
    /// </summary>
    public class ParserHelpers
    {
        /// <summary>
        /// Actions that controls how parsing errors should be handled during API requests.
        /// </summary>
        public enum ParsingErrorAction
        {
            Retry,
            ChangeApiAndRetry,
            StoreException
        }

        /// <summary>
        /// Sends a request to an LLM provider and returns the raw response body.
        /// </summary>
        /// <param name="parsedOffer">UOS offer data used to construct the provider-specific request body.</param>
        /// <param name="httpClient">Configured HTTP client used to perform the HTTP POST.</param>
        /// <param name="apiUrl">Target API endpoint URL.</param>
        /// <param name="allowedResponseTypes">Whitelist of acceptable response content types (MIME types).</param>
        /// <param name="maxOutputTokens">Maximum number of output tokens requested from the provider.</param>
        /// <param name="requestBodyParser">Delegate that builds the request payload for the selected provider.</param>
        /// <param name="cancellationToken">Token that propagates cancellation of the HTTP request.</param>
        /// <returns>Raw response content returned by the API.</returns>
        /// <exception cref="ParserException">Thrown when the response Content-Type is not allowed.</exception>
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails or a non-success status code is returned.</exception>
        /// <remarks>Validates the response media type and ensures a successful status code before returning content.</remarks>
        internal static async Task<string> ApiCall(FrozenDictionary<string, string> parsedOffer, HttpClient httpClient, string apiUrl, Dictionary<string, double> allowedResponseTypes, int maxOutputTokens, Func<string, string, int, byte, byte, object> requestBodyParser, CancellationToken cancellationToken, byte tries = 0)
        {
            object requestBody = requestBodyParser(parsedOffer["system"], parsedOffer["user"], maxOutputTokens, tries, 3);

            string apiPayload = JsonSerializer.Serialize(requestBody);
            using StringContent content = new(apiPayload, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content, cancellationToken);

            string responseHeader = response.Content.Headers.ContentType?.MediaType ?? "undefined/undefined";
            if (!allowedResponseTypes.ContainsKey(responseHeader))
                throw new ParserException($"{responseHeader} is not an allowed response type");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
