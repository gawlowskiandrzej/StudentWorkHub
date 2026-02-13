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
        /// <param name="modelName">Target Gemini model identifier (e.g., <c>gemini-2.5-pro</c>).</param>
        /// <returns>Absolute URL of the model endpoint.</returns>
        public static string GeminiApiStringBuilder(string modelName)
        {
            return $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent";
        }

        /// <summary>
        /// Gemini model name list.
        /// </summary>
        public static readonly List<string> geminiApiModelNames = [
            "gemini-2.0-flash-lite",
            "gemini-2.0-flash",
            "gemini-2.5-flash-lite",
            "gemini-2.5-flash",
            "gemini-2.5-pro"
        ];

        /// <summary>
        /// Maximum number of input tokens that can be sent per request for each supported Gemini model.
        /// </summary>
        public static readonly FrozenDictionary<string, int> geminiModelMaxRequestInputTokens = new Dictionary<string, int>()
        {
            {"gemini-2.0-flash-lite", 1048576},
            {"gemini-2.0-flash", 1048576},
            {"gemini-2.5-flash-lite", 1048576},
            {"gemini-2.5-flash", 1048576},
            {"gemini-2.5-pro", 1048576},
        }.ToFrozenDictionary();

        /// <summary>
        /// Maximum number of output tokens that can be requested per call for each supported Gemini model.
        /// </summary>        
        public static readonly FrozenDictionary<string, int> geminiModelMaxRequestOutputTokens = new Dictionary<string, int>()
        {
            {"gemini-2.0-flash-lite", 8192},
            {"gemini-2.0-flash", 8192},
            {"gemini-2.5-flash-lite", 8192},
            {"gemini-2.5-flash", 65536},
            {"gemini-2.5-pro", 65536},
        }.ToFrozenDictionary();

        /// <summary>
        /// Creates a minimal request payload for the Gemini <c>generateContent</c> API with system and user prompts.
        /// </summary>
        /// <param name="systemPrompt">High-level instruction describing parser behavior and constraints.</param>
        /// <param name="userPrompt">Concrete task or input (e.g., offer text) for the model to process.</param>
        /// <param name="maxOutputTokens">Upper bound for model completion tokens</param>
        /// <returns>Anonymous object that can be serialized to JSON and sent as the request body.</returns>
        /// <exception cref="ParserException">Thrown when required parameters are null or empty.</exception>
        /// <remarks>
        /// This function might be used as a reference body parser. internal body might change but return type and parameters must remain unchanged.
        /// Default sampling settings: temperature 0.0, topP 1.0, topK 0.
        /// </remarks>
        public static object GeminiRequestBodyBuilder(string systemPrompt, string userPrompt, int maxOutputTokens = 4096, byte tries = 0, byte max_tries = 3)
        {
            if (systemPrompt is null || userPrompt is null)
                throw new ParserException("systemPrompt and userPrompt parameters must not be empty.");

            maxOutputTokens = maxOutputTokens > 0 ? maxOutputTokens : throw new ParserException("maxOutputTokens must be greater or equal to 1.");
            
            
            double temperature = Math.Clamp(Math.Round(0.7 * tries / (max_tries - 1.0), 2) ,0.0, 1.0);
            double topP = Math.Clamp(Math.Round(0.85 + (0.13 * tries / (max_tries - 1.0)), 2), 0.0, 1.0);
            int topK = Math.Clamp(30 + (70 * tries / (max_tries - 1)), 0, 100);

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
                        thinkingBudget = 0 // Explicitly disable "thinking" budget for faster/cheaper decoding.
                    },
                    temperature,
                    topP,
                    topK,
                    maxOutputTokens,
                    candidateCount = 1,
                    responseMimeType = "application/json" // Ask the model to return JSON text.
                }
            };
        }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        // <summary>
        /// Extracts plain text from a Gemini <c>generateContent</c> JSON response.
        /// </summary>
        /// <param name="unparsedResult">Raw JSON string returned by the API.</param>
        /// <returns>First candidate response text.</returns>
        /// <exception cref="ParserException">Thrown when the response cannot be parsed or lacks expected fields.</exception>
        /// <remarks>
        /// This function might be used as a reference response parser. internal body might change but return type and parameters must remain unchanged.
        /// </remarks>
        public static string GeminiResultParser(string unparsedResult)
        {
            try
            {
                JsonDocument jdoc = JsonDocument.Parse(unparsedResult);
                JsonElement jroot = jdoc.RootElement;

                string rawText =  jroot.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString()!;

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
            catch (Exception ex)
            {
                throw new ParserException($"Failed to parse unparsedResult. Responses structure might have changed. Try using external parser.\n\nOffer structure:\n{unparsedResult}", ex);
            }
            
        }
    }
}
