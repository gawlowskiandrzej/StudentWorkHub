
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.UrlBuilders;
using worker.Models.Constants;

namespace Offer_collector.Models.Tools
{
    internal class PaginationModule
    {
        private readonly BaseHtmlScraper _scrapper;
        private readonly IUrlBuilder _urlBuilder;
        private readonly int _offerCountPercentage;
        private int _offerMaxCount;

        public PaginationModule(BaseHtmlScraper scrapper, IUrlBuilder urlBuilder, int offerCountPercentage = 100)
        {
            _scrapper = scrapper;
            _urlBuilder = urlBuilder;
            _offerCountPercentage = offerCountPercentage;
            _offerMaxCount = int.MaxValue;
        }

        /// <summary>
        /// Pobiera wszystkie oferty z paginowanego źródła i zwraca je jako JSON.
        /// </summary>
        public async IAsyncEnumerable<(string, List<string>, List<string>)> FetchAllOffersAsync(SearchFilters searchFilters,int maxPages = 5, int bathSize = 5)
        {
            var allOffers = new List<JToken>();
            int currentPage = 1;
            int totalPages = 1;
            bool hasMore = true;
            List<string> htmls = new List<string>();
            List<string> errors = new List<string>();
            int i = 0;
            while (hasMore && currentPage <= maxPages && allOffers.Count <= _offerMaxCount)
            {
                string url = _urlBuilder.BuildUrl(searchFilters, pageId: currentPage);

                int retryCount = 0;
                const int maxRetries = 3;
                bool success = false;

                while (!success && retryCount < maxRetries)
                {

                    await foreach (var (batchJson, htmlRaw, scrappingErrors) in _scrapper.GetOfferAsync(url, bathSize))
                    {
                        try
                        {
                            errors.AddRange(scrappingErrors);
                            var offers = ExtractOffers(batchJson);

                            if (offers.Count == 0)
                            {
                                hasMore = false;
                                yield break;
                            }
                            allOffers.AddRange(offers);
                            htmls.Add(htmlRaw);


                            if (i == 0)
                            {
                                int totalOffers = _scrapper.GetOfferCountFromHtml();
                                totalPages = (int)Math.Ceiling((double)totalOffers / _scrapper.GetOffersPerPage());
                                _offerMaxCount = (int)totalOffers * (_offerCountPercentage / 100);
                            }
                            
                            success = true;
                        }
                        catch (HttpRequestException ex)
                        {
                            retryCount++;
                            errors.Add($"Error while fetching page {currentPage}: {ex.Message}. Try {retryCount}/{maxRetries}");
                            await Task.Delay(ConstValues.delayBetweenRequests * retryCount);
                        }
                        catch (JsonException ex)
                        {
                            errors.Add($"Error parsing offer on page {currentPage}: {ex.Message}");
                            break;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Processing error on page {currentPage}: {ex.Message}");
                            break;
                        }
                        yield return (batchJson, new List<string>(htmls), new List<string>(errors));

                        if (allOffers.Count >= _offerMaxCount)
                            yield break;
                        i++;
                    }
                }

                currentPage++;
                hasMore = currentPage <= totalPages;
                await Task.Delay(ConstValues.delayBetweenRequests);
            }
           // yield return (JsonConvert.SerializeObject(allOffers, Formatting.Indented), new List<string>(htmls), new List<string>(errors));
        }


        private List<JToken> ExtractOffers(string offersJson)
        {
            try
            {
                // Jeśli masz w JSON-ie tablicę ofert
                return JArray.Parse(offersJson).ToList();
            }
            catch
            {
                // Możesz dostosować do konkretnego formatu JSON-a
                return new List<JToken>();
            }
        }
    }
}
