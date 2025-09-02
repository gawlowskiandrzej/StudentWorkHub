namespace Offer_collector.Models.UrlBuilders
{
    internal class JoobleUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://pl.jooble.org/SearchResult";
        public JoobleUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            return $"{baseUrl}";
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                //parameters["pn"] = pageId.ToString();
            }

            return parameters;
        }
        protected override string BuildQuery(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            parameters["ukw"] = "programista%20python";
            return base.BuildQuery(parameters, tags);
        }
    }
}
