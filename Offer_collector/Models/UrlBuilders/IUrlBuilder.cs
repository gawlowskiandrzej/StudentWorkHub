namespace Offer_collector.Models.UrlBuilders
{
    public interface IUrlBuilder
    {
        string BuildUrl(SearchFilters searchFilters, int pageId = 1);
    }
}
