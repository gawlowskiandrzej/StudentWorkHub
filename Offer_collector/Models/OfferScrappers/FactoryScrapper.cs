namespace Offer_collector.Models.OfferFetchers
{
    public static class FactoryScrapper
    {
        public static BaseHtmlScraper CreateScrapper(OfferSitesTypes type)
        {
            return type switch
            {
                OfferSitesTypes.Pracujpl => new PracujplScrapper(),
                OfferSitesTypes.Jooble => new JoobleScrapper(),
                OfferSitesTypes.Justjoinit => new JustJoinItScrapper(),
                OfferSitesTypes.Olxpraca => new OlxpracaScrapper(),
                _ => throw new ArgumentException($"No implementation for {type}")
            };
        }

    }
}
