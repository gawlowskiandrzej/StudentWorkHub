using Offer_collector.Models.OfferScrappers;

namespace Offer_collector.Models.OfferFetchers
{
    public static class FactoryScrapper
    {
        public static BaseHtmlScraper CreateScrapper(OfferSitesTypes type, ClientType clientType)
        {
            return type switch
            {
                OfferSitesTypes.Pracujpl => new PracujplScrapper(clientType),
                OfferSitesTypes.Aplikujpl => new AplikujplScrapper(clientType),
                OfferSitesTypes.Justjoinit => new JustJoinItScrapper(clientType),
                OfferSitesTypes.Olxpraca => new OlxpracaScrapper(clientType),
                _ => throw new ArgumentException($"No implementation for {type}")
            };
        }

    }
}
