using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.PracujPl;
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

            List<JToken> offerListJs = GetOffersJToken(allJs);
            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();

            foreach (JToken offer in offerListJs)
            {
                PracujplSchema schemaOffer = GetPracujplOfferObject(offer);
                pracujplSchemas.Add(schemaOffer);
                //Offer? offerObiect = schemaOffer.offers.FirstOrDefault();
                //if (offerObiect != null)
                //    GetOfferDetails(offerObiect.offerAbsoluteUri);
                PracujPlCompany? pracujCompany;
                if (schemaOffer.companyProfileAbsoluteUri != null)
                   pracujCompany = GetPracujplCompanyObject(await GetCompanyDetails(schemaOffer.companyProfileAbsoluteUri));


            }

            return JsonConvert.SerializeObject(offerListJs, Formatting.Indented) ?? "";
        }
        async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        string GetAllJson(string html) => GetJsonFragment(html, "<script id=\"__NEXT_DATA__\" type=\"application/json\">(.*?)</script>");
        string GetCompanyJson(string html) => GetJsonFragment(html, @"<script id=""__NEXT_DATA__"" type=""application/json""(?: nonce=""[^""]*"")?\s*>\s*({.*?})\s*</script>");

        PracujplSchema GetPracujplOfferObject(JToken token) => token.ToObject<PracujplSchema>() ?? new PracujplSchema();
        PracujPlCompany GetPracujplCompanyObject(JToken? token) => token?.ToObject<PracujPlCompany>() ?? new PracujPlCompany();
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
            return offerListJs;
        }
        JToken? GetCompanyJToken(string allJson)
        {
            JsonParser parser = new JsonParser(allJson);
            JToken? offerCompany = parser.GetSpecificJsonFragment("props" +
                ".pageProps" +
                ".data" +
                ".companyData");
            return offerCompany;
        }
        async Task<JToken?> GetCompanyDetails(string url)
        {
            string html = await GetHtmlSource(url);
            string allJs = GetCompanyJson(html);

            return GetCompanyJToken(allJs);
        }
        // TODO take hourly money from details in offer
        //private JToken GetOfferDetailJson(string allJson)
        //{
        //    JsonParser parser = new JsonParser(allJson);
        //    List<JToken> offerListJs = parser.GetSpecificJsonFragments("props" +
        //        ".pageProps" +
        //        ".dehydratedState" +
        //        ".queries[0]" +
        //        ".state" +
        //        ".data" +
        //        ".groupedOffers[*]");
        //    return offerListJs;
        //}
        //private async Task<JToken> GetOfferDetails(string url)
        //{
        //    string htmlSource =  await GetHtmlSource(url);
        //    string json = GetAllJson(htmlSource);


        //    return null;
        //}
    }
}
