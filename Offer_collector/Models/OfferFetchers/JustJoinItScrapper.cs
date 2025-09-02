using JustJoinIt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.UrlBuilders;
using OlxPraca;
using System.Text.Json;

namespace Offer_collector.Models.OfferFetchers
{
    internal class JustJoinItScrapper : BaseHtmlScraper
    {
        public override async Task<string> GetOfferAsync(string url = "")
        {
            string baseUrl = JustJoinItBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJson(allJs);
            List<JustJoinItSchema> justJoinItOffers = new List<JustJoinItSchema>();

            foreach (JToken offer in offerListJs)
            {
                justJoinItOffers.Add(GetJustJoinItSchema(offer));
            }

            return JsonConvert.SerializeObject(justJoinItOffers, Formatting.Indented) ?? "";
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        //private string GetAllJson(string html) => GetJsonFragment(html, "<script id=\"__NEXT_DATA__\" type=\"application/json\">(.*?)</script>");
        private string GetAllJson(string html) => GetSubstringJson(html);
        private string GetSubstringJson(string htmlSource)
        {
            // TODO Fix null issues with invalid casting JustJoinIt offers 
            const string marker = "self.__next_f.push([1,\"5:";
            const string endMarker = "}]]}]\\n\"])";

            var startIndex = htmlSource.IndexOf(marker);
            if (startIndex == -1) return null;

            startIndex = htmlSource.IndexOf('{', startIndex);
            if (startIndex == -1) return null;

            var endIndex = htmlSource.IndexOf(endMarker, startIndex);
            if (endIndex == -1) return null;

            endIndex += endMarker.Length;
            
            var json = htmlSource.Substring(startIndex, endIndex - startIndex-6).Trim();
            json = "\"" + json + "\"";
            if (json.EndsWith(";"))
                json = json.Substring(1, json.Length - 1);
            //json = DecodeUnicodeStrict(json);
            json = JsonConvert.DeserializeObject<string>(json)!;
            JsonDocument jsonDoc = JsonDocument.Parse(json);
            string pretty = jsonDoc.RootElement.ToString();
            //DecodeUnicode(json)
            return pretty;
        }
        private JustJoinItSchema GetJustJoinItSchema(JToken token)
        {
            try
            {
                return token.ToObject<JustJoinItSchema>() ?? new JustJoinItSchema();
            }
            catch (Exception)
            {
                return new JustJoinItSchema();
            }                
        }
        private List<JToken> GetOffersJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("state" +
                ".queries[3]" +
                ".state" +
                ".data" +
                ".pages[0]" +
                ".data[*]");
            return offerListJs;
        }
    }
}
