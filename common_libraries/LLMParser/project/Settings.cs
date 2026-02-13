using System.Collections.Frozen;
using System.Net.Http.Headers;

namespace LLMParser
{
    /// <summary>
    /// Aggregates configuration types used by the parser: API provider config, shared HTTP request config, and LLM prompt config.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Immutable configuration for a single API provider (credentials, endpoint, limits, and request body builder).
        /// </summary>
        /// <param name="apiKey">Secret API key value.</param>
        /// <param name="apiKeyHeader">Name of the HTTP header used to transmit the API key.</param>
        /// <param name="apiUrl">Absolute HTTPS URL of the API endpoint.</param>
        /// <param name="requestBodyParser">Delegate building a request body from system/user prompts and max output tokens.</param>
        /// <param name="requestInputTokenLimit">Maximum allowed input tokens for a single request (0 means unlimited/unknown).</param>
        /// <param name="requestOutputTokenLimit">Maximum allowed output tokens for a single request (0 means unlimited/unknown).</param>
        /// <exception cref="ParserException">Thrown when <paramref name="apiUrl"/> is not a valid HTTPS URL or token limits are negative.</exception>
        public readonly struct API(string apiKey, string apiKeyHeader, string apiUrl, Func<string, string, int, byte, byte, object> requestBodyParser, Func<string, string> outputOfferParser, int requestInputTokenLimit = 0, int requestOutputTokenLimit = 0)
        {
            /// <summary>
            /// API key value added to outgoing requests.
            /// </summary>
            private readonly string _apiKey = apiKey;

            /// <summary>
            /// HTTP header name carrying the API key.
            /// </summary>
            private readonly string _apiKeyHeader = apiKeyHeader;

            /// <summary>
            /// HTTPS endpoint used for API calls.
            /// </summary>
            internal readonly string apiUrl = !string.IsNullOrWhiteSpace(apiUrl)
                    && Uri.TryCreate(apiUrl, UriKind.Absolute, out Uri uriResult)
                    && uriResult.Scheme == Uri.UriSchemeHttps
                    ? apiUrl : throw new ParserException("apiUrl must be a valid HTTPS url.");

            /// <summary>
            /// Maximum allowed number of input tokens for a single request.
            /// </summary>
            internal readonly int requestInputTokenLimit = requestInputTokenLimit >= 0 ? requestInputTokenLimit : throw new ParserException("requestInputTokenLimit must be greater or equal than 0");

            /// <summary>
            /// Maximum allowed number of output tokens for a single request.
            /// </summary>
            internal readonly int requestOutputTokenLimit = requestOutputTokenLimit >= 0 ? requestOutputTokenLimit : throw new ParserException("requestOutputTokenLimit must be greater or equal than 0");

            /// <summary>
            /// Factory delegate creating the request payload for the target API.
            /// </summary>
            internal readonly Func<string, string, int, byte, byte, object> requestBodyParser = requestBodyParser;

            /// <summary>
            /// Delegate that extracts a normalized offer JSON from raw API response text.
            /// </summary>
            internal readonly Func<string, string> outputOfferParser = outputOfferParser;

            /// <summary>
            /// Adds the API key header to the provided <see cref="HttpClient"/>.
            /// </summary>
            /// <param name="httpClient">HTTP client to mutate.</param>
            internal void AddApiKey(ref HttpClient httpClient)
            {
                httpClient.DefaultRequestHeaders.Add(this._apiKeyHeader, this._apiKey);
            }
        }

        /// <summary>
        /// Immutable configuration of batch execution and accepted HTTP response media types.
        /// </summary>
        public readonly struct Request
        {
            /// <summary>
            /// Maximum degree of parallelism for batch parsing.
            /// </summary>
            internal readonly int batchSize;

            /// <summary>
            /// Allowed response content types with their quality factors (q-values).
            /// </summary>
            internal readonly Dictionary<string, double> _allowedResponseTypes = [];

            /// <summary>
            /// Creates a new request settings instance with parallelism and accepted response types.
            /// </summary>
            /// <param name="batchSize">Number of concurrent operations (must be &gt;= 1).</param>
            /// <param name="allowedResponseTypes">List of distinct media types to accept, ordered by preference.</param>
            /// <exception cref="ParserException">Thrown when <paramref name="batchSize"/> is invalid or no valid response types remain.</exception>
            public Request(int batchSize, List<string> allowedResponseTypes)
            {
                this.batchSize = batchSize > 0 ? batchSize : throw new ParserException("batchSize must be >= 1");
                int validCtr = 0;
                for (int i = 0; i < allowedResponseTypes.Count; i++)
                {
                    allowedResponseTypes[i] = allowedResponseTypes[i].Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(allowedResponseTypes[i]) || this._allowedResponseTypes.ContainsKey(allowedResponseTypes[i]))
                        continue;

                    validCtr++;
                    this._allowedResponseTypes.Add(allowedResponseTypes[i], Math.Round(Math.Clamp(1.0 - (validCtr * (1.0 / allowedResponseTypes.Count)), 0.001, 1.0), 3));
                }

                if (_allowedResponseTypes.Count <= 0)
                    throw new ParserException("allowedResponseTypes must contain at least one valid element.");
            }

            /// <summary>
            /// Sets the <c>Accept</c> headers on the provided <see cref="HttpClient"/> based on allowed media types.
            /// </summary>
            /// <param name="httpClient">HTTP client to configure.</param>
            internal void AddHttpClientResponseTypeHeaders(ref HttpClient httpClient)
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                foreach (var responseType in this._allowedResponseTypes)
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(responseType.Key) { Quality = responseType.Value });
            }
        }

        /// <summary>
        /// Immutable prompt template with parameter maps and an output parser for offers.
        /// </summary>
        public readonly struct Prompt
        {
            /// <summary>
            /// System prompt template (must contain parameter placeholders).
            /// </summary>
            internal readonly string systemPrompt;

            /// <summary>
            /// User prompt template (must contain parameter placeholders).
            /// </summary>
            internal readonly string userPrompt;

            /// <summary>
            /// Substitutions for system prompt placeholders.
            /// </summary>
            internal readonly FrozenDictionary<string, string> systemPromptParameters;

            /// <summary>
            /// Substitutions for user prompt placeholders.
            /// </summary>
            internal readonly FrozenDictionary<string, string> userPromptParameters;

            /// <summary>
            /// Creates a new prompt set with templates, parameter dictionaries, and an output parser.
            /// </summary>
            /// <param name="systemPrompt">System prompt template.</param>
            /// <param name="userPrompt">User prompt template.</param>
            /// <param name="outputOfferParser">Delegate parsing raw response into the target offer JSON.</param>
            /// <param name="systemPromptParameters">Optional parameters used in the system prompt.</param>
            /// <param name="userPromptParameters">Optional parameters used in the user prompt.</param>
            public Prompt(string systemPrompt, string userPrompt, Dictionary<string, string>? systemPromptParameters, Dictionary<string, string>? userPromptParameters)
            {
                this.systemPrompt = systemPrompt;
                this.userPrompt = userPrompt;
                Dictionary<string, string> parameters = [];
                if (systemPromptParameters != null)
                {
                    foreach (var parameter in systemPromptParameters)
                    {
                        if (string.IsNullOrWhiteSpace(parameter.Key) || string.IsNullOrWhiteSpace(parameter.Value))
                            continue;

                        parameters.Add(parameter.Key, parameter.Value);
                    }
                }
                this.systemPromptParameters = parameters.ToFrozenDictionary();

                parameters = [];
                if (userPromptParameters != null)
                {
                    foreach (var parameter in userPromptParameters)
                    {
                        if (string.IsNullOrWhiteSpace(parameter.Key) || string.IsNullOrWhiteSpace(parameter.Value))
                            continue;

                        parameters.Add(parameter.Key, parameter.Value);
                    }
                }
                this.userPromptParameters = parameters.ToFrozenDictionary();
            }
        }
    }
}
