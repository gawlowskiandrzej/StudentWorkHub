using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Offer_collector.Models.AI
{
    internal class AiApi
    {
        // Z mojego researchu dobre to Groq oraz OpenRouter gdzie Groq ma korzystniejesze rate limity

        private readonly string _authToken = @"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";
        private readonly string _baseUrl = @"https://openrouter.ai/api/v1/chat/completions";
        private readonly string _aiModel = "meta-llama/llama-3.3-8b-instruct:free";
        private static HttpClient? _restClient;
        public AiApi(string authToken = "")
        {
            if (authToken != "")
                _authToken = authToken;
            if (_restClient == null)
            {
                _restClient = new HttpClient()
                {
                    BaseAddress = new Uri(_baseUrl),
                };
                _restClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _restClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authToken);
            }

        }
        string CreatePrompt(string myOfferString, PromptType promptType)
        {
            SendPromptObject prompt = new SendPromptObject();
            AiPromptParameters promptParameters = PromptFactory.GetPromptParameters(promptType);
            string message = $@"
            You are an AI that extracts structured data from job offers.\n\nReturn only valid JSON matching the following C# class structure:\n\n```csharp\npublic class Skill\n{{\n    public string? name {{ get; set; }}\n    public int? years {{ get; set; }}\n}}\n\npublic class Requirements\n{{\n    public List<Skill>? skills {{ get; set; }}\n    public List<string>? education {{ get; set; }}\n    public List<Language>? languages {{ get; set; }}\n    public List<string>? benefits {{ get; set; }}\n}}\n\npublic class Language\n{{\n    public string? name {{ get; set; }}\n    public string? level {{ get; set; }}\n}}\n```\n\n---\n\n
            ### Example input:\n {promptParameters.ExampleDescriptionStructure}\n 
            ### Example output: {promptParameters.ExampleRequirementsStructure}\n
            ### Task: {promptParameters.ExampleTaskStructure}\n
            ### Text: {myOfferString}
            ";
            prompt.model = _aiModel;
            prompt.messages.Add(new Message
            {
                content = message,
                role = "user"
            });

            return JsonConvert.SerializeObject(prompt);
        }
        public async Task<DTOApiObject> SendPrompt(string myOfferString, PromptType promptType)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _restClient.BaseAddress);

            string prompt = CreatePrompt(myOfferString, promptType);

            request.Content = new StringContent(prompt, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await _restClient.SendAsync(request);
            string rawJsonResponse = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<DTOApiObject>(rawJsonResponse) ?? new DTOApiObject();
        }
    }
}
