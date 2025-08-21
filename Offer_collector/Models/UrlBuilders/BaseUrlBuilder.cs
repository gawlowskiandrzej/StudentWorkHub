namespace Offer_collector.Models.UrlBuilders
{
    abstract class BaseUrlBuilder : IUrlBuilder
    {
        protected string BaseUrl { get; }

        protected BaseUrlBuilder(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
        public string BuildUrl(Dictionary<string, string> parameters = null, int pageId = 1, Dictionary<string, string> tags = null)
        {
            parameters ??= new Dictionary<string, string>();
            tags ??= new Dictionary<string, string>();

            parameters = AddPaging(parameters, pageId);

            var url = BuildBaseUrl(parameters, tags);

            var query = BuildQuery(parameters, tags);

            return string.IsNullOrEmpty(query) ? url : $"{url}?{query}";
        }

        protected abstract string BuildBaseUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags);

        protected virtual string BuildQuery(Dictionary<string, string> parameters, Dictionary<string, string> tags)
        {
            var allParams = new List<string>();

            foreach (var param in parameters)
            {
                if (!string.IsNullOrEmpty(param.Value))
                    allParams.Add($"{param.Key}={Uri.EscapeDataString(param.Value)}");
            }

            foreach (var tag in tags)
            {
                if (!string.IsNullOrEmpty(tag.Value))
                    allParams.Add($"{tag.Key}={Uri.EscapeDataString(tag.Value)}");
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
