using Newtonsoft.Json;
using Offer_collector.Interfaces;
using Offer_collector.Models;
using Offer_collector.Models.AplikujPl;
using Offer_collector.Models.JustJoinIt;
using Offer_collector.Models.OfferFetchers;
using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.OlxPraca;
using Offer_collector.Models.Tools;
using Offer_collector.Models.UrlBuilders;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using worker.Models.Constants;
using worker.Models.Tools;

namespace worker.Models
{
    internal class Fetcher
    {
        private readonly OfferSitesTypes _offerSitesTypes;
        private readonly Offer_collector.Models.ClientType _clientType;
        private readonly bool _saveToLocalFile;

        public bool IsUsingAi { get; }

        public BaseHtmlScraper? scrapper;
        BaseUrlBuilder? urlBuilder;
        PaginationModule? paginationModule;

        public Fetcher(OfferSitesTypes offerSitesTypes, Offer_collector.Models.ClientType clientType = Offer_collector.Models.ClientType.httpClient, bool saveToLocalFile = false)
        {
            _offerSitesTypes = offerSitesTypes;
            _clientType = clientType;
            _saveToLocalFile = saveToLocalFile;

            InitScrappers();
            InitPaginationModule();
        }

        private void InitScrappers()
        {
            switch (_offerSitesTypes)
            {
                case OfferSitesTypes.Pracujpl:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(_offerSitesTypes, Offer_collector.Models.ClientType.headlessBrowser);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(_offerSitesTypes);
                    break;
                case OfferSitesTypes.Justjoinit:
                    scrapper = (JustJoinItScrapper)FactoryScrapper.CreateScrapper(_offerSitesTypes, _clientType);
                    urlBuilder = (JustJoinItBuilder)UrlBuilderFactory.Create(_offerSitesTypes);
                    break;
                case OfferSitesTypes.Olxpraca:
                    scrapper = (OlxpracaScrapper)FactoryScrapper.CreateScrapper(_offerSitesTypes, _clientType);
                    urlBuilder = (OlxPracaUrlBuilder)UrlBuilderFactory.Create(_offerSitesTypes);
                    break;
                case OfferSitesTypes.Aplikujpl:
                    scrapper = (AplikujplScrapper)FactoryScrapper.CreateScrapper(_offerSitesTypes, _clientType);
                    urlBuilder = (AplikujPlUrlBuilder)UrlBuilderFactory.Create(_offerSitesTypes);
                    break;
                default:
                    scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(_offerSitesTypes, _clientType);
                    urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(_offerSitesTypes);
                    break;
            }
        }
        private void InitPaginationModule()
        {
            if (scrapper != null && urlBuilder != null)
            {
                paginationModule = new PaginationModule(scrapper, urlBuilder);
            }
        }

        public async IAsyncEnumerable<(List<UnifiedOfferSchemaClass> offers, List<string> errors)> FetchOffers(
            SearchFilters searchFilter,
            IDatabase redisDB,
            [EnumeratorCancellation]CancellationToken cancellationToken,
            int offset = 0,
            int bathSize = 5)
        {
            if (paginationModule != null)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    List<string> errrosAll = new List<string>();
                    List<UnifiedOfferSchemaClass> allOffers = new List<UnifiedOfferSchemaClass>();
                    await foreach (var (offersJson, htmls, errors) in paginationModule.FetchAllOffersAsync(searchFilter, redisDB, cancellationToken, offset: offset, bathSize: bathSize))
                    {
                        List<UnifiedOfferSchemaClass> unifiedOfferSchemastemp = new List<UnifiedOfferSchemaClass>();
                        switch (_offerSitesTypes)
                        {
                            case OfferSitesTypes.Pracujpl:
                                ProcessOffers<PracujplSchema>(offersJson, allOffers, unifiedOfferSchemastemp);
                                break;

                            case OfferSitesTypes.Olxpraca:
                                ProcessOffers<OlxPracaSchema>(offersJson, allOffers, unifiedOfferSchemastemp);
                                break;

                            case OfferSitesTypes.Justjoinit:
                                ProcessOffers<JustJoinItSchema>(offersJson, allOffers, unifiedOfferSchemastemp);
                                break;

                            case OfferSitesTypes.Aplikujpl:
                                ProcessOffers<AplikujplSchema>(offersJson, allOffers, unifiedOfferSchemastemp);
                                break;

                            default:
                                break;
                        }
                        yield return (unifiedOfferSchemastemp, errors);
                    }
                }
            }
        }
        private void ProcessOffers<T>(string offersJson, List<UnifiedOfferSchemaClass> allOffers, List<UnifiedOfferSchemaClass> unifiedOfferSchemasTemp)
    where T : IUnificatable, new()
        {

            var schemas = OfferMapper.DeserializeJson<List<T>>(offersJson);

            foreach (var offer in schemas)
            {
                if (offer is null) continue;
                UnifiedOfferSchemaClass unified = OfferMapper.ToUnifiedSchema<T>(offer);
                allOffers.Add(unified);
                unifiedOfferSchemasTemp.Add(unified);
            }
        }
    }

}
