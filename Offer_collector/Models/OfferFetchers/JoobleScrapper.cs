using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            // TODO 
            // IMPLEMENT Playwright żeby pobrać kod źródła niestety tylko jaka przeglądarka
            _client.DefaultRequestHeaders.Add("User-Agent", "C# client");
            _client.DefaultRequestHeaders.Add("Cookie", "SessionCookie.pl=-6047165956284760389*3660289531847431222*638920692275332364; SessionUtmCookie.pl=; __cf_bm=R8.kf2vs5Ix6_K8znJPYc8xz9RMtag90Hj2KrxAWMOw-1756472428-1.0.1.1-GR9dsdGX3b3LVA.mVXCWholqo0RGQMA1yDn6rkHdByZSLkapNcY3EhXLTV2.g6a2Pgq8aDL2F9r_S6vpHxG7n74GDqQs_a4ZawJPNt_PJ2A; .AspNetCore.Session=CfDJ8K1oDNHRm91Mr3b2wux0ARrLNOUR1Jh4Njk5/KmzmNskEOm/cXUjZnK2/7FBgSyfpZXrPz6DTjR8boUIqJD8eZMRtIQpEZ9vsgCnG3AE5JZh5M/jOeLS4JJTfUfADqSAvLaRNgFARW88sz28Ul+JwpqUS8lOFdJvKPB4ROUapaJU; TrafficSource=262145*0; ULang=0; dt_groups=; rk_groups=113-0,132-0,498-0; sever=40; user_bucket=9");
            var cos = await GetHtmlAsync(url);
            return cos;
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
