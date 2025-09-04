using Newtonsoft.Json;
using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            OfferSitesTypes type = OfferSitesTypes.Pracujpl;
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
            string outputJson = scrapper.GetOfferAsync(fullUrl).Result;

            

            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
    }
}