using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.Json;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.PracujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        public PracujplScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async Task<(string, string, List<string>)> GetOfferAsync(string url = "")
        {
            List<string>errors = new List<string>();
            string baseUrl = PracujPlUrlBuilder.baseUrl;
            if (url != "")
                baseUrl = url;
            string html;

            try
            {
                html = await GetHtmlSource(baseUrl);
            }
            catch (Exception ex)
            {
                errors.Add($"Error while downloading HTML: {ex.Message}");
                return (string.Empty, string.Empty, errors);
            }


            string allJs = GetAllJson(html);
            maxOfferCount = GetOfferCount(allJs);
            List<JToken> offerListJs = GetOffersJToken(allJs).ToList(); // always 50 offers 


            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();
            List<string> requirementsData = new List<string>();
            foreach (JToken offer in offerListJs)
            {
                try
                {
                    PracujplSchema schemaOffer = OfferMapper.DeserializeJToken<PracujplSchema>(offer);

                    Offer? offerObject = schemaOffer.offers?.FirstOrDefault();
                    if (offerObject != null)
                    {
                        try
                        {
                            schemaOffer.details = OfferMapper.DeserializeJToken<PracujPlOfferDetails>(
                                await GetOfferDetails(offerObject.offerAbsoluteUri)
                            );
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error while scrapping offer details {offerObject.offerAbsoluteUri}: {ex.Message}");
                        }
                    }
                    if (schemaOffer.companyProfileAbsoluteUri != null)
                    {
                        try
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
                        catch (Exception p)
                        {
                            errors.Add($"Error while scrapping company/profile details {schemaOffer.companyProfileAbsoluteUri}: {p.Message}");
                        }
                       
                    }
                    bool? isAbroad = schemaOffer.details?.attributes?.workplaces?
                    .Any(_ => _.isAbroad.GetValueOrDefault());

                    if (!isAbroad ?? true)
                        pracujplSchemas.Add(schemaOffer);
                    requirementsData.Add(String.Join(";", schemaOffer.details?.sections?.Where(_ => _.sectionType.Contains("requirements"))?.FirstOrDefault()?.subSections?.FirstOrDefault()?.model?.bullets ?? new List<string>()));
                    await Task.Delay(Constants.delayBetweenRequests);
                }
                catch (Exception e)
                {
                    errors.Add($"Error while processing offer from main list: {e.Message}");
                }
                
            }

            return (JsonConvert.SerializeObject(pracujplSchemas, Formatting.Indented) ?? "", html, errors);
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
