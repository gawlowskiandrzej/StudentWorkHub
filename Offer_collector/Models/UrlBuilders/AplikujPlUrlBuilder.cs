
using System;

namespace Offer_collector.Models.UrlBuilders
{
    internal class AplikujPlUrlBuilder : BaseUrlBuilder
    {
        public static string baseUrl = "https://www.aplikuj.pl";
        public AplikujPlUrlBuilder() : base(baseUrl) { }
        protected override string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            string url = $"{baseUrl}/praca";
            if (parameters.TryGetValue("page", out var pageValue) && int.TryParse(pageValue, out int pageNumber) && pageNumber > 1)
            {
                url += $"/strona-{pageNumber}";
            }

            return url;
        }
        protected override Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 0)
            {
                parameters["page"] = pageId.ToString();
            }

            return parameters;
        }
        protected override string BuildQuery(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            parameters["keyword"] = "bioinformatyka";
            return base.BuildQuery(parameters, tags);
        }
    }
}
