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
        // Aplikujpl pełen url w polu w uniwersalnym schemacie
        // AplikujPl dodac logourl 
        static void Main(string[] args)
        {
            args[2] = "1";
            args[1] = "100";
            args[0] = "1";

            if (args.Length < 2)
            {
                Console.WriteLine("Użycie: App.exe <siteTypeId> <offerAmount>");
                Console.WriteLine("Przykład: App.exe 1 100");
                return;
            }

            if (!int.TryParse(args[0], out int siteTypeId))
            {
                Console.WriteLine("Pierwszy parametr musi być liczbą określającą typ portalu.");
                return;
            }

            if (!int.TryParse(args[1], out int offerAmount))
            {
                Console.WriteLine("Drugi parametr musi być liczbą określającą ilość ofert.");
                return;
            }

            if (!int.TryParse(args[2], out int clientType))
            {
                Console.WriteLine("Trzeci parameter musi byś liczbą od 0-1 okreslającą rodzaj scrapowania");
                return;
            }

            OfferSitesTypes type = (OfferSitesTypes)siteTypeId;
            ClientType clientTypeEnum = (ClientType)clientType;

            List<UnifiedOfferSchemaClass> unifiedOfferSchemas = new List<UnifiedOfferSchemaClass>();
            BaseHtmlScraper? scrapper = null;
            BaseUrlBuilder? urlBuilder = null;
            AIProcessor aiParser = new AIProcessor();
            ConstValues.PolishCities = JsonConvert.DeserializeObject<List<PlCityObject>>(File.ReadAllText("../../../../Models/ConstData/pl.json")) ?? new List<PlCityObject>();



            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type, clientTypeEnum);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Justjoinit:
                    scrapper = (JustJoinItScrapper)FactoryScrapper.CreateScrapper(type, clientTypeEnum);
                    urlBuilder = (JustJoinItBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Olxpraca:
                    scrapper = (OlxpracaScrapper)FactoryScrapper.CreateScrapper(type, clientTypeEnum);
                    urlBuilder = (OlxPracaUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                case OfferSitesTypes.Aplikujpl:
                    scrapper = (AplikujplScrapper)FactoryScrapper.CreateScrapper(type, clientTypeEnum);
                    urlBuilder = (AplikujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
                default:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type, clientTypeEnum);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);
                    break;
            }

            var paginator = new PaginationModule(scrapper, urlBuilder, offerAmount);
            (string outputJson, List<string> htmls) = paginator.FetchAllOffersAsync().Result;
            int i = 0;
            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    List<PracujplSchema> pracujSchemas = OfferMapper.DeserializeJson<List<PracujplSchema>>(outputJson);
                    List<string> unifSchemasJson = new List<string>();
                    List<List<string>> pracujPlAiOutput = new List<List<string>>();
                    foreach (PracujplSchema offer in pracujSchemas)
                    {
                        UnifiedOfferSchemaClass unifOffer = OfferMapper.ToUnifiedSchema<List<PracujplSchema>>(offer);
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
                        UnifiedOfferSchemaClass olxSch = OfferMapper.ToUnifiedSchema<List<OlxPracaSchema>>(offer, htmls.ElementAt(i++));
                        unifiedOfferSchemas.Add(olxSch);
                    }
                    break;
                case OfferSitesTypes.Justjoinit:
                    List<JustJoinItSchema> justJoinItSchemas = OfferMapper.DeserializeJson<List<JustJoinItSchema>>(outputJson);
                    List<UnifiedOfferSchemaClass> justJoinItUnifSchemas = new List<UnifiedOfferSchemaClass>();
                    List<List<string>> aiOutputsJustJoinit = new List<List<string>>();

                    foreach (JustJoinItSchema offer in justJoinItSchemas)
                    {
                        UnifiedOfferSchemaClass unifOffer = OfferMapper.ToUnifiedSchema<List<JustJoinItSchema>>(offer, htmls.ElementAt(i++));
                        unifiedOfferSchemas.Add(unifOffer);
                    }
                    //ExportToTxtt.ExportToTxt(justJoinItUnifSchemas, "justJoinItData.txt");
                    break;
                case OfferSitesTypes.Aplikujpl:
                    List<AplikujplSchema> aplikujSchemas = OfferMapper.DeserializeJson<List<AplikujplSchema>>(outputJson);
                    List<UnifiedOfferSchemaClass> aplikujUnifSchemas = new List<UnifiedOfferSchemaClass>();
                    List<List<string>> aiOutputsAplikuj = new List<List<string>>();
                    foreach (AplikujplSchema offer in aplikujSchemas)
                    {
                        UnifiedOfferSchemaClass unifOffer = OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmls.ElementAt(i++));
                        unifiedOfferSchemas.Add(OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmls.ElementAt(i++)));
                    }
                    
                    break;
                default:
                   
                    break;
            }
            // Processed by Ai
            List<UnifiedOfferSchemaClass> processedOffers = GetAiOutput(aiParser, unifiedOfferSchemas).Result;

            string output = JsonConvert.SerializeObject(processedOffers, Formatting.Indented);
            Console.WriteLine(output);
            Console.ReadKey();
        }
        static async Task<List<UnifiedOfferSchemaClass>> GetAiOutput(AIProcessor llm, List<UnifiedOfferSchemaClass> offers) => await llm.ProcessUnifiedSchemas(offers);
    }
}