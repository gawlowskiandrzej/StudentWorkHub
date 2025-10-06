
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
        public async Task<(string, List<string>)> FetchAllOffersAsync(int maxPages = int.MaxValue)
        {
            var allOffers = new List<JToken>();
            int currentPage = 1;
            int totalPages = 1;
            bool hasMore = true;
            List<string> htmls = new List<string>();
            while (hasMore && currentPage <= maxPages && allOffers.Count <= _offerMaxCount)
            {
                string url = _urlBuilder.BuildUrl(pageId: currentPage);
                var (offersJson, htmlRaw) = await _scrapper.GetOfferAsync(url);

                // wyciągamy dane ofert (możesz to zmienić na swój model)
                var offers = ExtractOffers(offersJson);
                
                if (offers.Count == 0)
                    break;

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

                currentPage++;
                hasMore = currentPage <= totalPages;
                await Task.Delay(Constants.delayBetweenRequests);
            }

            return (JsonConvert.SerializeObject(allOffers, Formatting.Indented), htmls);
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
