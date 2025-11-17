
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Offer_collector.Models.UrlBuilders;

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
        public async Task<(string, List<string>, List<string>)> FetchAllOffersAsync(SearchFilters searchFilters,int maxPages = 5)
        {
            var allOffers = new List<JToken>();
            int currentPage = 1;
            int totalPages = 1;
            bool hasMore = true;
            List<string> htmls = new List<string>();
            List<string> errors = new List<string>();

            while (hasMore && currentPage <= maxPages && allOffers.Count <= _offerMaxCount)
            {
                string url = _urlBuilder.BuildUrl(searchFilters, pageId: currentPage);

                int retryCount = 0;
                const int maxRetries = 3;
                bool success = false;

                while (!success && retryCount < maxRetries)
                {
                    try
                    {
                        
                        var (offersJson, htmlRaw, scrappingErrors) = await _scrapper.GetOfferAsync(url);
                        errors.AddRange(scrappingErrors);
                        // wyciągamy dane ofert
                        var offers = ExtractOffers(offersJson);

                        if (offers.Count == 0)
                        {
                            hasMore = false;
                            break;
                        }

                        allOffers.AddRange(offers);
                        htmls.Add(htmlRaw);

                        // przy pierwszej stronie ustal całkowitą liczbę stron
                        if (currentPage == 1)
                        {
                            int totalOffers = _scrapper.GetOfferCountFromHtml();
                            int perPage = offers.Count;
                            totalPages = (int)Math.Ceiling((double)totalOffers / perPage);
                            _offerMaxCount = (int)totalOffers * (_offerCountPercentage / 100);
                        }

                        success = true;
                    }
                    catch (HttpRequestException ex)
                    {
                        retryCount++;
                        errors.Add($"Error while fetching page {currentPage}: {ex.Message}. Try {retryCount}/{maxRetries}");
                        await Task.Delay(Constants.delayBetweenRequests * retryCount); // exponential backoff
                    }
                    catch (JsonException ex)
                    {
                        errors.Add($"Error parsing offer on page {currentPage}: {ex.Message}");
                        break; // nie ma sensu retry, jeśli JSON jest niepoprawny
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Processing error on page {currentPage}: {ex.Message}");
                        break;
                    }
                }

                currentPage++;
                hasMore = currentPage <= totalPages;
                await Task.Delay(Constants.delayBetweenRequests);
            }

            return (JsonConvert.SerializeObject(allOffers, Formatting.Indented), htmls, errors);
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
