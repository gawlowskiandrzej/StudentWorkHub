using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;

namespace Offer_collector.Models.OfferFetchers
{
    internal class OlxpracaScrapper : BaseHtmlScraper
    {
        const int defaultCategory = 1;
        public override async Task<(string, string)> GetOfferAsync(string url = "")
        {
            try
            {
                string baseUrl = OlxPracaUrlBuilder.baseUrl;
                if (url != "")
                    baseUrl = url;
                string html = await GetHtmlSource(baseUrl);
                string allJs = GetAllJson(html);

                List<JToken> offerListJs = GetOffersJson(allJs);
                string cos = JsonConvert.SerializeObject(offerListJs, Formatting.Indented);
                List<OlxPracaSchema> olxPracaSchema = new List<OlxPracaSchema>();

                // TODO cashowanie kategori i tak są w jednym requset ale zawsze mniej operacji
                foreach (JToken offer in offerListJs)
                {
                    OlxPracaSchema obj = GetOlxPracaObject(offer);

                    obj.htmlOfferDetail = await GetHtmlSource(obj.url);
                    string htmlOfferDetail = GetSubstringJson(obj.htmlOfferDetail);
                    List<JToken> pol = GetOfferDetailsJson(htmlOfferDetail);
                    string categoriesListObj = JsonConvert.SerializeObject(pol);
                    obj.category.categoryDetails = GetOlxPracaCategoryById(obj.category.id ?? defaultCategory, categoriesListObj);
                    olxPracaSchema.Add(obj);

                    await Task.Delay(Constants.delayBetweenRequests);
                }

                return (JsonConvert.SerializeObject(olxPracaSchema, Formatting.Indented) ?? "", html);
            }
            catch (Exception)
            {

                
            }
            return (null, null);
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        private OlxPracaCategory GetOlxPracaCategoryById(int id, string categoryJson)
        {
            string modifiedJson = JsonParser.MakeListFromObject(categoryJson);
            var categoriesList = JsonConvert.DeserializeObject<List<OlxPracaCategory>>(modifiedJson);
            return categoriesList?.Where(_ => _.id == id).FirstOrDefault() ?? new OlxPracaCategory();
        }
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
        private OlxPracaOfferDetails GetOlxPracaDetailsObject(JToken token) => token.ToObject<OlxPracaOfferDetails>() ?? new OlxPracaOfferDetails();
        private List<JToken> GetOffersJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("listing" +
                ".listing" +
                ".ads[*]");
            return offerListJs;
        }
        private List<JToken> GetOfferDetailsJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("categories.list");
            return offerListJs;
        }
    }
}
