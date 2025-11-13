namespace Offer_collector.Models.UrlBuilders
{
    internal class PracujPlUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://it.pracuj.pl/praca";
        public PracujPlUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            return $"{baseUrl}/informatyka;kw";
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["pn"] = pageId.ToString();
            }

            return parameters;
        }
    }
}
