using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;

namespace Offer_collector.Models.OfferFetchers
{
    internal class OlxpracaScrapper : BaseHtmlScraper
    {
        public override async Task<string> GetOfferAsync(string url = "")
        {
            string baseUrl = PracujPlUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJson(allJs);
            List<OlxPracaSchema> olxPracaSchema = new List<OlxPracaSchema>();

            foreach (JToken offer in offerListJs)
            {
                olxPracaSchema.Add(GetOlxPracaObject(offer));
            }

            return JsonConvert.SerializeObject(offerListJs, Formatting.Indented) ?? "";
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        private string GetAllJson(string html) => GetSubstringJson(html);
        private string GetSubstringJson(string htmlSource) 
        {
            const string marker = "window.__PRERENDERED_STATE__=";
            const string endMarker = "}}]}}\";";

            var startIndex = htmlSource.IndexOf(marker);
            if (startIndex == -1) return null;

            startIndex = htmlSource.IndexOf('{', startIndex);
            if (startIndex == -1) return null;

            var endIndex = htmlSource.IndexOf(endMarker, startIndex);
            if (endIndex == -1) return null;

            endIndex += endMarker.Length;

            var json = htmlSource.Substring(startIndex-1, endIndex - startIndex).Trim();

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);
            //json = DecodeUnicodeStrict(json);
            json = JsonConvert.DeserializeObject<string>(json)!;
            JsonDocument jsonDoc = JsonDocument.Parse(json);
            string pretty = jsonDoc.RootElement.ToString();
            //DecodeUnicode(json)
            return pretty;
        }
        private OlxPracaSchema GetOlxPracaObject(JToken token) => token.ToObject<OlxPracaSchema>() ?? new OlxPracaSchema();
        private List<JToken> GetOffersJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("listing" +
                ".listing" +
                ".ads[*]");
            return offerListJs;
        }
    }
}
