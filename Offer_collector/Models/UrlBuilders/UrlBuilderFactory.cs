namespace Offer_collector.Models.UrlBuilders
{
    public static class UrlBuilderFactory
    {
        public static IUrlBuilder Create(OfferSitesTypes siteType)
        {
            return siteType switch
            {
                OfferSitesTypes.Pracujpl => new PracujPlUrlBuilder(),
                OfferSitesTypes.Jooble => new JoobleUrlBuilder(),
                OfferSitesTypes.Justjoinit => new JustJoinItBuilder(),
                OfferSitesTypes.Olxpraca => new OlxPracaUrlBuilder(),
                _ => throw new ArgumentException($"No implementation for {siteType}")
            };
        }
    }
}
