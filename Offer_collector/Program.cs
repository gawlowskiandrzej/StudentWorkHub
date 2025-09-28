using LLMParser;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.AI;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector
{
    internal class Program
    {
        // TODO 
        // Obsługa błędów (Exceptions na każdy etap + wysalnie loga do bazy)
        // Cashowanie danych - firmy
        // Tylko polskie oferty
        // Olxpraca logoUrl null - sprawdzić !!
        // JustJoinIt uzupełnić level skilla
        // Aplikujpl pełen url w polu w uniwersalnym schemacie
        // JustJoinIt usunąć htmla żeby prompt nie był za długi
        // AplikujPl dodac logourl 
        static void Main(string[] args)
        {
            
            OfferSitesTypes type = OfferSitesTypes.Aplikujpl;
            BaseHtmlScraper? scrapper = null;
            BaseUrlBuilder? urlBuilder = null;
            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Justjoinit:
                    scrapper = (JustJoinItScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (JustJoinItBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Olxpraca:
                    scrapper = (OlxpracaScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (OlxPracaUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Aplikujpl:
                    scrapper = (AplikujplScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (AplikujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                default:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
            }

            string fullUrl = urlBuilder.BuildUrl();
            (string outputJson, string htmlRaw) = scrapper.GetOfferAsync(fullUrl).Result;
            string key = "AIzaSyD4NdOOnQ--vxG7rE72zRB6JGZhJILQUyU";

            LLMParser.Parser parser = new Parser(key);

            //AiApi ai = new AiApi();

            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    List<PracujplSchema> pracujSchemas = OfferMapper.DeserializeJson<List<PracujplSchema>>(outputJson);
                    List<UnifiedOfferSchema> pracujUnifSchemas = new List<UnifiedOfferSchema>();
                    List<string> unifSchemasJson = new List<string>();
                    foreach (PracujplSchema offer in pracujSchemas)
                    {
                        UnifiedOfferSchema unifOffer = OfferMapper.ToUnifiedSchema<List<PracujplSchema>>(offer);
                        pracujUnifSchemas.Add(unifOffer);
                       // var ouput = JsonConvert.DeserializeObject<Offer_collector.Models.AI.Message>(ai.SendPrompt(unifOffer.requirements.skills.Select(_ => _.skill).ToList()).Result).content;
                    }
                    // TODO extract requirements section in this case
                    List<string> aiOutputPracuj = GetAiOutput(parser, pracujUnifSchemas).Result;
                    //ExportToTxtt.ExportToTxt(output, "pracujData.txt");
                    break;
                case OfferSitesTypes.Olxpraca:
                    List<OlxPracaSchema> olxSchemas = OfferMapper.DeserializeJson<List<OlxPracaSchema>>(outputJson);
                    List<UnifiedOfferSchema> olxUnifSchemas = new List<UnifiedOfferSchema>();
                    List<string> outputs = new List<string>();
                    foreach (OlxPracaSchema offer in olxSchemas)
                    {
                        UnifiedOfferSchema olxSch = OfferMapper.ToUnifiedSchema<List<OlxPracaSchema>>(offer, htmlRaw);
                        olxUnifSchemas.Add(olxSch);
                        //outputs.Add(JsonConvert.DeserializeObject<Offer_collector.Models.AI.Message>(ai.SendPrompt(new List<string> { olxSch.description }).Result).content);
                        
                    }
                    //List<string> aiOutputOlx = GetAiOutput(parser, olxUnifSchemas).Result;
                    ExportToTxtt.ExportToTxt(olxUnifSchemas, "olxData.txt");
                    break;
                case OfferSitesTypes.Justjoinit:
                    List<JustJoinItSchema> justJoinItSchemas = OfferMapper.DeserializeJson<List<JustJoinItSchema>>(outputJson);
                    List<UnifiedOfferSchema> justJoinItUnifSchemas = new List<UnifiedOfferSchema>();
                    List<List<string>> aiOutputsJustJoinit = new List<List<string>>();

                    foreach (JustJoinItSchema offer in justJoinItSchemas)
                    {
                        UnifiedOfferSchema unifOffer = OfferMapper.ToUnifiedSchema<List<JustJoinItSchema>>(offer, htmlRaw);
                        justJoinItUnifSchemas.Add(unifOffer);
                        aiOutputsJustJoinit.Add(GetAiOutput(parser, new List<UnifiedOfferSchema> { unifOffer }).Result);
                    }
                    //ExportToTxtt.ExportToTxt(justJoinItUnifSchemas, "justJoinItData.txt");
                    break;
                case OfferSitesTypes.Aplikujpl:
                    List<AplikujplSchema> aplikujSchemas = OfferMapper.DeserializeJson<List<AplikujplSchema>>(outputJson);
                    List<UnifiedOfferSchema> aplikujUnifSchemas = new List<UnifiedOfferSchema>();
                    List<List<string>> aiOutputsAplikuj = new List<List<string>>();
                    foreach (AplikujplSchema offer in aplikujSchemas)
                    {
                        UnifiedOfferSchema unifOffer = OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmlRaw);
                        aplikujUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmlRaw));
                        aiOutputsAplikuj.Add(GetAiOutput(parser, new List<UnifiedOfferSchema> { unifOffer }).Result);
                    }
                    break;
                default:
                   
                    break;
            }

           
            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
        static async Task<List<string>> GetAiOutput(Parser llm, List<UnifiedOfferSchema> offers)
        {
            List<string> strings = new List<string>();
            foreach (UnifiedOfferSchema offer in offers)
               strings.Add(JsonConvert.SerializeObject(offer, Formatting.Indented));
            return await llm.ParseBatchAsync(strings);
        }
    }
}