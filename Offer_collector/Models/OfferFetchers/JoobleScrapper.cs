using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.BrowserTools;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;

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
        
        private string GetAllJson(string html) => GetSubstringJson(html); // @"(?is)<script\b(?=[^>]*\btype\s*=\s*(?:""|')?application/ld\+json(?:;[^""'>]*)?(?:""|')?)[^>]*>(.*?)</script>"
        private string GetSubstringJson(string htmlSource)
        {
            const string marker = "window.__INITIAL_STATE__ = ";
            const string endMarker = "}}}<";

            var startIndex = htmlSource.IndexOf(marker);
            if (startIndex == -1) return null;

            startIndex = htmlSource.IndexOf('{', startIndex);
            if (startIndex == -1) return null;

            var endIndex = htmlSource.IndexOf(endMarker, startIndex);
            if (endIndex == -1) return null;

            endIndex += endMarker.Length;

            var json = htmlSource.Substring(startIndex - 1, endIndex - startIndex).Trim();

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);
            //json = DecodeUnicodeStrict(json);
            //DecodeUnicode(json)
            return json;
        }
        private JoobleSchema GetJoobleObject(JToken token) => token.ToObject<JoobleSchema>() ?? new JoobleSchema();
        public JToken GetPagination(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            return GetPagination(allJson, parser);
        }
        private List<JToken> GetOffersJson(string allJson)
        {
            // TODO make auto adding new page and feth all offers
            JsonParser parser = new JsonParser(allJson);
            //JToken pagination = GetPagination(allJson, parser);
            
            List<JToken> offerListJs = parser
            .GetSpecificJsonFragments(
                "serpJobs" +
                ".jobs[0]" +
                ".items[*]")
            .Where(token => token["componentName"] == null)
            .ToList();

            return offerListJs;
        }
        private JToken GetPagination(string allJson, JsonParser parser) => parser.GetSpecificJsonFragments("serpJobs")[0];
    }
}
