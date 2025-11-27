using HtmlAgilityPack;
using Newtonsoft.Json;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;
using System.Net;

namespace Offer_collector.Models.OfferScrappers
{
    internal class AplikujplScrapper : BaseHtmlScraper
    {
        string _offerListHtml = "";

        public AplikujplScrapper(ClientType clientType) : base(clientType)
        {
        }

        public override async IAsyncEnumerable<(string, string, List<string>)> GetOfferAsync(string url = "", int batchSize = 5)
        {
            List<string> errors = new List<string>();
            try
            {
                _offerListHtml = await GetHtmlSource(url);
            }
            catch (Exception e)
            {
                errors.Add($"Error while downloading HTML from url: {url}: {e.Message}");
                yield break;
            }
            await foreach (var (offers, errors2) in GetOfferList(batchSize))
            {
                yield return (JsonConvert.SerializeObject(offers, Formatting.Indented) ?? "", _offerListHtml, offers.Where(o => o.errorMessages != null && o.errorMessages.Any()).Select(o => string.Join('\n', o.errorMessages)).ToList());
            }

        }
        private async Task<string> GetHtmlSource(string url) => await GetHtmlAsync(url);
        async IAsyncEnumerable<(List<AplikujplSchema>, List<string>)> GetOfferList(int bathSize)
        {
            List<AplikujplSchema> offerList = new List<AplikujplSchema>();
            List<string> errors = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(_offerListHtml);

            var maxOfferNode = doc.DocumentNode
                .SelectSingleNode("//div[contains(@class,'hidden sm:flex sm:flex-1 sm:items-center sm:justify-between')]/div/p/span[3]");

            if (maxOfferNode != null && int.TryParse(maxOfferNode.InnerText, out int maxOffers))
            {
                maxOfferCount = maxOffers;
            }
            else
            {
                errors.Add($"Cant get max number of offers.");
            }

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-list\"]");
            if (node == null)
            {
                errors.Add("Cant get list of offers on site");
                yield break;
            }
            int i = 1;
            offersPerPage = node.SelectNodes(".//li[contains(concat(' ', normalize-space(@class), ' '), ' offer-card ')]")?.Count ?? 0;
            foreach (HtmlNode offerNode in node.SelectNodes(".//li[contains(concat(' ', normalize-space(@class), ' '), ' offer-card ')]") ?? Enumerable.Empty<HtmlNode>())
            {
                try
                {
                    AplikujplSchema ap = new AplikujplSchema();
                    OfferListHeader header = GetHeader(offerNode);

                    if (header != null)
                    {
                        ap.header = header;

                        //if (ConstValues.PolishCities.Any(_ => _.City == ap.header.location))
                        //{
                        string detailsUrl = header.link;
                        string detailsHtml = "";
                        try
                        {
                            detailsHtml = await GetHtmlSource(detailsUrl);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Cant get details of offer {detailsUrl}: {ex.Message}");

                        }

                        if (!string.IsNullOrEmpty(detailsHtml))
                        {
                            try
                            {
                                ap.details = GetOfferDetails(detailsHtml);
                                offerList.Add(ap);
                            }
                            catch (Exception ex)
                            {
                                errors.Add($"Error while parsing details of offer {detailsUrl}: {ex.Message}");
                            }
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Error while processing offer {ex.Message}");
                }
                if (i++ >= bathSize)
                {
                    yield return (offerList, errors);
                    offerList = new List<AplikujplSchema>();
                    errors = new List<string>();
                    i = 1;
                }
            }

            if (offerList.Count > 0)
                yield return (offerList, errors);
        }
        OfferListHeader GetHeader(HtmlNode node)
        {
            var header = new OfferListHeader();

            //TODO fix nulls

            header.title = node.SelectSingleNode(".//a[@class='offer-title']")?.InnerText.Trim() ?? "";

            header.link = node.SelectSingleNode(".//a[@class='offer-title']")?.GetAttributeValue("href", "") ?? "";

            header.company = node.SelectSingleNode(".//div[@class='text-sm']/a")?.InnerText.Trim() ?? "";

            header.location = node.SelectSingleNode(".//li[contains(@class,'workPlace')]/span")?.InnerText.Trim() ?? "";

            header.employmentType = node.SelectSingleNode(".//li[contains(@class,'employmentType')]/span")?.InnerText.Trim() ?? "";

            header.companyLogoUrl = node.SelectSingleNode(".//div[@class='offer-card-thumb']//img")?.GetAttributeValue("src", "") ?? "";

            header.dateAdded = node.SelectSingleNode(".//time")?.InnerText.Trim() ?? "";

            header.recomended = node.SelectSingleNode(".//span[contains(@class,'offer-badge')]") != null;

            header.salary = node.SelectSingleNode(".//span[contains(@class,'offer-salary')]")?.InnerText.Trim() ?? "";
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
                string publishRaw = publishNode.GetAttributeValue("datetime", "");
                if (DateTime.TryParse(publishRaw, out var publishDate))
                    date.publishionDate = publishDate;
            }

            if (dateCollection.ElementAt(1) != null)
            {
                HtmlNode expireDate = dateCollection.ElementAt(1);
                string expireRaw = expireDate.GetAttributeValue("datetime", "");
                if (DateTime.TryParse(expireRaw, out var expirationDate))
                    date.expirationDate = expirationDate;
            }

            return date;
        }
        AplikujPl.Company GetCompany(HtmlNode topDeatailHeader)
        {
            HtmlNode companyNode = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'text-md lg:text-base')]");
            AplikujPl.Company company = new AplikujPl.Company();
            var companyLinkNode = companyNode?.SelectSingleNode(".//a");

            if (companyLinkNode != null)
            {
                company.company = companyLinkNode.InnerText.Trim();
                company.companyLink = companyNode?.GetAttributeValue("href", "") ?? "";
            }
            else
            {
                company.company = companyNode?.InnerText.Trim();
            }

            company.companyLogo = topDeatailHeader.SelectSingleNode(".//div[contains(@class,'mr-2 mt-2 sm:mr-8 sm:mt-0')]//img")?.GetAttributeValue("src", "") ?? "";

            return company;
        }
        OfferDetails GetOfferDetails(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"offer-container\"]");

            OfferDetails det = new OfferDetails();
            det.dates = GetDates(node);
            det.company = GetCompany(node);
            HtmlNode detailsSection = node.SelectSingleNode(".//div[contains(@class, 'pt-6')]");
            HtmlNode informationSection = node.SelectSingleNode(".//div[contains(@class, 'pt-8')]");
            HtmlNodeCollection skillsSections = node.SelectNodes(".//div[contains(@class, 'pb-4 sm:pb-12')]");

            HtmlNode? salarySection = node.SelectSingleNode(".//div[contains(@class, 'flex bg-gray-100 rounded-lg py-1 lg:py-2.5 px-2 lg:px-4 mt-4')]");

            det.responsibilities = GetFeature(skillsSections?.FirstOrDefault());
            if (det.responsibilities.Count == 0) ;
            if (skillsSections?.Count > 1)
                det.requirements = GetFeature(skillsSections?.ElementAt(1));
            if (skillsSections?.Count > 2)
                det.benefits = GetFeature(skillsSections?.ElementAt(2));
            det.localization = GetLocalization(informationSection, doc.DocumentNode);
            if (salarySection != null)
                det.salary = GetSalary(salarySection);
            det.infoFeatures = GetInfofeature(node);
            if (skillsSections == null)
                det.infoFeatures.description = node.SelectSingleNode(".//div[contains(@class, 'leading-8 text-lg')]")?.InnerText.ToString() ?? "";
            det.category = informationSection.SelectSingleNode(".//div//div[2]//div//div[2]//p")?.InnerText.Trim() ?? "";

            return det;

        }

        private InfoFeatures GetInfofeature(HtmlNode document)
        {
            HtmlNode? infoSection = document.SelectSingleNode(".//div[contains(@class, 'grid grid-col-1 sm:grid-cols-2 gap-x-8 gap-y-3 py-3 md:pb-8 offer__label-wrapper')]");
            HtmlNodeCollection? descriptionSection = document.SelectNodes(".//div[contains(@class, 'leading-6 py-6')]");
            InfoFeatures features = new InfoFeatures();
            string typeofContract = infoSection.SelectSingleNode(".//div//div[2]//p")?.InnerText.Trim() ?? "";
            string typeofWork = infoSection.SelectSingleNode(".//div[2]//div[2]//p")?.InnerText.Trim() ?? "";

            features.typeofContract = typeofContract;
            features.typeofWork = typeofWork;

            features.isRemote = false;
            foreach (HtmlNode node in infoSection.SelectNodes(".//div[contains(@class, 'flex-1')]"))
            {
                string? content = node.SelectSingleNode(".//p")?.InnerText.Trim();
                if (content != null && content.Contains("Praca zdalna"))
                    features.isRemote = true;
                if (content != null && content.Contains("Запрошуємо працівників з України"))
                    features.isforUkrainians = true;
                if (content != null && content.Contains("Praca od zaraz"))
                    features.isUrgent = true;
            }
            if (descriptionSection != null)
                features.description = WebUtility.HtmlDecode(string.Join("", descriptionSection.Select(n => n.InnerHtml)).Trim());

            return features;
        }
        AplikujPl.Salary GetSalary(HtmlNode node)
        {
            AplikujPl.Salary salaryObj = new AplikujPl.Salary();
            HtmlNode salaryBlock = node.SelectSingleNode(".//ul//li//div");
            string typeofContract = salaryBlock.SelectSingleNode(".//span").InnerText.Trim();
            string salary = salaryBlock.SelectSingleNode(".//div//span").InnerText.Trim();

            return SalaryParser.Parse(salary);
        }
        Localization GetLocalization(HtmlNode? node, HtmlNode? allNodes)
        {
            Localization loc = new Localization();
            HtmlNode? tempNode = node?.SelectSingleNode(".//li[contains(@class, 'pt-1')]");
            string localizatinoString = tempNode?.SelectSingleNode(".//div[2]//span//span").InnerText.Trim() ?? "";
            string[] splitted = localizatinoString.Split(',');
            if (splitted.Length > 1)
                loc.city = splitted[0];
            else
                loc.city = localizatinoString;
            return loc;
        }
        List<string> GetFeature(HtmlNode? node)
        {
            List<string> featureList = new List<string>();
            HtmlNodeCollection? skills = node?.SelectNodes(".//li");
            HtmlNodeCollection? skills2 = node?.SelectNodes(".//p");

            if (skills != null)
            {
                foreach (HtmlNode respo in skills)
                {
                    string text = respo.SelectNodes(".//div").ElementAt(1)?.InnerText.Trim() ?? "";
                    featureList.Add(WebUtility.HtmlDecode(text));
                }
                return featureList;
            }
            else if (skills2 != null && skills == null)
            {
                foreach (HtmlNode respo in skills2.Skip(1))
                {
                    string text = respo.InnerText.Trim() ?? "";
                    featureList.Add(WebUtility.HtmlDecode(text));
                }
                return featureList;
            }
            return new List<string>();
        }

    }
}
