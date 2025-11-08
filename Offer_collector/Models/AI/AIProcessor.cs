using LLMParser;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace Offer_collector.Models.AI
{
    internal class AIProcessor
    {
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
        public AIProcessor(string geminiKey = "")
        {


            if (String.IsNullOrEmpty(geminiKey))
            {
                var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

                geminiKey = config["ApiKeys:GeminiKey"] ?? "";
                apiSettings = new(
                    geminiKey,
                    PredefinedElements.geminiApiKeyHeader,
                    PredefinedElements.GeminiApiStringBuilder(modelName),
                    PredefinedElements.GeminiRequestBodyBuilder,
                    PredefinedElements.GeminiResultParser,
                    PredefinedElements.geminiModelMaxRequestInputTokens[modelName],
                    PredefinedElements.geminiModelMaxRequestOutputTokens[modelName]
                 );
            }
            llmParser = new([apiSettings], reqSettings);
        }
        public async Task<List<UnifiedOfferSchema>> ProcessUnifiedSchemas(List<UnifiedOfferSchema> offers)
        {
            List<string>outputs = new List<string>();
            foreach (UnifiedOfferSchema offer in offers)
            {
                string jsOffer = JsonConvert.SerializeObject(offer, Formatting.Indented);
                string aiOutput = "";
                try
                {
                    aiOutput = (await llmParser.ParseBatchAsync(new List<string> { jsOffer}, promptSettings)).FirstOrDefault() ?? "";
                }
                catch (Exception ex)
                {
                    offer.aiErrorMessages.Add(ex.ToString());
                    foreach (var a in llmParser.GetLastBatchErrors())
                        Console.WriteLine(a.ToString());
                }

                if (aiOutput != "" && offer != null)
                {
                    outputs.Add(aiOutput);
                    UnifiedOfferSchema? aiOffer = JsonConvert.DeserializeObject<UnifiedOfferSchema>(aiOutput);
                    offer.requirements = ProcessRequirements(offer, aiOffer);
                    offer.benefits = ProcessBenefits(offer, aiOffer); 
                }
            }
            return offers;
        }

        private List<string>? ProcessBenefits(UnifiedOfferSchema offer, UnifiedOfferSchema? aiOffer)
        {
            if (aiOffer?.benefits?.Count > 0 && (offer.benefits == null || offer.benefits.Count > 0))
                offer.benefits = aiOffer.benefits;

            return offer.benefits ?? new List<string>();
        }

        Requirements ProcessRequirements(UnifiedOfferSchema offer, UnifiedOfferSchema? aiOffer)
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
