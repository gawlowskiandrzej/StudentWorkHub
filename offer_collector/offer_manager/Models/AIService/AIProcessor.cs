using LLMParser;
using offer_manager;
namespace Offer_collector.Models.AI
{
    public class AIProcessor
    {
        static string modelName = PredefinedElements.geminiApiModelNames[1];
        Settings.API apiSettings = new(
            "tu klucz api odczytany z env",
            PredefinedElements.geminiApiKeyHeader,
            PredefinedElements.GeminiApiStringBuilder(modelName),
            PredefinedElements.GeminiRequestBodyBuilder,
            PredefinedElements.GeminiResultParser,
            PredefinedElements.geminiModelMaxRequestInputTokens[modelName],
            PredefinedElements.geminiModelMaxRequestOutputTokens[modelName]
         );

        Settings.Request reqSettings = new(
            30,
            PredefinedElements.apiResponseTypes
        );

        Settings.Prompt promptSettings = new(
            Prompts.defaultSystemPromptTemplate,
            Prompts.descriptionInformationExtratorUserPrompt,
            Prompts.descriptionInformationExtratorSystemPredefinedSet,
            null
        );
        LLMParser.Parser llmParser;
        public AIProcessor(string geminiKey = "")
        {
            Dictionary<string, string> systemPromptParams;
            var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            if (String.IsNullOrEmpty(geminiKey))
            {
                geminiKey = config["ApiKeys:GeminiKey"] ?? "";
            }

            apiSettings = new(
                geminiKey,
                PredefinedElements.geminiApiKeyHeader,
                PredefinedElements.GeminiApiStringBuilder(modelName),
                PredefinedElements.GeminiRequestBodyBuilder,
                PredefinedElements.GeminiResultParser,
                PredefinedElements.geminiModelMaxRequestInputTokens[modelName],
                PredefinedElements.geminiModelMaxRequestOutputTokens[modelName]
             );

            llmParser = new([apiSettings], reqSettings);
        }
        public async Task<(List<string?>, List<string>)> ProcessUnifiedSchemas(List<string> offers, Dictionary<string, string> systemPromptParams)
        {
            List<string> errors = new List<string>();
            List<string> outputs = new List<string>();

            if (systemPromptParams != null && systemPromptParams?.Count > 0)
            {
                promptSettings = new(
                    Prompts.defaultSystemPromptTemplate,
                    Prompts.descriptionInformationExtratorUserPrompt,
                    systemPromptParams,
                    null
                );
            }
            List<string> errorMessages = new List<string>();
            List<string?> aiOutput = new List<string?>();
            try
            {
                
                try
                {
                    aiOutput = await llmParser.ParseBatchAsync(offers, promptSettings, ParserHelpers.ParsingErrorAction.Retry);
                }
                catch (Exception ex)
                {

                    errorMessages.Add(ex.ToString());
                    foreach (var a in llmParser.GetLastBatchErrors())
                        errorMessages.Add(a);
                }
            }
            catch (Exception e)
            {

                errors.Add($"An error while processing bath {e}");
            }
            errors.AddRange(errorMessages);
            return (aiOutput, errors);
        }

        private List<string>? ProcessBenefits(UnifiedOfferSchemaClass offer, UnifiedOfferSchemaClass? aiOffer)
        {
            if (aiOffer?.benefits?.Count > 0 && (offer.benefits == null || offer.benefits.Count > 0))
                offer.benefits = aiOffer.benefits;

            return offer.benefits ?? new List<string>();
        }

        Requirements ProcessRequirements(UnifiedOfferSchemaClass offer, UnifiedOfferSchemaClass? aiOffer)
        {
            if (offer == null)
                throw new NullReferenceException("Offer cannot be null when processing requirements.");

            if (aiOffer?.requirements != null && aiOffer?.requirements?.skills?.Count > 0)
            {
                if (offer.requirements == null)
                    offer.requirements = new Requirements();
                else
                    offer.requirements.skills = aiOffer.requirements.skills;

                if (aiOffer.requirements.languages?.Count > 0 && (offer.requirements.languages == null || offer.requirements.languages.Count > 0))
                    offer.requirements.languages = aiOffer.requirements.languages;
            }

            return offer.requirements ?? new Requirements();
        }
    }
}
