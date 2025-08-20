namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        const string baseUrl = "https://www.pracuj.pl/";
        public override async Task<string> GetOfferAsync(string parameters = "", string url = "")
        {
            string htmlSource = await GetHtmlAsync(url);

            return null;
        }
    }
}
