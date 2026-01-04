using LLMParser;
using Newtonsoft.Json;
using offer_manager.Interfaces;
using offer_manager.Models.Dictionaries;
using OffersConnector;
using shared_models.Dto;
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
        public async Task<(bool, IEnumerable<long?>,List<string>)> AddOffersToDatabaseAsync(List<string> aiOffers)
        {
            List<string> errors = new List<string>();   
            if (aiOffers == null || aiOffers.Count == 0)
            {
                errors.Add("No offers to add to database");
                return (false, new List<long?> { } ,errors);
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

                return (true , batchResults.Values.Select(_ => _.OfferId),errors);
            }
            catch (Exception ex)
            {
                errors.Add($"Unexpected error while adding offers to database: {ex.Message}");
                return (false, new List<long?> { }, errors);
            }
        }
        public async Task<IEnumerable<string>> GetUrlHashes()
        {
            try
            {
                FrozenSet<string> urls = await connector.GetExternalOfferUrlsAsync();
                return urls;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public async Task<FrozenSet<UOS?>> GetOffers(SearchDto searchFilters)
        {
            try
            {
                var uosOffers = await connector.GetExternalOffers(
                search_text: searchFilters.Keyword,
                leadingCategory: searchFilters.Category,
                salaryFrom: searchFilters.SalaryFrom != null ? decimal.Parse(searchFilters.SalaryFrom) : null,
                salaryTo: searchFilters.SalaryTo != null ? decimal.Parse(searchFilters.SalaryTo) : null,
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

        public async Task<DictionariesDto> GetDictionaries(List<string> dicNames)
        {
            try
            {
                var dictionaries = await connector.GetSimpleLookups(dicNames);
                return new DictionariesDto
                {
                    EmploymentSchedules = dictionaries[dicNames[0]].Select(_ => _.Value).ToList(),
                    EmploymentType = dictionaries[dicNames[1]].Select(_ => _.Value).ToList(),
                    SalaryPeriods = dictionaries[dicNames[2]].Select(_ => _.Value).ToList()
                };
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
