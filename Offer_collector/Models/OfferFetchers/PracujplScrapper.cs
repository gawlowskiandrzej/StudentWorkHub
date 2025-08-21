using Newtonsoft.Json.Linq;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        
        public override async Task<string> GetOfferAsync(string url = "")
        {
            string baseUrl = PracujPlUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJson(allJs);
            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();

            foreach (JToken offer in offerListJs)
            {
                pracujplSchemas.Add(GetPracujplObject(offer));
            }

            return offerListJs.ToString() ?? "";
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        private string GetAllJson(string html) => GetJsonFragment(html, "<script id=\"__NEXT_DATA__\" type=\"application/json\">(.*?)</script>");

        private PracujplSchema GetPracujplObject(JToken token) => token.ToObject<PracujplSchema>() ?? new PracujplSchema();
        private List<JToken> GetOffersJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("props" +
                ".pageProps" +
                ".dehydratedState" +
                ".queries[0]" +
                ".state" +
                ".data" +
                ".groupedOffers[*]");
            return offerListJs;
        }
    }
}
