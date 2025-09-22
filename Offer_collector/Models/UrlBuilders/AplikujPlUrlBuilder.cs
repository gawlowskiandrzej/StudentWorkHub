
namespace Offer_collector.Models.UrlBuilders
{
    internal class AplikujPlUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.aplikuj.pl";
        public AplikujPlUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            return $"{baseUrl}/praca";
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
            parameters["keyword"] = "magazynier";
            return base.BuildQuery(parameters, tags);
        }
    }
}
