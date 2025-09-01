using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.BrowserTools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferFetchers
{
    internal class JoobleScrapper : BaseHtmlScraper
    {
        public override async Task<string> GetOfferAsync(string url = "")
        {
            string baseUrl = JoobleUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJson(allJs);
            List<JoobleSchema> joobleSchemas = new List<JoobleSchema>();

            foreach (JToken offer in offerListJs)
            {
                joobleSchemas.Add(GetJoobleObject(offer));
            }

            return JsonConvert.SerializeObject(offerListJs, Formatting.Indented) ?? "";
        }
        private async Task<string> GetHtmlSource(string url)
        {

            // TODO nodejs server with stealth plugin
            // IMPLEMENT api and automatisation to craeate token playwright instead


            // Creating headless browser and send get request
            HeadlessBrowser headlessBrowser = new HeadlessBrowser(url);
            string htmlSource = await headlessBrowser.GetWebPageSource(url);

            return htmlSource;
        }
        private string GetAllJson(string html) => GetJsonFragment(html, "<script data-rh=\"true\" type=\"application/ld+json\">(.*?)</script>");

        private JoobleSchema GetJoobleObject(JToken token) => token.ToObject<JoobleSchema>() ?? new JoobleSchema();
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
