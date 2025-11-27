using LLMParser;
using Newtonsoft.Json;
using OffersConnector;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace Offer_collector.Models.DatabaseService
{
    internal class DBService
    {
        private readonly DatabaseSettings _settings;
        private readonly PgConnector connector;

        public DBService()
        {
            // --- Wczytaj konfigurację z appsettings.json ---
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _settings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()
                        ?? new DatabaseSettings();
            connector = new PgConnector(
                   _settings.Username,
                   _settings.Password,
                   _settings.Host,
                   _settings.Port,
                   _settings.Database
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
