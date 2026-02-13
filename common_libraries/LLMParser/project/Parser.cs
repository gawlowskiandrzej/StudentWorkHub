using System.Collections.Concurrent;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace LLMParser
{
    /// <summary>
    /// Central coordinator that holds API clients and orchestrates batch parsing into UOS.
    /// Supports multiple API backends for redundancy and failover.
    /// </summary>
    public class Parser : IDisposable
    {
        /// <summary>HTTP clients created per configured API backend.</summary>
        private readonly List<HttpClient> _httpClients = [];
        /// <summary>Immutable API configuration entries aligned with the HTTP clients.</summary>
        private readonly List<Settings.API> _apiSettings = [];
        /// <summary>Global request settings used to build requests and prompts.</summary>
        private readonly Settings.Request _requestSettings;
        /// <summary>Transient buffer of error messages collected during the last batch parse.</summary>
        private ConcurrentQueue<string> _batchExceptions = [];

        /// <summary>
        /// Initializes the parser with API backends and shared request settings.
        /// </summary>
        /// <param name="apiSettings">Collection of API backends to use for parsing.</param>
        /// <param name="requestSettings">Global request configuration applied to all calls.</param>
        /// <returns>Parser instance.</returns>
        /// <exception cref="ParserException">Thrown when configuration is invalid or initialization fails.</exception>
        public Parser(List<Settings.API> apiSettings, Settings.Request requestSettings) 
        {
            try
            {
                foreach (Settings.API api in apiSettings)
                {
                    HttpClient client = new()
                    {
                        Timeout = TimeSpan.FromSeconds(15)
                    };
                    api.AddApiKey(ref client);
                    requestSettings.AddHttpClientResponseTypeHeaders(ref client);
                    this._httpClients.Add(client);
                    this._apiSettings.Add(api);
                }

                this._requestSettings = requestSettings;
            }
            catch (Exception ex)
            {
                throw new ParserException("Unknown error occured", ex);
            }
        }

        /// <summary>
        /// Parses a batch of offers in parallel using configured APIs and merges model output with the source offer to produce a full UOS offer.
        /// </summary>
        /// <param name="offers">Source offers to parse (order preserved).</param>
        /// <param name="promptSettings">Prompt/template settings used to build requests.</param>
        /// <param name="onApiFailure">Action to take on API failure (retry, switch API, or store exception).</param>
        /// <param name="cancellationToken">Token to cancel the batch operation.</param>
        /// <returns>A list of full UOS offers as strings. Items may be <c>null</c> when parsing fails for a given input (depending on the error action).</returns>
        /// <exception cref="ParserException">Thrown when inputs are invalid, token limits are exceeded, or the whole batch fails.</exception>
        /// <remarks>
        /// Returns in the same order as input. The internal error list is overwritten on each call; inspect it via <see cref="GetLastBatchErrors"/>.
        /// Actions on API failure are:
        /// - Retry - makes 3 retries with cooldown between from 500ms to 1500ms
        /// - ChangeApiAndRetry - goest through the list of provided APIs and tries to use them
        /// - StoreException - fails after first time, normal behaviour.
        /// </remarks>

        public async Task<List<string?>> ParseBatchAsync(List<string> offers, Settings.Prompt promptSettings, ParserHelpers.ParsingErrorAction onApiFailure = ParserHelpers.ParsingErrorAction.StoreException, CancellationToken cancellationToken = new())
        {
            if (offers.Count <= 0)
            {
                throw new ParserException("Offers list is empty");
            }
            
            string[] parsedOffers = new string[offers.Count];

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = this._requestSettings.batchSize,
                CancellationToken = cancellationToken
            };

            this._batchExceptions = new();
            bool batchSuccess = false;
            await Parallel.ForAsync(0, offers.Count, parallelOptions, async (i, cancellationToken) =>
            {
                string parsedResponse = "";
                try
                {
                    FrozenDictionary<string, string> parsedOffer = Prompts.BuildPrompts(offers[i], promptSettings);
                    int estimatedInputTokens = EstimateTokens(string.Join(Environment.NewLine, parsedOffer.Values));
                    
                    string responseBody = "";
                    bool success = false;
                    List<Exception> apiErrors = [];
                    switch (onApiFailure)
                    {
                        case ParserHelpers.ParsingErrorAction.ChangeApiAndRetry:
                            apiErrors = [];
                            for (int apiCtr = 0; apiCtr < this._httpClients.Count; apiCtr++)
                            {
                                if (estimatedInputTokens > this._apiSettings[apiCtr].requestInputTokenLimit)
                                    throw new ParserException($"Estimated input lenght ({estimatedInputTokens}) is greater than allowed by API ({this._apiSettings[apiCtr].requestInputTokenLimit})");

                                try
                                {
                                    responseBody = await ParserHelpers.ApiCall(
                                        parsedOffer,
                                        this._httpClients[apiCtr],
                                        this._apiSettings[apiCtr].apiUrl,
                                        this._requestSettings._allowedResponseTypes,
                                        this._apiSettings[apiCtr].requestOutputTokenLimit,
                                        this._apiSettings[apiCtr].requestBodyParser,
                                        cancellationToken
                                     );

                                    success = true;
                                    parsedResponse = this._apiSettings[apiCtr].outputOfferParser(responseBody);
                                    break;
                                }
                                catch (HttpRequestException ex) 
                                {
                                    apiErrors.Add(ex);
                                }
                                catch (Exception ex)
                                {
                                    apiErrors.Add(ex);
                                    throw new ParserException("Unknown exception while sending API request.", ex);
                                }
                            }
                            if (!success)
                                throw new ParserException($"All APIs failed. This might indicate network problems.\n\nCollected error messages:\n{string.Join(Environment.NewLine, apiErrors.Select(e => e.Message))}");

                            parsedOffers[i] = UOSUtils.BuildFromString(offers[i]).MergeWith(parsedResponse).AsString(true);
                            break;

                        case ParserHelpers.ParsingErrorAction.Retry:
                            apiErrors = [];
                            if (estimatedInputTokens > this._apiSettings[0].requestInputTokenLimit)
                                throw new ParserException($"Estimated input lenght ({estimatedInputTokens}) is greater than allowed by API ({this._apiSettings[0].requestInputTokenLimit})");

                            byte offerParserErrorsCount = 0;
                            for (int retries = 0; retries < 3; retries++)
                            {
                                try
                                {
                                    responseBody = await ParserHelpers.ApiCall(
                                        parsedOffer,
                                        this._httpClients[0],
                                        this._apiSettings[0].apiUrl,
                                        this._requestSettings._allowedResponseTypes,
                                        this._apiSettings[0].requestOutputTokenLimit,
                                        this._apiSettings[0].requestBodyParser,
                                        cancellationToken,
                                        offerParserErrorsCount
                                     );
                                    parsedResponse = this._apiSettings[0].outputOfferParser(responseBody);

                                    try
                                    {
                                        parsedOffers[i] = UOSUtils.BuildFromString(offers[i]).MergeWith(parsedResponse).AsString(true);
                                    }
                                    catch (Exception ex) { apiErrors.Add(ex); }
                                    if (!string.IsNullOrWhiteSpace(parsedOffers[i]))
                                    {
                                        success = true;
                                        break;
                                    }
                                    else
                                        offerParserErrorsCount++;
                                }
                                catch (HttpRequestException ex)
                                {
                                   apiErrors.Add(ex);
                                }
                                catch (Exception ex) when (ex is not UOSException)
                                {
                                    apiErrors.Add(ex);
                                    throw new ParserException("Unknown exception while sending API request.", ex);
                                }
                                await Task.Delay(retries * 500, cancellationToken);
                            }
                            if (!success)
                                throw new ParserException($"Failed 3 attempts. This might indicate that API failure, or API returned incorrect offer format.\n\nCollected error messages:\n{string.Join(Environment.NewLine, apiErrors.Select(e => e.Message))}");

                            break;

                        case ParserHelpers.ParsingErrorAction.StoreException:
                            if (estimatedInputTokens > this._apiSettings[0].requestInputTokenLimit)
                                throw new ParserException($"Estimated input lenght ({estimatedInputTokens}) is greater than allowed by API ({this._apiSettings[0].requestInputTokenLimit})");
                            try
                            {
                                responseBody = await ParserHelpers.ApiCall(
                                    parsedOffer,
                                    this._httpClients[0],
                                    this._apiSettings[0].apiUrl,
                                    this._requestSettings._allowedResponseTypes,
                                    this._apiSettings[0].requestOutputTokenLimit,
                                    this._apiSettings[0].requestBodyParser,
                                    cancellationToken
                                );
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            parsedResponse = this._apiSettings[0].outputOfferParser(responseBody);
                            parsedOffers[i] = UOSUtils.BuildFromString(offers[i]).MergeWith(parsedResponse).AsString(true);
                            break;

                        default:
                            break;
                    }

                   batchSuccess = true;
                }
                catch (Exception ex)
                {
                    this._batchExceptions.Enqueue($"Offer id:\n{i}\n\nException message:\n{ex.Message}\n\nParsed response:\n{parsedResponse}\n\nBase offer:\n{offers[i]}");
                }
            });

            if (!batchSuccess)
                throw new ParserException("Whole batch failed. Check GetLastBatchErrors() for details.");

            return [.. parsedOffers];
        }

        /// <summary>
        /// Roughly estimates token count for a given prompt to compare  model inptut tokens limit with prompt size.
        /// Tokens are estimated as 1 token per 4 characters
        /// </summary>
        /// <param name="prompt">Full prompt text used as LLM input.</param>
        /// <returns>Estimated number of tokens as an integer.</returns>
        private static int EstimateTokens(string prompt)
        {
            return (int)Math.Ceiling(prompt.Length / 4.0);
        }

        /// <summary>
        /// Returns the set of error descriptions collected during the most recent batch processing.
        /// </summary>
        /// <returns>
        /// An immutable set containing distinct error descriptions from the last processed batch.
        /// </returns>
        public FrozenSet<string> GetLastBatchErrors()
        {
            return this._batchExceptions.ToFrozenSet();
        }

        /// <summary>
        /// Disposes underlying resources and clears transient error buffers.
        /// </summary>
        public void Dispose()
        {
            foreach (HttpClient client in this._httpClients)
                client.Dispose();

            _batchExceptions.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
