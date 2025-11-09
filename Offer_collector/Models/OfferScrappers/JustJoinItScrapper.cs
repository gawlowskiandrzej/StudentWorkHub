using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.ConstData;
using Offer_collector.Models.Json;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.UrlBuilders;
using System.Text.Json;

namespace Offer_collector.Models.OfferFetchers
{
    internal class JustJoinItScrapper : BaseHtmlScraper
    {
        public JustJoinItScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async Task<(string, string)> GetOfferAsync(string url = "")
        {
            string baseUrl = JustJoinItBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);
            maxOfferCount = GetOfferCount(allJs);
            List<JToken> offerListJs = GetOffersJson(allJs);
            #if DEBUG
                var listJs = JsonConvert.SerializeObject(offerListJs, Formatting.Indented);
            #endif

            List<JustJoinItSchema> justJoinItOffers = new List<JustJoinItSchema>();
           
            foreach (JToken offer in offerListJs)
            {
                JustJoinItSchema schema = GetJustJoinItSchema(offer);
               
                

                string detailsHtml = await GetHtmlSource(JustJoinItBuilder.baseUrlOfferDetail + schema.slug);
                JToken? detailsToken = GetCompanyDetails(detailsHtml);
                schema.details = GetJustJoinItOfferDetails(detailsToken ?? "");
                schema.detailsHtml = detailsHtml;
                schema.description = JsonConvert.DeserializeObject<JustJoinItDescription>(GetDescriptionSubString(detailsHtml) ?? "")?.description ?? "";

                if ((bool)schema.multilocation.Any(m => ConstValues.PolishCities.Any(p => p.City == m.city)))
                    justJoinItOffers.Add(schema);

                await Task.Delay(Constants.delayBetweenRequests);
            }

            return (JsonConvert.SerializeObject(justJoinItOffers, Formatting.Indented) ?? "", html);
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
