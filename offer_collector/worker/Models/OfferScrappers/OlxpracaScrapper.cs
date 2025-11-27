using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;
using worker.Models.Constants;

namespace Offer_collector.Models.OfferFetchers
{
    internal class OlxpracaScrapper : BaseHtmlScraper
    {
        const int defaultCategory = 1;

        public OlxpracaScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(string url = "", int batchSize = 5)
        {
            string baseUrl = OlxPracaUrlBuilder.baseUrl;
            List<string> errors = new List<string>();

            if (!string.IsNullOrEmpty(url))
                baseUrl = url;

            string html = string.Empty;
            try
            {
                html = await GetHtmlSource(baseUrl);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load main HTML from '{baseUrl}': {ex.Message}");
                yield break;
            }

            string allJs;
            try
            {
                allJs = GetAllJson(html);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to extract JSON data from the HTML: {ex.Message}");
                yield break;
            }

            //int maxOfferCount = 0;
            try
            {

                maxOfferCount = GetOfferCount(allJs);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to read offer count: {ex.Message}");
            }

            List<JToken> offerListJs = new List<JToken>();
            try
            {
                offerListJs = GetOffersJson(allJs);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse offers JSON: {ex.Message}");
            }

            List<OlxPracaSchema> olxPracaSchema = new List<OlxPracaSchema>();
            int i = 1;
            offersPerPage = offerListJs.Count;
            foreach (JToken offer in offerListJs)
            {
                try
                {
                    OlxPracaSchema obj = GetOlxPracaObject(offer);

                    try
                    {
                        obj.htmlOfferDetail = await GetHtmlSource(obj.url);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to load offer details for URL '{obj.url}': {ex.Message}");
                        continue; // Skip to next offer
                    }

                    try
                    {
                        string htmlOfferDetail = GetSubstringJson(obj.htmlOfferDetail);
                        List<JToken> offerDetails = GetOfferDetailsJson(htmlOfferDetail);
                        string categoriesListObj = JsonConvert.SerializeObject(offerDetails);
                        obj.category.categoryDetails = GetOlxPracaCategoryById(obj.category.id ?? defaultCategory, categoriesListObj);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to parse details or categories for offer '{obj.url}': {ex.Message}");
                    }

                    olxPracaSchema.Add(obj);

                    await Task.Delay(ConstValues.delayBetweenRequests);
                }
                catch (Exception ex)
                {
                    errors.Add($"Unexpected error while processing an offer: {ex.Message}");
                }

                if (i++ >= batchSize)
                {
                    yield return (JsonConvert.SerializeObject(olxPracaSchema, Formatting.Indented) ?? "", html, errors);
                    olxPracaSchema = new List<OlxPracaSchema>();
                    errors = new List<string>();
                    i = 1;
                }
            }
            if (olxPracaSchema.Count > 0)
                yield return (JsonConvert.SerializeObject(olxPracaSchema, Formatting.Indented) ?? "", html, errors);
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
            if (startIndex == -1) return "";

            startIndex = htmlSource.IndexOf('{', startIndex);
            if (startIndex == -1) return "";

            var endIndex = htmlSource.IndexOf(endMarker, startIndex);
            if (endIndex == -1) return "";

            endIndex += endMarker.Length;

            var json = htmlSource.Substring(startIndex - 1, endIndex - startIndex).Trim();

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
        private int GetOfferCount(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerCount = parser.GetSpecificJsonFragment("listing" +
                ".listing" +
                ".totalElements");
            
            return int.Parse(JsonConvert.SerializeObject(offerCount));
        }
        private List<JToken> GetOfferDetailsJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("categories.list");
            return offerListJs;
        }
    }
}
