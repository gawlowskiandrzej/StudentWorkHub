
using HtmlAgilityPack;
using Offer_collector.Models.AplikujPl;

namespace Offer_collector.Models.OfferScrappers
{
    internal class AplikujplScrapper : BaseHtmlScraper
    {
        string? _offerListHtml;
        public override Task<(string, string)> GetOfferAsync(string url = "")
        {
            GetOfferList();
           

            throw new NotImplementedException();
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        List<AplikujplSchema> GetOfferList()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(_offerListHtml);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-list\"]");
            List<OfferListHeader> offerListHeader = new List<OfferListHeader>();
            foreach (HtmlNode offerNode in node.ChildNodes)
            {
                OfferListHeader header = GetHeader(offerNode);
                offerListHeader.Add(header);
            }
            


            return null;
        }
        OfferListHeader GetHeader(HtmlNode node)
        {


            return null;
        }
    }
}
