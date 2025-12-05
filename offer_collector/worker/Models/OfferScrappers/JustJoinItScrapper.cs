using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;
using worker.Models.Constants;

namespace Offer_collector.Models.OfferFetchers
{
    internal class JustJoinItScrapper : BaseHtmlScraper
    {
        public JustJoinItScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async IAsyncEnumerable<(string, string, List<string>)>
    GetOfferAsync(string url = "", int batchSize = 5, int offset = 0)
        {
            // Load main HTML + extract offers
            var (mainHtml, offers, initErrors) = await LoadMainPageAsync(url);

            if (offers.Count == 0)
            {
                yield return ("", mainHtml, initErrors);
                yield break;
            }

            // Skip previous batches
            var remainingOffers = offers.Skip(offset * batchSize).ToList();

            // Process in batches
            var batch = new List<Task<(JustJoinItSchema, List<string>)>>();

            foreach (var offerToken in remainingOffers)
            {
                batch.Add(ProcessOfferAsync(offerToken));

                if (batch.Count == batchSize)
                {
                    yield return await RunBatchAsync(batch, mainHtml);
                    batch.Clear();
                }
            }

            // last batch
            if (batch.Count > 0)
            {
                yield return await RunBatchAsync(batch, mainHtml);
            }
        }

        private async Task<(string Html, List<JToken> Offers, List<string> Errors)>
    LoadMainPageAsync(string url)
        {
            var errors = new List<string>();
            string baseUrl = string.IsNullOrEmpty(url) ? JustJoinItBuilder.baseUrl : url;

            string html = "";
            try
            {
                html = await GetHtmlSource(baseUrl);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load base HTML: {ex.Message}");
                return (html, new List<JToken>(), errors);
            }

            string jsonBlock;
            try
            {
                jsonBlock = GetAllJson(html);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to extract JSON block: {ex.Message}");
                return (html, new List<JToken>(), errors);
            }

            List<JToken> offers;
            try
            {
                maxOfferCount = GetOfferCount(jsonBlock);
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to read offer count: {ex.Message}");
            }
            try
            {
                offers = GetOffersJson(jsonBlock);
                offersPerPage = offers.Count;
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse offers JSON: {ex.Message}");
                return (html, new List<JToken>(), errors);
            }

            return (html, offers, errors);
        }

        private async Task<(string Json, string Html, List<string> Errors)>
    RunBatchAsync(List<Task<(JustJoinItSchema schema, List<string> errors)>> batch, string html)
        {
            var results = await Task.WhenAll(batch);

            var offers = results.Where(r => r.schema != null).Select(_ => _.schema).ToList();
            var errors = results.SelectMany(r => r.errors).ToList();

            string json = JsonConvert.SerializeObject(offers, Formatting.Indented) ?? "";

            return (json, html, errors);
        }

        private async Task<(JustJoinItSchema, List<string>)> ProcessOfferAsync(JToken offerToken)
        {
            List<string> errors = new List<string>();
            // Parse schema
            if (!TryParseSchema(offerToken, out var schema, errors))
                return (schema, errors);

            // Load offer page
            if (!await TryLoadDetailsHtml(schema, errors))
                return (schema, errors);

            // Parse details JSON
            TryParseDetails(schema, errors);

            // Parse description JSON
            TryParseDescription(schema, errors);

            return (schema, errors);
        }

        private bool TryParseSchema(JToken token, out JustJoinItSchema schema, List<string> errors)
        {
            schema = new JustJoinItSchema();
            try
            {
                schema = GetJustJoinItSchema(token);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse schema: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TryLoadDetailsHtml(JustJoinItSchema schema, List<string> errors)
        {
            try
            {
                schema.detailsHtml =
                    await GetHtmlSource(JustJoinItBuilder.baseUrlOfferDetail + schema.slug);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to load details HTML for '{schema.slug}': {ex.Message}");
                return false;
            }
        }

        private void TryParseDetails(JustJoinItSchema schema, List<string> errors)
        {
            try
            {
                var token = GetCompanyDetails(schema.detailsHtml);
                schema.details = GetJustJoinItOfferDetails(token ?? "");
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse company details for '{schema.slug}': {ex.Message}");
            }
        }

        private void TryParseDescription(JustJoinItSchema schema, List<string> errors)
        {
            try
            {
                var json = GetDescriptionSubString(schema.detailsHtml) ?? "";
                schema.description =
                    JsonConvert.DeserializeObject<JustJoinItDescription>(json)?.description ?? "";
            }
            catch (Exception ex)
            {
                errors.Add($"Failed to parse description for '{schema.slug}': {ex.Message}");
            }
        }

        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);

        private string GetAllJson(string html) => GetSubstringJson(html) ?? "";
        private string? GetSubstringJson(string htmlSource)
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

            var json = htmlSource.Substring(startIndex, endIndex - startIndex - 6).Trim();
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
        private string? GetDescriptionSubString(string htmlSource) => GetJsonFragment(htmlSource, @"<script[^>]*type=['""]application/ld\+json['""][^>]*>(.*?)</script>");
        
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
        JustJoinItOfferDetails GetJustJoinItOfferDetails(JToken token)
        {
            try
            {
                return token.ToObject<JustJoinItOfferDetails>() ?? new JustJoinItOfferDetails();
            }
            catch (Exception)
            {
                return new JustJoinItOfferDetails();
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
        private JToken? GetOfferDetailsJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerDetails = parser.GetSpecificJsonFragment("state" +
                ".queries[0]" +
                ".state" +
                ".data");
            return offerDetails;
        }
        private int GetOfferCount(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerCount = parser.GetSpecificJsonFragment("state" +
                ".queries[2]" +
                ".state" +
                ".data" +
                ".count");
            return int.Parse(JsonConvert.SerializeObject(offerCount));
        }
        JToken? GetCompanyDetails(string html)
        {

            string allJs = GetSubstringJson(html) ?? "";

            return GetOfferDetailsJson(allJs);//GetCompanyJToken(allJs);
        }
    }
}
