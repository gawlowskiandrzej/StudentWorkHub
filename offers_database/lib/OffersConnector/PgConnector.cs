using Npgsql;
using NpgsqlTypes;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Data;
using System.Text.Json;
using UnifiedOfferSchema;

namespace OffersConnector
{
    public sealed class PgConnector : IAsyncDisposable
    {
        private static int OFFER_EXPIRATION_MINUTES = 1440;
        private readonly NpgsqlDataSource _dataSource;
        public PgConnector(string username, string password, string host = "127.0.0.1", int port = 5432, string dbName = "offers")
        {
            // Validate input parameters before building connection string
            if (string.IsNullOrEmpty(username)) throw new PgConnectorException("username must not be empty");
            if (string.IsNullOrEmpty(password)) throw new PgConnectorException("password must not be empty");
            if (string.IsNullOrEmpty(host)) throw new PgConnectorException("host must not be empty");
            if (port < 1024 || port > 65535) throw new PgConnectorException("port must be in range <1024; 65535>");
            if (string.IsNullOrEmpty(dbName)) throw new PgConnectorException("dbName must not be empty");

            // Build the PostgreSQL connection string with pooling enabled
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = dbName,
                Pooling = true,  // Enable connection pooling for performance
                Timeout = 5      // Connection timeout in seconds
            };

            try
            {
                // Attempt to create the data source using the validated builder
                var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.ConnectionString);

                // 2. Map your C# types to PostgreSQL composite types
                // This tells Npgsql how to handle these custom types globally.
                dataSourceBuilder.MapComposite<ExternalOfferUosInput>("public.external_offer_uos_input");
                dataSourceBuilder.MapComposite<BatchResult>("public.batch_result");

                // 3. Build the data source
                _dataSource = dataSourceBuilder.Build();
            }
            catch (ArgumentException ex)
            {
                throw new PgConnectorException("Invalid connection string", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PgConnectorException("Configuration error", ex);
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("PostgreSQL error", ex);
            }
            catch (Exception ex)
            {
                throw new PgConnectorException("Unexpected error", ex);
            }
        }

        public async Task<FrozenDictionary<int, BatchResult>> AddOffersBatch(List<UOS> uosOffers, bool no_throw = true, CancellationToken cancellationToken = default)
        {
            if (uosOffers == null || uosOffers.Count == 0)
            {
                return FrozenDictionary<int, BatchResult>.Empty;
            }

            var inputData = new ExternalOfferUosInput[uosOffers.Count];
            var results = new ConcurrentDictionary<int, BatchResult>();

            try
            {
                List<string> links = new(uosOffers.Count);
                foreach (var uos in uosOffers)
                {
                    links.Add(uos.Url);
                }
                FrozenDictionary<string, ValueTuple<string, string>> parsed_links = Helpers.SplitLinks(links, no_throw);

                var parallelOptions = new ParallelOptions
                {
                    CancellationToken = cancellationToken
                };

                await Parallel.ForAsync(0, uosOffers.Count, parallelOptions, async (i, ct) =>
                {
                    var uos = uosOffers[i];

                    if (!parsed_links.TryGetValue(uos.Url, out var parsedUrl))
                    {
                        results.TryAdd(i, new BatchResult(i, null, null, "Failed to split URL."));
                        inputData[i] = null;
                        return;
                    }

                    try
                    {
                        FrozenDictionary<string, List<string>> skills = Helpers.SplitSkills(uos.Requirements.Skills);
                        FrozenDictionary<string, List<string>> languages = Helpers.SplitLanguages(uos.Requirements.Languages);

                        var publishedDate = DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates.Published)).ToUniversalTime();
                        var expiresDate = DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates.Expires)).ToUniversalTime();

                        inputData[i] = new ExternalOfferUosInput(
                            Helpers.SanitizeInput(uos.Source),
                            Helpers.SanitizeInput(parsedUrl.Item1),
                            Helpers.SanitizeInput(parsedUrl.Item2),
                            Helpers.SanitizeInput(uos.JobTitle),
                            Helpers.SanitizeInput(uos.Company.Name),
                            Helpers.SanitizeInput(uos.Company.LogoUrl),
                            Helpers.SanitizeInput(uos.Description),
                            uos.Salary.From,
                            uos.Salary.To,
                            Helpers.SanitizeInput(uos.Salary.Currency),
                            Helpers.SanitizeInput(uos.Salary.Period),
                            uos.Salary.Type == "gross",
                            Helpers.SanitizeInput(uos.Location.BuildingNumber),
                            Helpers.SanitizeInput(uos.Location.Street),
                            Helpers.SanitizeInput(uos.Location.City),
                            Helpers.SanitizeInput(uos.Location.PostalCode),
                            uos.Location.Coordinates.Latitude,
                            uos.Location.Coordinates.Longitude,
                            uos.Location.IsHybrid != true ? uos.Location.IsRemote : null,
                            uos.Location.IsRemote != true ? uos.Location.IsHybrid : null,
                            Helpers.SanitizeInput(uos.Category.LeadingCategory),
                            Helpers.SanitizeArray(uos.Category.SubCategories),
                            skills["skill"],
                            Helpers.MapToShort(skills["experienceMonths"]),
                            skills["experienceLevel"],
                            Helpers.SanitizeArray(uos.Requirements.Education),
                            languages["languages"],
                            languages["languageLevels"],
                            Helpers.SanitizeArray(uos.Employment.Types),
                            Helpers.SanitizeArray(uos.Employment.Schedules),
                            publishedDate,
                            expiresDate,
                            Helpers.SanitizeArray(uos.Benefits),
                            uos.IsUrgent,
                            uos.IsForUkrainians,
                            DateTimeOffset.UtcNow.AddMinutes(OFFER_EXPIRATION_MINUTES)
                        );
                    }
                    catch (Exception ex)
                    {
                        results.TryAdd(i, new BatchResult(i, null, null, $"Failed to parse item data: {ex.Message}"));
                        inputData[i] = null;
                    }
                });
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) when (ex is not PgConnectorException)
            {
                throw new PgConnectorException("An unexpected error occurred during parallel data preparation.", ex);
            }

            const string UpsertCommand = "public.upsert_external_offers_batch";
            try
            {
                await using (var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(UpsertCommand, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new NpgsqlParameter<ExternalOfferUosInput[]>("p_inputs", inputData));

                        var resultsParam = new NpgsqlParameter("o_results", NpgsqlDbType.Array | NpgsqlDbType.Unknown)
                        {
                            Direction = ParameterDirection.Output,
                            DataTypeName = "public.batch_result"
                        };
                        command.Parameters.Add(resultsParam);

                        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);

                        if (resultsParam.Value is BatchResult[] dbResults)
                        {
                            if (dbResults.Length > 0)
                            {
                                foreach (var res in dbResults)
                                {
                                    results[res.Idx] = res;
                                }
                            }
                            return results.ToFrozenDictionary();
                        }

                        if (resultsParam.Value == null || resultsParam.Value == DBNull.Value)
                        {
                            return results.ToFrozenDictionary();
                        }

                        throw new PgConnectorException($"Procedure returned an unexpected type for 'o_results': {resultsParam.Value.GetType().FullName}");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred during the upsert batch operation.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException)
            {
                throw new PgConnectorException("An unexpected error occurred during the upsert batch operation.", ex);
            }
        }

        public async Task<List<string>> GetRestrictions(CancellationToken cancellationToken = default)
        {
            const string GetDictionariesCommand = "SELECT public.get_dictionaries_text();";
            try
            {

                await using (var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(GetDictionariesCommand, connection))
                    {

                        var jsonResult = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                        if (jsonResult is string jsonString)
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            var dictionaries = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString, options);
                                
                            if (dictionaries == null)
                            {
                                return [];
                            }

                            return ResultParsers.RestrictionsParser(dictionaries);
                        }

                        if (jsonResult == null || jsonResult == DBNull.Value)
                        {
                            return [];
                        }

                        throw new PgConnectorException($"Database function returned an unexpected type: {jsonResult.GetType().FullName}");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching dictionaries.", ex);
            }
            catch (JsonException ex)
            {
                throw new PgConnectorException("Failed to parse JSON response from database. Check JSON structure.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching dictionaries.", ex);
            }
        }

        /// <summary>
        /// Asynchronously disposes of the NpgsqlDataSource, 
        /// ensuring that the connection pool and resources are properly released.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            // Dispose the data source asynchronously to gracefully clean up the pool
            await _dataSource.DisposeAsync().ConfigureAwait(false);
        }
    }
}
