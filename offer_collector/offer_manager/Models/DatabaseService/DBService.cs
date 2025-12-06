using LLMParser;
using Newtonsoft.Json;
using Offer_collector.Models.UrlBuilders;
using offer_manager.Interfaces;
using OffersConnector;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace Offer_collector.Models.DatabaseService
{
    public class DBService : IDatabaseService
    {
        private readonly PgConnector connector;

        public DBService(string username, string password, string host, int port, string database)
        {
            connector = new PgConnector(
                   username,password, host,port, database   
               );

        }

        /// <summary>
        /// Dodaje listę ofert do bazy danych.
        /// </summary>
        public async Task<(bool, List<string>)> AddOffersToDatabaseAsync(List<string> aiOffers)
        {
            List<string> errors = new List<string>();   
            if (aiOffers == null || aiOffers.Count == 0)
            {
                errors.Add("No offers to add to database");
                return (false, errors);
            }

            try
            {
                // --- 1. Utwórz listę ofert UOS ---
                List<UOS> offersList = new List<UOS>();
                foreach (var offerStr in aiOffers)
                {
                    try
                    {
                        var uosOffer = UOSUtils.BuildFromString(offerStr);
                        offersList.Add(uosOffer);
                    }
                    catch (Exception)
                    {
                        errors.Add($"Failed to deserialize offer: {offerStr}");
                    }
                   
                }
                
               
                FrozenDictionary<int, BatchResult> batchResults =
                    await connector.AddExternalOffersBatch(offersList);

                for (int i = 0; i < batchResults.Values.Length; i++)
                {
                    if (!string.IsNullOrEmpty(batchResults.Values.ElementAt(i).Error))
                        errors.Add($"{JsonConvert.SerializeObject(new { offer = offersList.ElementAt(i), error = batchResults.Values.ElementAt(i).Error }, Formatting.Indented)}");
                }

                return (true, errors);
            }
            catch (Exception ex)
            {
                errors.Add($"Unexpected error while adding offers to database: {ex.Message}");
                return (false, errors);
            }
        }

        public async Task<FrozenSet<UOS?>> GetOffers(SearchFilters searchFilters)
        {
            try
            {
                var uosOffers = await connector.GetExternalOffers(
                search_text: searchFilters.Keyword,
                leadingCategory: searchFilters.Category,
                salaryFrom: searchFilters.SalaryFrom,
                salaryTo: searchFilters.SalaryTo,
                locationCity: searchFilters.Localization
                );
                return uosOffers;
            }
            catch (Exception e)
            {
                return FrozenSet<UOS?>.Empty;
            }
            
        }

        /// <summary>
        /// Pobiera restrykcje z bazy i przygotowuje je do promptów systemowych.
        /// </summary>
        public async Task<Dictionary<string, string>> GetSystemPromptParamsAsync()
        {
            List<string> restrictions = await connector.GetRestrictions();
            string joinedRestrictions = string.Join("\n", restrictions);

            return new Dictionary<string, string>
            {
                { "ROLE", Prompts.descriptionInformationExtratorRole },
                { "RULES", Prompts.descriptionInformationExtratorRules },
                { "TASK", Prompts.descriptionInformationExtratorTask },
                { "RESTRICTIONS", joinedRestrictions }
            };
        }
    }
}
