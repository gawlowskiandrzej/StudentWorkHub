# Usage of LLMParser #
## Add LLMParser to project ##
1. Right-click on `Dependencies` -> `add` -> `Project reference`.
2. `Browse` on the bottom panel of `Reference manager`.
3. Select `LLMParser.dll` and click `Add`.
4. Import library inside the code

> ℹ **Info**: LLMParser requires `UnifiedOfferSchema` to work.

```c#
using LLMParser;
using UnifiedOfferSchema;
```

> ⚠ **Warning**: Parser expects models in correct `Unified Offer schema` format. Parsing different format will result in errors.

## Settings objects ##
### API ###
> ℹ **Info**: `API` structure is used to pass API specific settings. You may create and use multiple instances for different APIs/Models to use in case of main API (first passed), failure. To use this feature you must use ` ParserHelpers.ParsingErrorAction.ChangeApiAndRetry` as a onApiFailure action in `ParseBatchAsync` function.

#### Usage for Gemini API ####
> ℹ **Info**: Library contains predefined elements for gemini api. They might serve as a reference to building custom elements for other APIs.

```c#
Settings.API apiSettings = new(
    "Gemini_API_Key",
    // Api key header - api key will be sent with this parameter.
    // geminiApiKeyHeader = "x-goog-api-key"
    PredefinedElements.geminiApiKeyHeader,

    // Api string - absolute url to api endpoint.
    // GeminiApiStringBuilder(modelName) - Returns api url with specified model name 
    // e.g. https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-lite:generateContent
    PredefinedElements.GeminiApiStringBuilder(modelName),

    // Request body parser - function that creates payload to be sent to API.
    // More details below.
    PredefinedElements.GeminiRequestBodyBuilder,

    // Result parser - extracts response content from API response.
    PredefinedElements.GeminiResultParser,

    // Model max request input tokens - maximum number that model can take as input
    // geminiModelMaxRequestInputTokens[modelName] - returns int value of tokens for specified gemini model e.g. 1048576
    PredefinedElements.geminiModelMaxRequestInputTokens[modelName],

    // Model max request output tokens - maximum number that model can return as output
    // geminiModelMaxRequestOutputTokens[modelName] - returns int value of tokens for specified gemini model e.g. 1048576
    PredefinedElements.geminiModelMaxRequestOutputTokens[modelName]
 );
```

#### Usage for custom API (i.e. GPT5-nano) ####
> ℹ **Info**: For custom API we must first create our own functions build based on predefined gemini functions. How to create them is shown below.

```c#
Settings.API apiSettings = new(
    "Bearer OpenAI_API_Key",
    "Authorization",
    "https://api.openai.com/v1/responses",
    GPTRequestBodyBuilder,
    GPTResultParser,
    400000,
    128000
 );
```

### Request ###
> ℹ **Info**: `Request` structure is used to pass request settings, common for all APIs.

> ℹ **Info**: Allowed resonse types have priorities assigned based on list order.
> *Smaller index = higher priority.*

#### Usage for both APIs ####
```c#
Settings.Request requestSettings = new(
    30, // How many concurrent request are sent to api.
    // List of llowed types in order e.g. ['application/json', 'text/plain']
    PredefinedElements.apiResonseTypes 
);
```

### Prompt ###
> ℹ **Info**: `Prompt` structure is used to pass prompts (user/system) with parameters, common for all APIs. Values **[{KEY}]** inside prompts are later replaced, by values from dictionaries. Some keys are required:
> - System prompt:
>   - UNIFIED-OFFER-SCHEMA
>   - UNIFIED-OFFER-SCHEMA-EXPLANATIONS
>   - RESULT-EXAMPLE
> - User prompt:
>   - USER-OFFER
>
> Those keys are filled internally and don't require passing them in dictionaries.

> ℹ **Info**: Predefined prompts and values for their keys are defined in `Prompts.cs` file.


#### Usage for both APIs ####
```c#
Settings.Prompt promptSettings = new(
    // Use default system prompt template, contains additional keys:
    // ['ROLE', 'RULES', 'RESTRICTIONS', 'TASK'] - you need to pass values for them
    Prompts.defaultSystemPromptTemplate,

    // Use default user prompt, doesn't contain any additional keys
    Prompts.descriptionInformationExtractorUserPrompt,

    // Dictionary with values for defaultSystemPromptTemplate 
    Prompts.descriptionInformationExtractorSystemPredefinedSet,

    // User prompt doesn't require any additional parameters 
    null
);
```

#### Composing prompt values dictionary ####
`Prompts.descriptionInformationExtractorSystemPredefinedSet` definition:
```c#
public static readonly Dictionary<string, string> descriptionInformationExtractorSystemPredefinedSet = new()
{
    { "ROLE", descriptionInformationExtractorRole },
    { "RULES", descriptionInformationExtractorRules },
    { "TASK", descriptionInformationExtractorTask },
    { "RESTRICTIONS", descriptionInformationExtractorRestrictions }
};
```
You may replace values or create whole custom dictionary for custom system prompt. \
Those values e.g. `descriptionInformationExtractorRole` are predefined prompt parts, you may also use them in your custom dictionary.

> ⚠ **Warning**: Dictionary must contain all keys defined in system/user prompt (except for internal keys).

## Parsing offers ##
> ℹ **Info**: Make sure to define all 3 objects before.

> ℹ **Info**: Please note that `ParseBatchAsync` uses `await` keyword and need to be run from `async` function. Also note that results are defined as `List<string?>` because ParseBatchAsync returns null when encountering an error.

```c#
// You may pass more than one Settings.API object
// Create parser object
Parser llmParser = new([apiSettings], requestSettings);

List<string> offers = 
[
    "offer 1",
    "offer 2",
    "...",
    "offer n"
];

// Run parser with offers and promptSettings
List<string?> results = await llmParser.ParseBatchAsync(offers, promptSettings);
```

## Batch error collection ##
Errors encountered during processing batch are stored internally and are accessible through `llmParser.GetLastBatchErrors()` function.

```c#
List<string?> results = [];
try
{
    results = await llmParser.ParseBatchAsync(offers, promptSettings);
}
catch (ParserException ex) // If whole batch failed it returns ParserException
{
    foreach (var e in llmParser.GetLastBatchErrors())
        Console.WriteLine(e.ToString());
}

// Check all exceptions that occured during processing 
List<string> batchExceptions = llmParser.GetLastBatchErrors();
if (batchException.Count() > 0)
    foreach (var e in batchExceptions)
        Console.WriteLine(e.ToString());

```

Exceptions are listed in format:
```text
Offer id:
id

Exception message:
message
```

## API failure actions ##
> ℹ **Info**: Actions `Retry` and `ChangeApiAndRetry` will return 1 combined error. 

Actions to take on failure are defined in `ParserHelpers.cs`:
```c#
public enum ParsingErrorAction
{
    Retry, // Retries 3 times with increasing delay (only first api on the list)
    ChangeApiAndRetry, // Tries every API on the list
    StoreException // Tries 1 time with first api on the list (default behaviour)
}
```
You may pass this action in `ParseBatchAsync`.
```c#
await llmParser.ParseBatchAsync(offers, promptSettings, ParserHelpers.ParsingErrorAction.ChangeApiAndRetry);
```

## Error handling ##
> ℹ **Info**: Most errors are caught and rethrown as **ParserException**

```c#
try 
{
    // Offer parsing
}
catch (ParserException ex)
{
    // Catch exceptions thrown by Parser
}
catch (Exception ex)
{
    // Catch unknown exceptions (doubled security)
}
```

## Creating custom builders ##
To create custom builder you may use default gemini builder as base

### Default builder ###
```c#
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
```

### Custom builder (gpt-5-nano) ###
> ℹ **Info**: GPT-5-nano takes less API parameters than gemini, but `tries` and `max_tries` are still required, even though they aren't used.

```c#
public static object GPTRequestBodyBuilder(string systemPrompt, string userPrompt, int maxOutputTokens = 4096, byte tries = 0, byte max_tries = 3)
{
    if (systemPrompt is null || userPrompt is null)
        throw new ParserException("systemPrompt and userPrompt parameters must not be empty.");

    maxOutputTokens = maxOutputTokens > 0 ? maxOutputTokens : throw new ParserException("maxOutputTokens must be greater or equal to 1.");

    return new
    {
        model = "gpt-5-nano",
        reasoning = new {effort = "low"},
        input = new object[]
            {
                new
                {
                    role = "developer",
                    content = systemPrompt
                },
                new
                {
                    role = "user",
                    content =  userPrompt 
                }
            },
             max_output_tokens = maxOutputTokens
    };
}
```

## Creating custom output parsers ##
To create custom parser you may use default gemini parser as base.

### Default parser ###
```c#
public static string GeminiResultParser(string unparsedResult)
{
    try
    {
        JsonDocument jdoc = JsonDocument.Parse(unparsedResult);
        JsonElement jroot = jdoc.RootElement;

        string rawText =  jroot
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString()!;

        try
        {
            using JsonDocument formattedDoc = JsonDocument.Parse(rawText);
            return JsonSerializer.Serialize(formattedDoc, new JsonSerializerOptions 
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true 
            });
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
```

### Custom parser (gpt-5-nano) ###
```c#
static string GPTResultParser(string unparsedResult)
{
    try
    {
        JsonDocument jdoc = JsonDocument.Parse(unparsedResult);
        JsonElement jroot = jdoc.RootElement;

        string rawText = jroot
                    .GetProperty("output")[1] 
                    .GetProperty("content")[0]
                    .GetProperty("text") 
                    .GetString()!;
        try
        {
            using JsonDocument formattedDoc = JsonDocument.Parse(rawText);
            return JsonSerializer.Serialize(formattedDoc, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            });
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
```