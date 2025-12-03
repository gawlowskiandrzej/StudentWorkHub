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

        public override async IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(string url = "", int batchSize = 5, int offset = 0)
        {
            string baseUrl = JustJoinItBuilder.baseUrl;
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

            int maxOfferCount = 0;
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

#if DEBUG
            var listJs = JsonConvert.SerializeObject(offerListJs, Formatting.Indented);
#endif
            offersPerPage = offerListJs.Count;
            List<JustJoinItSchema> justJoinItOffers = new List<JustJoinItSchema>();
            int i = 1;
            int skipped = 0;
            int skippedOffersCount = batchSize * offset;
            foreach (JToken offer in offerListJs)
            {
                try
                {
                    if (skipped < skippedOffersCount)
                    {
                        skipped++;
                        continue;
                    }
                    JustJoinItSchema schema = GetJustJoinItSchema(offer);

                    string detailsHtml = string.Empty;
                    try
                    {
                        detailsHtml = await GetHtmlSource(JustJoinItBuilder.baseUrlOfferDetail + schema.slug);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to load offer details for slug '{schema.slug}': {ex.Message}");
                        continue;
                    }

                    JToken? detailsToken = null;
                    try
                    {
                        detailsToken = GetCompanyDetails(detailsHtml);
                        schema.details = GetJustJoinItOfferDetails(detailsToken ?? "");
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to parse company details for slug '{schema.slug}': {ex.Message}");
                    }

                    schema.detailsHtml = detailsHtml;

                    try
                    {
                        schema.description = JsonConvert.DeserializeObject<JustJoinItDescription>(
                            GetDescriptionSubString(detailsHtml) ?? ""
                        )?.description ?? "";
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Failed to parse offer description for slug '{schema.slug}': {ex.Message}");
                    }

                    justJoinItOffers.Add(schema);

                    await Task.Delay(ConstValues.delayBetweenRequests);
                }
                catch (Exception ex)
                {
                    errors.Add($"Unexpected error while processing an offer: {ex.Message}");
                }
                if (i++ >= batchSize)
                {
                    yield return (JsonConvert.SerializeObject(justJoinItOffers, Formatting.Indented) ?? "", html, new List<string>(errors));
                    justJoinItOffers = new List<JustJoinItSchema>();
                    errors = new List<string>();
                    i = 1;
                }
            }
            if (justJoinItOffers.Count > 0)
                yield return (JsonConvert.SerializeObject(justJoinItOffers, Formatting.Indented) ?? "", html, new List<string>(errors));
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
