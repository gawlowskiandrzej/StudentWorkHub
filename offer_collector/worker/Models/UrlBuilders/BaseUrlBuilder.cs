namespace Offer_collector.Models.UrlBuilders
{
    abstract class BaseUrlBuilder : IUrlBuilder
    {
        protected string BaseUrl { get; }

        protected BaseUrlBuilder(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
        public string BuildUrl(SearchFilters searchFilters, int pageId = 1)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters = AddPaging(parameters, pageId);

            var url = BuildBaseUrl(searchFilters,parameters);

            var query = BuildQuery(parameters);

            return string.IsNullOrEmpty(query) ? url : $"{url}?{query}";
        }

        protected abstract string BuildBaseUrl(SearchFilters searchFilters,Dictionary<string, string> parameters);

        protected virtual string BuildQuery(Dictionary<string, string> parameters)
        {
            var allParams = new List<string>();

            foreach (var param in parameters)
            {
                if (!string.IsNullOrEmpty(param.Value))
                    allParams.Add($"{param.Key}={Uri.EscapeDataString(param.Value)}");
            }

            return string.Join("&", allParams);
        }

        protected virtual Dictionary<string, string> AddPaging(Dictionary<string, string> parameters, int pageId)
        {
            if (pageId > 1)
            {
                parameters["page"] = pageId.ToString();
            }
            return parameters;
        }

    }
}
