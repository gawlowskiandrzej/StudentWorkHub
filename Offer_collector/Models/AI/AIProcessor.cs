using LLMParser;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace Offer_collector.Models.AI
{
    internal class AIProcessor
    {
        static int bathSize = 1;
        static string modelName = PredefinedElements.geminiApiModelNames[0];
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
        public AIProcessor(Dictionary<string, string> systemPromptParams, string geminiKey = "")
        {

            var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            if (String.IsNullOrEmpty(geminiKey))
            {
                geminiKey = config["ApiKeys:GeminiKey"] ?? "";
            }

            if (systemPromptParams != null && systemPromptParams?.Count > 0)
            {
                promptSettings = new(
                    Prompts.defaultSystemPromptTemplate,
                    Prompts.descriptionInformationExtratorUserPrompt,
                    systemPromptParams,
                    null
                );
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
        public async Task<(List<string>, List<string>)> ProcessUnifiedSchemas(List<UnifiedOfferSchemaClass> offers)
        {

            List<string> outputs = new List<string>();
            List<string> errors = new List<string>();

            List<AiProcessedOffer> aiProcessedOffers = new List<AiProcessedOffer>();
            for (int i = 0; i < offers.Count / bathSize; i++)
            {
                IEnumerable<UnifiedOfferSchemaClass> bath = offers.Skip(i * bathSize).Take(bathSize);
                {
                    List<string> errorMessages = new List<string>();
                    try
                    {
                        List<string> serializedOffers = new List<string>();
                        foreach (UnifiedOfferSchemaClass item in bath)
                        {
                            serializedOffers.Add(JsonConvert.SerializeObject(item, Formatting.Indented));
                        }
                        List<string?> aiOutput = new List<string?>();
                        try
                        {
                            aiOutput = await llmParser.ParseBatchAsync(serializedOffers, promptSettings, ParserHelpers.ParsingErrorAction.Retry);
                        }
                        catch (Exception ex)
                        {

                            errorMessages.Add(ex.ToString());
                            foreach (var a in llmParser.GetLastBatchErrors())
                                errorMessages.Add(a);
                        }

                        if (aiOutput.Count > 0)
                        {
                            try
                            {
                                outputs.AddRange(aiOutput);
                                //List<UnifiedOfferSchemaClass>? aiOffers = JsonConvert.DeserializeObject<List<UnifiedOfferSchemaClass>>(aiOutput);
                                //foreach (UnifiedOfferSchemaClass aiOffer in aiOffers ?? new List<UnifiedOfferSchemaClass>())
                                //{
                                //    offer.requirements = ProcessRequirements(offer, aiOffer);
                                //    offer.benefits = ProcessBenefits(offer, aiOffer);
                                //}

                            }
                            catch (Exception e)
                            {
                                errorMessages.Add($"Error deserializing ai output for offers {bath}: {e.Message}");
                            }

                        }
                    }
                    catch (Exception e)
                    {

                        errors.Add($"An error while processing bath {e}");
                    }
                    errors.AddRange(errorMessages);
                }
            }
            return (outputs, errors);
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
