using LLMParser;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
                if (aiOutput != "")
                {
                    outputs.Add(aiOutput);
                    //TODO some parsing to get interesting outcome

                }
            }
            return offers;
        }
    }
}
