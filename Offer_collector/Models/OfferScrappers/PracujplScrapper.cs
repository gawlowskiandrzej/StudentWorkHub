using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.AI;
using Offer_collector.Models.Json;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        
        public override async Task<(string, string)> GetOfferAsync(string url = "")
        {
            AiApi aiApi = new AiApi();

            string baseUrl = PracujPlUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            List<JToken> offerListJs = GetOffersJToken(allJs).Take(1).ToList();
            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();
            List<string> requirementsData = new List<string>();
            foreach (JToken offer in offerListJs)
            {
                try
                {
                    PracujplSchema schemaOffer = OfferMapper.DeserializeJToken<PracujplSchema>(offer);

                    Offer? offerObiect = schemaOffer.offers?.FirstOrDefault();
                    if (offerObiect != null)
                        schemaOffer.details = OfferMapper.DeserializeJToken<PracujPlOfferDetails>(await GetOfferDetails(offerObiect.offerAbsoluteUri));

                    if (schemaOffer.companyProfileAbsoluteUri != null)
                    {
                        var token = await CompanyCache.GetOrAddAsync(
                            schemaOffer.companyProfileAbsoluteUri,
                            () => GetCompanyDetails(schemaOffer.companyProfileAbsoluteUri)
                        );

                        if (token?.SelectToken("slug") != null)
                            schemaOffer.profile = OfferMapper.DeserializeJToken<PracujPlProfile>(token);
                        else
                            schemaOffer.company = OfferMapper.DeserializeJToken<PracujPlCompany>(token);
                    }

                    // TODO parsing skills by AI 


                    pracujplSchemas.Add(schemaOffer);
                    requirementsData.Add(String.Join(";", schemaOffer.details?.sections.Where(_ => _.sectionType.Contains("requirements"))?.FirstOrDefault()?.subSections?.FirstOrDefault()?.model?.bullets ?? new List<string>()));
                    await Task.Delay(Constants.delayBetweenRequests);
                }
                catch (Exception e)
                {
                    
                }
                
            }

            return (JsonConvert.SerializeObject(pracujplSchemas, Formatting.Indented) ?? "", html);
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
