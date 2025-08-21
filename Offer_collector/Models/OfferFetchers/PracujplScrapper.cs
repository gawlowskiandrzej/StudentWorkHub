using Newtonsoft.Json.Linq;
using System;

namespace Offer_collector.Models.OfferFetchers
{
    internal class PracujplScrapper : BaseHtmlScraper
    {
        string baseUrl = "https://it.pracuj.pl/praca/programista%20python;kw?pn=1";
        public override async Task<string> GetOfferAsync(string parameters = "", string url = "")
        {
            if (url != "")
                baseUrl = url;
            string html = await GetHtmlSource(baseUrl);
            string allJs = GetAllJson(html);

            JsonParser parser = new JsonParser(allJs);
            List<JToken> offerListJs = parser.GetSpecificJsonFragments("");
            List<PracujplSchema> pracujplSchemas = new List<PracujplSchema>();

            foreach (JToken offer in offerListJs)
            {
                GetPracujplObject(offer);
            }

            return null;
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(baseUrl);
        private string GetAllJson(string html) => GetJsonFragment(html, "<script id=\"__NEXT_DATA__\" type=\"application/json\">(.*?)</script>");

        private PracujplSchema GetPracujplObject(JToken token) => new PracujplSchema(token);
    }
}
