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

            JToken jooblePaginationJToken = GetPaginationJToken(allJs);
            List<JToken> offertJToken = GetOffersJsonJToken(allJs);
            List<JoobleSchema> joobleSchemas = new List<JoobleSchema>();

            foreach (JToken token in offertJToken)
                joobleSchemas.Add(GetOfferObject(token));

            JooblePagination pagination = GetPaginationObject(jooblePaginationJToken);
            JoobleSchemaWithPagination validObject = GetValidObject(pagination, joobleSchemas);

            return JsonConvert.SerializeObject(validObject, Formatting.Indented) ?? "";
        }
        private async Task<string> GetHtmlSource(string url)
        {
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
        private JoobleSchema GetOfferObject(JToken token) => token.ToObject<JoobleSchema>() ?? new JoobleSchema();
        private JooblePagination GetPaginationObject(JToken token) => token.ToObject<JooblePagination>() ?? new JooblePagination();
        private JoobleSchemaWithPagination GetValidObject(JooblePagination pagination, List<JoobleSchema> schemas) => new JoobleSchemaWithPagination(pagination, schemas);
        private JToken GetPaginationJToken(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            return GetPagination(allJson, parser);
        }
        private List<JToken> GetOffersJsonJToken(string allJson)
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
