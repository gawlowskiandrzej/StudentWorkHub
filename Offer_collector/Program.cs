using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //OfferSitesTypes type = OfferSitesTypes.Pracujpl;

            //PracujplScrapper scraper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type);
            //PracujPlUrlBuilder urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);

            OfferSitesTypes type = OfferSitesTypes.Olxpraca;

            OlxpracaScrapper scraper = (OlxpracaScrapper)FactoryScrapper.CreateScrapper(type);
            OlxPracaUrlBuilder urlBuilder = (OlxPracaUrlBuilder)UrlBuilderFactory.Create(type);

            string fullUrl = urlBuilder.BuildUrl();
            string outputJson = scraper.GetOfferAsync(fullUrl).Result;



            Console.WriteLine(outputJson);
            Console.ReadKey();
        }
    }
}