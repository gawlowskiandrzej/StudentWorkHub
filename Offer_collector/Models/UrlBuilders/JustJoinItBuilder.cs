namespace Offer_collector.Models.UrlBuilders
{
    internal class JustJoinItBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://justjoin.it/job-offers/all-locations";
        public JustJoinItBuilder() : base(baseUrl) { }
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
            parameters["keyword"] = "programista python";
            return base.BuildQuery(parameters, tags);
        }
    }
}
