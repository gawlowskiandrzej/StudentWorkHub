namespace Offer_collector.Models.AI
{
    public static class PromptFactory
    {
        public static AiPromptParameters GetPromptParameters(PromptType variant)
        {
            return variant switch
            {
                PromptType.FromDescription => new AiPromptParameters
                {
                    
                },
                PromptType.FromListOfSkills => new AiPromptParameters
                {

                }
            };
        }
    }
}
