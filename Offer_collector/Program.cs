using LLMParser;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Offer_collector.Models;
using Offer_collector.Models.AI;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.ConstData;
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
        // Tylko polskie oferty
        // Olxpraca logoUrl null - sprawdzić !!
        // JustJoinIt uzupełnić level skilla
        // Aplikujpl pełen url w polu w uniwersalnym schemacie
        // AplikujPl dodac logourl 
        // Rozdzielać skille które są po przecinku jeżeli są
        // JustJoinIt sprawdzać po mieście zpliku json\
        // Parametr który określi ile oferty zaciągnąć procentowo 
        static void Main(string[] args)
        {
            
            OfferSitesTypes type = OfferSitesTypes.Pracujpl;
            int offerAmount = 100;
            List<UnifiedOfferSchema> unifiedOfferSchemas = new List<UnifiedOfferSchema>();
            BaseHtmlScraper? scrapper = null;
            BaseUrlBuilder? urlBuilder = null;
            AIProcessor aiParser = new AIProcessor();
            ConstValues.PolishCities = JsonConvert.DeserializeObject<List<PlCityObject>>(File.ReadAllText("../../../Models/ConstData/pl.json")) ?? new List<PlCityObject>();



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

            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    List<PracujplSchema> pracujSchemas = OfferMapper.DeserializeJson<List<PracujplSchema>>(outputJson);
                    List<string> unifSchemasJson = new List<string>();
                    List<List<string>> pracujPlAiOutput = new List<List<string>>();
                    foreach (PracujplSchema offer in pracujSchemas)
                    {
                        UnifiedOfferSchema unifOffer = OfferMapper.ToUnifiedSchema<List<PracujplSchema>>(offer);
                        unifiedOfferSchemas.Add(unifOffer);
                    }

                    //ExportToTxtt.ExportToTxt(output, "pracujData.txt");
                    break;
                case OfferSitesTypes.Olxpraca:
                    List<OlxPracaSchema> olxSchemas = OfferMapper.DeserializeJson<List<OlxPracaSchema>>(outputJson);
                    List<List<string>> olxaiOutput = new List<List<string>>();
                    List<string> outputs = new List<string>();
                    foreach (OlxPracaSchema offer in olxSchemas)
                    {
                        UnifiedOfferSchema olxSch = OfferMapper.ToUnifiedSchema<List<OlxPracaSchema>>(offer, htmlRaw);
                        unifiedOfferSchemas.Add(olxSch);
                    }
                    break;
                case OfferSitesTypes.Justjoinit:
                    List<JustJoinItSchema> justJoinItSchemas = OfferMapper.DeserializeJson<List<JustJoinItSchema>>(outputJson);
                    List<UnifiedOfferSchema> justJoinItUnifSchemas = new List<UnifiedOfferSchema>();
                    List<List<string>> aiOutputsJustJoinit = new List<List<string>>();

                    foreach (JustJoinItSchema offer in justJoinItSchemas)
                    {
                        UnifiedOfferSchema unifOffer = OfferMapper.ToUnifiedSchema<List<JustJoinItSchema>>(offer, htmlRaw);
                        unifiedOfferSchemas.Add(unifOffer);
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
                        unifiedOfferSchemas.Add(OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmlRaw));
                    }
                    
                    break;
                default:
                   
                    break;
            }
            // Processed by Ai
            List<UnifiedOfferSchema> processedOffers = GetAiOutput(aiParser, unifiedOfferSchemas).Result;


            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
        static async Task<List<UnifiedOfferSchema>> GetAiOutput(AIProcessor llm, List<UnifiedOfferSchema> offers) => await llm.ProcessUnifiedSchemas(offers);
    }
}