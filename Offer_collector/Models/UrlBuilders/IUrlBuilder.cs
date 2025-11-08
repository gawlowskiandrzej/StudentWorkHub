namespace Offer_collector.Models.UrlBuilders
{
    public interface IUrlBuilder
    {
        string BuildUrl(Dictionary<string, string> parameters, Dictionary<string, string> tags, int pageId = 1);
    }
}
