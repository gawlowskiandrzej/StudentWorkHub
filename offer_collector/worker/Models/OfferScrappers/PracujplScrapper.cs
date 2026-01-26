using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using worker.Models.Constants;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        public PracujplScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(IDatabase redisDB, [EnumeratorCancellation] CancellationToken cancellationToken,
    string url = "",
    int batchSize = 5,
    int offset = 0)
        {
            string baseUrl = string.IsNullOrEmpty(url) ? PracujPlUrlBuilder.baseUrl : url;

            string htmlBody;
            try
            {
                htmlBody = await GetHtmlSource(baseUrl);
            }
            catch (Exception ex)
            {
                yield break;
            }

            string allJs = GetAllJson(htmlBody);
            maxOfferCount = GetOfferCount(allJs);
            List<JToken> offers = GetOffersJToken(allJs).ToList();
            offersPerPage = offers.Count;
            int skip = batchSize * offset;

            var batchTasks = new List<Task<(PracujplSchema?, List<string>)>>();

            foreach (var token in offers.Skip(skip))
            {
                cancellationToken.ThrowIfCancellationRequested();
                List<string> errors = new List<string>();

                var schema = OfferMapper.DeserializeJToken<PracujplSchema>(token);
                if (await redisDB.SetContainsAsync("offers:db:index", schema.offers?.FirstOrDefault()?.offerAbsoluteUri))
                    continue;

                batchTasks.Add(ProcessOfferAsync(schema, errors));

                if (batchTasks.Count >= batchSize)
                {
                    yield return await ProcessBatchAsync(batchTasks, htmlBody);
                    batchTasks.Clear();
                }
            }

            if (batchTasks.Count > 0)
                yield return await ProcessBatchAsync(batchTasks, htmlBody);
            else
                yield return (JsonConvert.SerializeObject(new List<PracujplSchema>(), Formatting.Indented), htmlBody, new List<string>());
        }
        private async Task<(PracujplSchema?, List<string>)> ProcessOfferAsync(PracujplSchema schema, List<string> errors)
        {
            try
            {
                await FetchOfferDetailsAsync(schema, errors);
                await FetchCompanyProfileAsync(schema, errors);

                await Task.Delay(ConstValues.delayBetweenRequests);
            }
            catch (Exception ex)
            {
                errors.Add($"General error: {ex.Message}");
            }

            return (schema, errors);
        }
        private async Task FetchOfferDetailsAsync(PracujplSchema schema, List<string> errors)
        {
            var offer = schema.offers?.FirstOrDefault();
            if (offer == null) return;

            try
            {
                var json = await GetOfferDetails(offer.offerAbsoluteUri);
                schema.details = OfferMapper.DeserializeJToken<PracujPlOfferDetails>(json);
            }
            catch (Exception ex)
            {
                errors.Add($"Error fetching offer details {offer.offerAbsoluteUri}: {ex.Message}");
            }
        }
        private async Task FetchCompanyProfileAsync(PracujplSchema schema, List<string> errors)
        {
            if (schema.companyProfileAbsoluteUri == null)
                return;

            try
            {
                var token = await CompanyCache.GetOrAddAsync(
                    schema.companyProfileAbsoluteUri,
                    () => GetCompanyDetails(schema.companyProfileAbsoluteUri)
                );

                if (token?.SelectToken("slug") != null)
                    schema.profile = OfferMapper.DeserializeJToken<PracujPlProfile>(token);
                else
                    schema.company = OfferMapper.DeserializeJToken<PracujPlCompany>(token);
            }
            catch (Exception ex)
            {
                errors.Add($"Error fetching company/profile {schema.companyProfileAbsoluteUri}: {ex.Message}");
            }
        }
        private async Task<(string, string, List<string>)> ProcessBatchAsync(
    List<Task<(PracujplSchema?, List<string>)>> batch,
    string htmlBody)
        {
            var results = await Task.WhenAll(batch);

            var schemas = results
                .Where(r => r.Item1 != null)
                .Select(r => r.Item1!)
                .ToList();

            var errors = results
                .SelectMany(r => r.Item2)
                .ToList();

            return (
                JsonConvert.SerializeObject(schemas, Formatting.Indented),
                htmlBody,
                errors
            );
        }

        async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        string GetAllJson(string html) => GetJsonFragment(html, "<script id=\"__NEXT_DATA__\" type=\"application/json\">(.*?)</script>");
        string GetCompanyJson(string html) => GetJsonFragment(html, @"<script id=""__NEXT_DATA__"" type=""application/json""(?: nonce=""[^""]*"")?\s*>\s*({.*?})\s*</script>");

        List<JToken> GetOffersJToken(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("props" +
                ".pageProps" +
                ".dehydratedState" +
                ".queries[0]" +
                ".state" +
                ".data" +
                ".groupedOffers[*]");

            var stringg = JsonConvert.SerializeObject(offerListJs, Formatting.Indented);
            return offerListJs;
        }
        int GetOfferCount(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerListJs = parser.GetSpecificJsonFragment("props" +
                ".pageProps" +
                ".dehydratedState" +
                ".queries[0]" +
                ".state" +
                ".data" +
                ".offersTotalCount");

            var stringg = JsonConvert.SerializeObject(offerListJs) ?? "0";
            return int.Parse(stringg);
        }
        JToken? GetCompanyJToken(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerCompany = parser.GetSpecificJsonFragment("props" +
                ".pageProps" +
                ".data" +
                ".companyData");
            if (offerCompany == null)
                offerCompany = GetProfileJToken(allJson);
            
            return offerCompany;
        }
        JToken? GetProfileJToken(string json)
        {
            JsonParser parser = new JsonParser(json);
            JToken? offerProfile = parser.GetSpecificJsonFragment("props" +
                ".pageProps" +
                ".data" +
                ".profileData");
            return offerProfile;
        }
        async Task<JToken?> GetCompanyDetails(string url)
        {
            string html = await GetHtmlSource(url);
            string allJs = GetCompanyJson(html);

            return GetCompanyJToken(allJs);
        }
        private async Task<JToken?> GetOfferDetails(string url)
        {
            string htmlSource = await GetHtmlSource(url);
            string json = GetAllJson(htmlSource);
            JToken? offerJToken = GetOfferDetailJson(json);
            return offerJToken;
        }
        // TODO take hourly money from details in offer
        private JToken? GetOfferDetailJson(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerDetails = parser.GetSpecificJsonFragment("props" +
                ".pageProps" +
                ".dehydratedState" +
                ".queries[0]" +
                ".state" +
                ".data");
            return offerDetails;
        }
    }
}
