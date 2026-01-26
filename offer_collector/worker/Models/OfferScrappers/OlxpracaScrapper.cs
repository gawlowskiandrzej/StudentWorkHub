using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.UrlBuilders;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using worker.Models.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Offer_collector.Models.OfferFetchers
{
    internal class OlxpracaScrapper : BaseHtmlScraper
    {
        const int defaultCategory = 1;

        public OlxpracaScrapper(ClientType clientType) : base(clientType)
        {
        }

        //public override async IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(string url = "", int batchSize = 5, int offset= 0)
        //{
        //    string baseUrl = OlxPracaUrlBuilder.baseUrl;
        //    List<string> errors = new List<string>();

        //    if (!string.IsNullOrEmpty(url))
        //        baseUrl = url;

        //    string html = string.Empty;
        //    try
        //    {
        //        html = await GetHtmlSource(baseUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Failed to load main HTML from '{baseUrl}': {ex.Message}");
        //        yield break;
        //    }

        //    string allJs;
        //    try
        //    {
        //        allJs = GetAllJson(html);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Failed to extract JSON data from the HTML: {ex.Message}");
        //        yield break;
        //    }

        //    //int maxOfferCount = 0;
        //    try
        //    {

        //        maxOfferCount = GetOfferCount(allJs);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Failed to read offer count: {ex.Message}");
        //    }

        //    List<JToken> offerListJs = new List<JToken>();
        //    try
        //    {
        //        offerListJs = GetOffersJson(allJs);
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Failed to parse offers JSON: {ex.Message}");
        //    }

        //    List<OlxPracaSchema> olxPracaSchema = new List<OlxPracaSchema>();
        //    int i = 1;
        //    int skipped = 0;
        //    int skippedOffersCount = batchSize * offset;
        //    offersPerPage = offerListJs.Count;
        //    foreach (JToken offer in offerListJs)
        //    {
        //        try
        //        {
        //            if (skipped < skippedOffersCount)
        //            {
        //                skipped++;
        //                continue;
        //            }
        //            OlxPracaSchema obj = GetOlxPracaObject(offer);

        //            try
        //            {
        //                obj.htmlOfferDetail = await GetHtmlSource(obj.url);
        //            }
        //            catch (Exception ex)
        //            {
        //                errors.Add($"Failed to load offer details for URL '{obj.url}': {ex.Message}");
        //                continue; // Skip to next offer
        //            }

        //            try
        //            {
        //                string htmlOfferDetail = GetSubstringJson(obj.htmlOfferDetail);
        //                List<JToken> offerDetails = GetOfferDetailsJson(htmlOfferDetail);
        //                string categoriesListObj = JsonConvert.SerializeObject(offerDetails);
        //                obj.category.categoryDetails = GetOlxPracaCategoryById(obj.category.id ?? defaultCategory, categoriesListObj);
        //            }
        //            catch (Exception ex)
        //            {
        //                errors.Add($"Failed to parse details or categories for offer '{obj.url}': {ex.Message}");
        //            }

        //            olxPracaSchema.Add(obj);

        //            await Task.Delay(ConstValues.delayBetweenRequests);
        //        }
        //        catch (Exception ex)
        //        {
        //            errors.Add($"Unexpected error while processing an offer: {ex.Message}");
        //        }

        //        if (i++ >= batchSize)
        //        {
        //            yield return (JsonConvert.SerializeObject(olxPracaSchema, Formatting.Indented) ?? "", html, errors);
        //            olxPracaSchema = new List<OlxPracaSchema>();
        //            errors = new List<string>();
        //            i = 1;
        //        }
        //    }
        //    if (olxPracaSchema.Count > 0)
        //        yield return (JsonConvert.SerializeObject(olxPracaSchema, Formatting.Indented) ?? "", html, errors);
        //}
        public override async IAsyncEnumerable<(string, string, List<string>)>
    GetOfferAsync(IDatabase redisDB, [EnumeratorCancellation] CancellationToken cancellationToken,string url = "", int batchSize = 5, int offset = 0)
        {
            var (mainHtml, offers, initialErrors) = await LoadMainPageAsync(url);

            if (offers.Count == 0)
            {
                yield return ("", mainHtml, initialErrors);
                yield break;
            }

            offersPerPage = offers.Count;

            var remaining = offers.Skip(batchSize * offset).ToList();

            var batch = new List<Task<(OlxPracaSchema, List<string>)>>();

            foreach (var offerToken in remaining)
            {
                cancellationToken.ThrowIfCancellationRequested();
                List<string> errors = new List<string>();
                if (!TryParseSchema(offerToken, out var schema, errors))
                    continue;

                if (await redisDB.SetContainsAsync("offers:db:index", schema.url))
                    continue;

                batch.Add(ProcessOfferAsync(schema, errors));

                if (batch.Count == batchSize)
                {
                    yield return await RunBatchAsync(batch, mainHtml);
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
                yield return await RunBatchAsync(batch, mainHtml);
            else
                yield return ("", mainHtml, initialErrors);
        }

        private async Task<(string Html, List<JToken> Offers, List<string> Errors)>
    LoadMainPageAsync(string url)
        {
            var errors = new List<string>();
            string baseUrl = string.IsNullOrEmpty(url) ? OlxPracaUrlBuilder.baseUrl : url;

            string html = "";
            try
            {
                html = await GetHtmlSource(baseUrl);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load main HTML '{baseUrl}': {ex.Message}");
                return (html, new(), errors);
            }

            string jsonBlock;
            try
            {
                jsonBlock = GetAllJson(html);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to extract JSON: {ex.Message}");
                return (html, new(), errors);
            }

            try
            {
                maxOfferCount = GetOfferCount(jsonBlock);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to read offer count: {ex.Message}");
            }

            List<JToken> offers;
            try
            {
                offers = GetOffersJson(jsonBlock);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse offers JSON: {ex.Message}");
                return (html, new(), errors);
            }

            return (html, offers, errors);
        }

        private async Task<(string Json, string Html, List<string> Errors)>
    RunBatchAsync(List<Task<(OlxPracaSchema schema, List<string> errors)>> tasks, string html)
        {
            var results = await Task.WhenAll(tasks);

            var items = results.Where(r => r.schema != null).Select(_ => _.schema).ToList();
            var errors = results.SelectMany(r => r.errors).ToList();

            string json = JsonConvert.SerializeObject(items, Formatting.Indented) ?? "";

            return (json, html, errors);
        }

        private async Task<(OlxPracaSchema schema, List<string> errors)> ProcessOfferAsync(OlxPracaSchema schema, List<string> errors)
        {
            // Load details HTML
            if (!await TryLoadOfferHtml(schema, errors))
                return (schema, errors);

            // Parse details and categories
            TryParseDetails(schema, errors);

            return (schema, errors);
        }

        private bool TryParseSchema(JToken token, out OlxPracaSchema schema, List<string> errors)
        {
            schema = new OlxPracaSchema();
            try
            {
                schema = GetOlxPracaObject(token);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse schema: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TryLoadOfferHtml(OlxPracaSchema schema, List<string> errors)
        {
            try
            {
                schema.htmlOfferDetail = await GetHtmlSource(schema.url);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load offer HTML '{schema.url}': {ex.Message}");
                return false;
            }
        }

        private void TryParseDetails(OlxPracaSchema schema, List<string> errors)
        {
            try
            {
                string jsonPart = GetSubstringJson(schema.htmlOfferDetail);
                var details = GetOfferDetailsJson(jsonPart);

                string serialized = JsonConvert.SerializeObject(details);

                schema.category.categoryDetails =
                    GetOlxPracaCategoryById(schema.category.id ?? defaultCategory, serialized);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse offer details '{schema.url}': {ex.Message}");
            }
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
