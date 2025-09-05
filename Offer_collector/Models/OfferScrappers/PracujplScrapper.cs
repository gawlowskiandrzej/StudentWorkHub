using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        
        public override async Task<string> GetOfferAsync(string url = "")
        {
            string baseUrl = PracujPlUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJToken(allJs).Take(3).ToList();
            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();
            foreach (JToken offer in offerListJs)
            {
                PracujplSchema schemaOffer = OfferMapper.DeserializeJToken<PracujplSchema>(offer);
                pracujplSchemas.Add(schemaOffer);
                Offer? offerObiect = schemaOffer.offers.FirstOrDefault();
                if (offerObiect != null)
                    schemaOffer.details = OfferMapper.DeserializeJToken<PracujPlOfferDetails>(await GetOfferDetails(offerObiect.offerAbsoluteUri));

                if (schemaOffer.companyProfileAbsoluteUri != null)
                {
                    JToken? token = await GetCompanyDetails(schemaOffer.companyProfileAbsoluteUri);
                    // TODO cast to profile or company
                    // map fields to company 
                    if (token?.SelectToken("slug") != null)
                       schemaOffer.profile = OfferMapper.DeserializeJToken<PracujPlProfile>(token);
                    else
                        schemaOffer.company = OfferMapper.DeserializeJToken<PracujPlCompany>(token);
                }
                await Task.Delay(500);
            }

            return JsonConvert.SerializeObject(pracujplSchemas, Formatting.Indented) ?? "";
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
        private async Task<JToken?> GetOfferDetails(string url)
        {
            string htmlSource =  await GetHtmlSource(url);
            string json = GetAllJson(htmlSource);
            JToken? offerJToken = GetOfferDetailJson(json);
            return offerJToken;
        }
    }
}
