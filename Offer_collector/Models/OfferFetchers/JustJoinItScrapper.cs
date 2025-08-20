namespace Offer_collector.Models.OfferFetchers
{
    internal class JustJoinItScrapper : BaseHtmlScraper
    {
        public override async Task<string> GetOfferAsync(string url)
        {
            string htmlSource = await GetHtmlAsync(url);

            return null;
        }
    }
}
