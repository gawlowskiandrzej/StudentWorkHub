using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OfferSitesTypes type = OfferSitesTypes.Pracujpl;

            PracujplScrapper scraper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type);
            PracujPlUrlBuilder urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);

            string fullUrl = urlBuilder.BuildUrl();
            string asd = scraper.GetOfferAsync(fullUrl).Result;



            Console.WriteLine(asd);
            Console.ReadKey();
        }
    }
}