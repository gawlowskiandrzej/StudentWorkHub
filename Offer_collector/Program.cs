using Offer_collector.Interfaces;
using Offer_collector.Models.OfferFetchers;

namespace Offer_collector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseHtmlScraper scraper;

            switch (args[1])
            {
                case "1": scraper = new PracujplScrapper(); break;
                case "2": scraper = new OlxpracaScrapper(); break;
                case "3": scraper = new JoobleScrapper(); break;
                case "4": scraper = new JustJoinItScrapper(); break;

                default:
                    scraper = new PracujplScrapper();
                    break;
            }


        }
    }
}