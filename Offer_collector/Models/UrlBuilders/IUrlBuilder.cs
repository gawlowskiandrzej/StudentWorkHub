namespace Offer_collector.Models.UrlBuilders
{
    public interface IUrlBuilder
    {
        string BuildUrl(Dictionary<string, string> parameters = null, int pageId = 1, Dictionary<string, string> tags = null);
    }
}
