
using HtmlAgilityPack;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.UrlBuilders;
using System.Xml.Linq;

namespace Offer_collector.Models.OfferScrappers
{
    internal class AplikujplScrapper : BaseHtmlScraper
    {
        string? _offerListHtml;
        public override async Task<(string, string)> GetOfferAsync(string url = "")
        {
            _offerListHtml = await GetHtmlSource(url);
            await GetOfferList();
           

            throw new NotImplementedException();
        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        async Task<List<AplikujplSchema>> GetOfferList()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(_offerListHtml);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-list\"]");
            List<OfferListHeader> offerListHeader = new List<OfferListHeader>();

            foreach (HtmlNode offerNode in node.SelectNodes(".//li[contains(concat(' ', normalize-space(@class), ' '), ' offer-card ')]"))
            {
                OfferListHeader header = GetHeader(offerNode);
                if (header != null)
                    offerListHeader.Add(header);

                string detailsUrl = AplikujPlUrlBuilder.baseUrl + header.link;
                GetOfferDetails(await GetHtmlSource(detailsUrl));
            }



            return null;
        }
        OfferListHeader GetHeader(HtmlNode node)
        {
            var header = new OfferListHeader();

            //TODO fix nulls

            header.title = node.SelectSingleNode(".//a[@class='offer-title']")?.InnerText.Trim();

            header.link = node.SelectSingleNode(".//a[@class='offer-title']")?.GetAttributeValue("href", null);

            header.company = node.SelectSingleNode(".//div[@class='text-sm']/a")?.InnerText.Trim();

            header.location = node.SelectSingleNode(".//li[contains(@class,'workPlace')]/span")?.InnerText.Trim();

            header.employmentType = node.SelectSingleNode(".//li[contains(@class,'employmentType')]/span")?.InnerText.Trim();

            header.companyLogoUrl = node.SelectSingleNode(".//div[@class='offer-card-thumb']//img")?.GetAttributeValue("src", null);

            header.dateAdded = node.SelectSingleNode(".//time")?.InnerText.Trim();

            header.recomended = node.SelectSingleNode(".//span[contains(@class,'offer-badge')]") != null;

            header.salary = node.SelectSingleNode(".//span[contains(@class,'offer-salary')]")?.InnerText.Trim();
            header.remoteOption = node.SelectSingleNode(".//span[contains(@class,'offer-card-labels-list-item--remoteWork')]") != null;

            return header;
        }
        AplikujPl.Dates GetDates(HtmlNode node)
        {
            AplikujPl.Dates date = new AplikujPl.Dates();
            HtmlNodeCollection dateCollection = node.SelectNodes(".//div[contains(@class,'offer__dates')]//time");
            if (dateCollection.First() != null)
            {
                HtmlNode publishNode = dateCollection.First();
                string publishRaw = publishNode.GetAttributeValue("datetime", null);
                if (DateTime.TryParse(publishRaw, out var publishDate))
                    date.publishionDate = publishDate;
            }


            if (dateCollection.ElementAt(1) != null)
            {
                HtmlNode expireDate = dateCollection.ElementAt(1);
                string expireRaw = expireDate.GetAttributeValue("datetime", null);
                if (DateTime.TryParse(expireRaw, out var expirationDate))
                    date.expirationDate = expirationDate;
            }

            return date;
        }
        AplikujPl.Company GetCompany(HtmlNode topDeatailHeader)
        {
            HtmlNode companyNode = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'text-md lg:text-base')]//a");
            AplikujPl.Company company = new AplikujPl.Company();
            company.company = companyNode.InnerText.Trim();
            company.companyLink = companyNode.GetAttributeValue("href", "");
            company.companyLogo = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'mr-2 mt-2 sm:mr-8 sm:mt-0')]//img").GetAttributeValue("src", "");

            return company;
        }
        OfferDetails GetOfferDetails(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-container\"]");

            OfferDetails det =  new OfferDetails();
            det.dates = GetDates(node);
            det.company = GetCompany(node);
            det.responsibilities = GetResponsibilities(node.SelectSingleNode(".//div[contains(@class, 'pt-6')]"));



            return det;
        }

        List<string> GetResponsibilities(HtmlNode node)
        {
            HtmlNode respoNode = node.SelectSingleNode(".//div[contains(@class, 'pb-4 sm:pb-12')]");
            List<string> respoList = new List<string>();
            foreach (HtmlNode respo in respoNode.SelectNodes(".//li[contains(@class, 'leading-6 flex py-1')]"))
            {
                respoList.Add(respo.SelectNodes(".//div").ElementAt(1)?.InnerText.Trim() ?? "");
            }
            return respoList;
        }
    }
}
