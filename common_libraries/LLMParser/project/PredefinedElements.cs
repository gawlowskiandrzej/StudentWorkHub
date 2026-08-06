using System.Collections.Frozen;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace LLMParser
{
    // <summary>
    /// Central set of predefined constants and helpers for building requests and parsing results used by the UOS parser.
    /// This is primarily intended for gemini API and models.
    /// </summary>
    public class PredefinedElements
    {
        /// <summary>
        /// Allowed response content types accepted by HTTP clients when calling LLM APIs.
        /// </summary>
        public static readonly List<string> apiResponseTypes =
        [
            "application/json",
            "text/plain",
            "application/xml"
        ];

        /// <summary>
        /// HTTP header name used to pass the Gemini API key.
        /// </summary>
        public static readonly string geminiApiKeyHeader = "x-goog-api-key";

        /// <summary>
        /// Builds a full Gemini <c>generateContent</c> endpoint URL for the provided model name.
        /// </summary>
        /// <param name="modelName">Target Gemini model identifier (e.g., <c>gemini-3.5-flash-lite</c>).</param>
        /// <returns>Absolute URL of the model endpoint.</returns>
        public static string GeminiApiStringBuilder(string modelName)
        {
            return $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent";
        }

        /// <summary>
        /// Gemini model name list, ordered from the cheapest/fastest to the most capable.
        /// Only stable models without an announced shutdown date are listed here.
        /// </summary>
        public static readonly List<string> geminiApiModelNames = [
            "gemini-3.5-flash-lite",
            "gemini-3.5-flash",
            "gemini-3.6-flash",
            "gemini-2.5-flash-lite",
            "gemini-2.5-flash",
            "gemini-2.5-pro"
        ];

        /// <summary>
        /// Maximum number of input tokens that can be sent per request for each supported Gemini model.
        /// </summary>
        public static readonly FrozenDictionary<string, int> geminiModelMaxRequestInputTokens = new Dictionary<string, int>()
        {
            {"gemini-3.5-flash-lite", 1048576},
            {"gemini-3.5-flash", 1048576},
            {"gemini-3.6-flash", 1048576},
            {"gemini-2.5-flash-lite", 1048576},
            {"gemini-2.5-flash", 1048576},
            {"gemini-2.5-pro", 1048576},
        }.ToFrozenDictionary();

        /// <summary>
        /// Maximum number of output tokens that can be requested per call for each supported Gemini model.
        /// </summary>
        public static readonly FrozenDictionary<string, int> geminiModelMaxRequestOutputTokens = new Dictionary<string, int>()
        {
            {"gemini-3.5-flash-lite", 65536},
            {"gemini-3.5-flash", 65536},
            {"gemini-3.6-flash", 65536},
            {"gemini-2.5-flash-lite", 65536},
            {"gemini-2.5-flash", 65536},
            {"gemini-2.5-pro", 65536},
        }.ToFrozenDictionary();

        /// <summary>
        /// Creates a request payload for the Gemini <c>generateContent</c> API, constrained by a response schema.
        /// </summary>
        /// <param name="systemPrompt">High-level instruction describing parser behavior and constraints.</param>
        /// <param name="userPrompt">Concrete task or input (e.g., offer text) for the model to process.</param>
        /// <param name="responseSchema">Schema the model output must conform to, built by <see cref="UosResponseSchema.Build"/>.</param>
        /// <param name="maxOutputTokens">Upper bound for model completion tokens</param>
        /// <returns>Anonymous object that can be serialized to JSON and sent as the request body.</returns>
        /// <exception cref="ParserException">Thrown when required parameters are null or empty.</exception>
        /// <remarks>
        /// This function might be used as a reference body parser. internal body might change but return type and parameters must remain unchanged.
        /// Sampling parameters (temperature, topP, topK) are omitted: Gemini 3.x is tuned for its defaults and rejects them in newer model generations.
        /// </remarks>
        public static object GeminiRequestBodyBuilder(string systemPrompt, string userPrompt, object responseSchema, int maxOutputTokens = 4096)
        {
            if (systemPrompt is null || userPrompt is null)
                throw new ParserException("systemPrompt and userPrompt parameters must not be empty.");

            if (responseSchema is null)
                throw new ParserException("responseSchema must not be null.");

            maxOutputTokens = maxOutputTokens > 0 ? maxOutputTokens : throw new ParserException("maxOutputTokens must be greater or equal to 1.");

            return new
            {
                system_instruction = new
                {
                    parts = new object[]
                    {
                new { text = systemPrompt }
                    }
                },
                contents = new[]
                {
            new
            {
                parts = new object[]
                {
                    new { text = userPrompt }
                }
            }
        },
                generationConfig = new
                {
                    thinkingConfig = new
                    {
                        thinkingLevel = "minimal" // Lowest reasoning effort: this is high-volume extraction, not a reasoning task.
                    },
                    maxOutputTokens,
                    responseMimeType = "application/json",
                    responseSchema
                }
            };
        }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        /// <summary>
        /// Extracts plain text from a Gemini <c>generateContent</c> JSON response.
        /// </summary>
        /// <param name="unparsedResult">Raw JSON string returned by the API.</param>
        /// <returns>First candidate response text.</returns>
        /// <exception cref="ParserException">Thrown when generation did not complete normally, or when the response cannot be parsed or lacks expected fields.</exception>
        /// <remarks>
        /// This function might be used as a reference response parser. internal body might change but return type and parameters must remain unchanged.
        /// </remarks>
        public static string GeminiResultParser(string unparsedResult)
        {
            try
            {
                JsonDocument jdoc = JsonDocument.Parse(unparsedResult);
                JsonElement jroot = jdoc.RootElement;

                JsonElement candidate = jroot.GetProperty("candidates")[0];

                // A truncated or blocked generation still returns well-formed envelope JSON, but the payload inside is unusable.
                if (candidate.TryGetProperty("finishReason", out JsonElement finishReason)
                    && finishReason.GetString() is string finishReasonValue
                    && !string.Equals(finishReasonValue, "STOP", StringComparison.Ordinal))
                {
                    throw new ParserException($"Generation did not complete normally. finishReason: {finishReasonValue}. For MAX_TOKENS raise the output token limit, otherwise inspect the offer content.");
                }

                string rawText = candidate.GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()!;

                try
                {
                    using JsonDocument formattedDoc = JsonDocument.Parse(rawText);
                    return JsonSerializer.Serialize(formattedDoc, _serializerOptions);
                }
                catch
                {
                    return rawText;
                }
            }
            catch (Exception ex) when (ex is not ParserException)
            {
                throw new ParserException($"Failed to parse unparsedResult. Responses structure might have changed. Try using external parser.\n\nOffer structure:\n{unparsedResult}", ex);
            }
        }
    }
}
