using Offer_collector.Models;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.Jooble;
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



            switch (type)
            {
                case OfferSitesTypes.Pracujpl:
                    List<PracujplSchema> pracujSchemas = OfferMapper.DeserializeJson<List<PracujplSchema>>(outputJson);
                    List<UnifiedOfferSchema> pracujUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (PracujplSchema offer in pracujSchemas)
                        pracujUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<PracujplSchema>>(offer));
                    //ExportToTxtt.ExportToTxt(pracujUnifSchemas, "pracujData.txt");
                    break;
                case OfferSitesTypes.Olxpraca:
                    List<OlxPracaSchema> olxSchemas = OfferMapper.DeserializeJson<List<OlxPracaSchema>>(outputJson);
                    List<UnifiedOfferSchema> olxUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (OlxPracaSchema offer in olxSchemas)
                        olxUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<OlxPracaSchema>>(offer, htmlRaw));
                    //ExportToTxtt.ExportToTxt(olxUnifSchemas, "olxData.txt");
                    break;
                case OfferSitesTypes.Justjoinit:
                    List<JustJoinItSchema> justJoinItSchemas = OfferMapper.DeserializeJson<List<JustJoinItSchema>>(outputJson);
                    List<UnifiedOfferSchema> justJoinItUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (JustJoinItSchema offer in justJoinItSchemas)
                        justJoinItUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<JustJoinItSchema>>(offer, htmlRaw));
                    //ExportToTxtt.ExportToTxt(justJoinItUnifSchemas, "justJoinItData.txt");
                    break;
                case OfferSitesTypes.Aplikujpl:
                    List<AplikujplSchema> aplikujSchemas = OfferMapper.DeserializeJson<List<AplikujplSchema>>(outputJson);
                    List<UnifiedOfferSchema> aplikujUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (AplikujplSchema offer in aplikujSchemas)
                        aplikujUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<AplikujplSchema>>(offer, htmlRaw));
                    break;
                default:
                   
                    break;
            }

            
            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
    }
}