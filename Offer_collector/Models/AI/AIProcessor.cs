using LLMParser;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Offer_collector.Models.PracujPl;
namespace Offer_collector.Models.AI
{
    internal class AIProcessor
    {
        Parser aiParser;
        public AIProcessor(string geminiKey = "")
        {
            if (String.IsNullOrEmpty(geminiKey))
            {
                var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

                geminiKey = config["ApiKeys:GeminiKey"] ?? "";
            }
            aiParser = new Parser(geminiKey);
        }
        public async Task<List<UnifiedOfferSchema>> ProcessUnifiedSchemas(List<UnifiedOfferSchema> offers)
        {
            List<string>outputs = new List<string>();
            foreach (UnifiedOfferSchema offer in offers)
            {
                string jsOffer = JsonConvert.SerializeObject(offer, Formatting.Indented);
                string aiOutput = (await aiParser.ParseBatchAsync(new List<string> { jsOffer })).FirstOrDefault() ?? "";
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
            if (aiOffer?.benefits?.Count > 0 && (offer?.benefits == null || offer.benefits.Count > 0))
                offer.benefits = aiOffer.benefits;

            return offer.benefits;
        }

        Requirements ProcessRequirements(UnifiedOfferSchema offer, UnifiedOfferSchema? aiOffer)
        {
            if (aiOffer?.requirements?.skills?.Count > 0)
            {
                if (offer?.requirements == null && offer != null)
                    offer.requirements = new Requirements();
                else
                    offer.requirements.skills = aiOffer.requirements.skills;
            }
            
            if (aiOffer?.requirements?.languages?.Count > 0 && (offer?.requirements?.languages == null || offer.requirements.languages.Count > 0))
                offer.requirements.languages = aiOffer.requirements.languages;


            return offer.requirements ?? new Requirements();
        }
    }
}
