namespace Offer_collector.Models.UrlBuilders
{
    internal class OlxPracaUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.olx.pl/praca";
        public OlxPracaUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            return $"{baseUrl}/q-magazynier";
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["page"] = pageId.ToString();
            }

            return parameters;
        }
    }
}
