using Offer_collector.Models;
using Offer_collector.Models.Jooble;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            OfferSitesTypes type = OfferSitesTypes.Justjoinit;
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
                case OfferSitesTypes.Jooble:
                    scrapper = (JoobleScrapper)FactoryScrapper.CreateScrapper(type);
                    urlBuilder = (JoobleUrlBuilder)UrlBuilderFactory.Create(type);
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
                    break;
                case OfferSitesTypes.Olxpraca:
                    List<OlxPracaSchema> olxSchemas = OfferMapper.DeserializeJson<List<OlxPracaSchema>>(outputJson);
                    List<UnifiedOfferSchema> olxUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (OlxPracaSchema offer in olxSchemas)
                        olxUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<OlxPracaSchema>>(offer, htmlRaw));
                    break;
                case OfferSitesTypes.Justjoinit:
                    List<JustJoinItSchema> justJoinItSchemas = OfferMapper.DeserializeJson<List<JustJoinItSchema>>(outputJson);
                    List<UnifiedOfferSchema> justJoinItUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (JustJoinItSchema offer in justJoinItSchemas)
                        justJoinItUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<JustJoinItSchema>>(offer, htmlRaw));
                    break;
                case OfferSitesTypes.Jooble:
                    List<JoobleSchema> joobleSchemas = OfferMapper.DeserializeJson<List<JoobleSchema>>(outputJson);
                    List<UnifiedOfferSchema> joobleUnifSchemas = new List<UnifiedOfferSchema>();
                    foreach (JoobleSchema offer in joobleSchemas)
                        joobleUnifSchemas.Add(OfferMapper.ToUnifiedSchema<List<JoobleSchema>>(offer, htmlRaw));
                    break;
                default:
                   
                    break;
            }

            
            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
    }
}