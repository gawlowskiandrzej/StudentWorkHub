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
                MinPoolSize = 20, // High but allows for faster multiple concurrent operaions
                MaxPoolSize = 100, // High traffic
                Timeout = 15,      // Connection timeout in seconds
                CommandTimeout = 60
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

        public async Task<FrozenDictionary<int, BatchResult>> AddExternalOffersBatch(List<UOS> uosOffers, bool no_throw = true, CancellationToken cancellationToken = default)
        {
            if (uosOffers == null || uosOffers.Count < 1)
            {
                return FrozenDictionary<int, BatchResult>.Empty;
            }

            var inputData = new ExternalOfferUosInput[uosOffers.Count];
            var results = new ConcurrentDictionary<int, BatchResult>();

            try
            {
                var parallelOptions = new ParallelOptions { CancellationToken = cancellationToken };
                List<string> baseSources = await GetSourcesBaseUrls(cancellationToken).ConfigureAwait(false);

                await Parallel.ForAsync(0, uosOffers.Count, parallelOptions, async (i, ct) =>
                {
                    var uos = uosOffers[i];

                    string? matchedPrefix = baseSources.FirstOrDefault(baseUrl =>
                            baseUrl != null &&
                            uos.Url != null &&
                            uos.Url.StartsWith(baseUrl, StringComparison.Ordinal));

                    if (string.IsNullOrWhiteSpace(matchedPrefix))
                    {
                        results.TryAdd(i, new BatchResult(i, null, null, "No matching base URL found."));
                        inputData[i] = null;
                        return;
                    }

                    try
                    {
                        FrozenDictionary<string, List<string>> skills = Helpers.SplitSkills(uos.Requirements.Skills);
                        FrozenDictionary<string, List<string>> languages = Helpers.SplitLanguages(uos.Requirements.Languages);

                        inputData[i] = new ExternalOfferUosInput(
                            Helpers.SanitizeInput(uos.Source),
                            Helpers.SanitizeInput(matchedPrefix),
                            Helpers.SanitizeInput(uos.Url.Replace(matchedPrefix, string.Empty)),
                            Helpers.SanitizeInput(uos.JobTitle),
                            Helpers.SanitizeInput(uos.Company.Name),
                            Helpers.SanitizeInput(uos.Company.LogoUrl),
                            Helpers.SanitizeInput(uos.Description),
                            uos.Salary.From,
                            uos.Salary.To,
                            Helpers.SanitizeInput(uos.Salary.Currency),
                            Helpers.SanitizeInput(uos.Salary.Period),
                            uos.Salary.Type != null ? uos.Salary.Type == "gross" : null,
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
                            DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates.Published)).ToUniversalTime(),
                            DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates.Expires)).ToUniversalTime(),
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
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred during parallel data preparation.", ex);
            }

            const string UpsertCommand = "public.upsert_external_offers_batch";
            const int BatchSize = 20;

            // Chunk indices to preserve mapping to the original inputData array
            var indexChunks = Enumerable.Range(0, inputData.Length).Chunk(BatchSize);

            try
            {
                var dbParallelOptions = new ParallelOptions { CancellationToken = cancellationToken };

                await Parallel.ForEachAsync(indexChunks, dbParallelOptions, async (indices, ct) =>
                {
                    // Prepare payload for this batch, filtering nulls but keeping track of original IDs
                    var batchPayload = new List<ExternalOfferUosInput>(indices.Length);
                    var indexMap = new List<int>(indices.Length);

                    foreach (var idx in indices)
                    {
                        if (inputData[idx] != null)
                        {
                            batchPayload.Add(inputData[idx]);
                            indexMap.Add(idx);
                        }
                    }

                    if (batchPayload.Count == 0) return;

                    await using var connection = await _dataSource.OpenConnectionAsync(ct).ConfigureAwait(false);
                    await using var command = new NpgsqlCommand(UpsertCommand, connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter<List<ExternalOfferUosInput>>("p_inputs", batchPayload));

                    var resultsParam = new NpgsqlParameter("o_results", NpgsqlDbType.Array | NpgsqlDbType.Unknown)
                    {
                        Direction = ParameterDirection.Output,
                        DataTypeName = "public.batch_result"
                    };
                    command.Parameters.Add(resultsParam);

                    await command.ExecuteNonQueryAsync(ct).ConfigureAwait(false);

                    if (resultsParam.Value is BatchResult[] dbResults)
                    {
                        foreach (var res in dbResults)
                        {
                            // Map the batch-local index back to the global inputData index
                            if (res.Idx >= 0 && res.Idx < indexMap.Count)
                            {
                                var originalIndex = indexMap[res.Idx];
                                results.TryAdd(originalIndex, res);
                            }
                        }
                    }
                    else if (resultsParam.Value != null && resultsParam.Value != DBNull.Value)
                    {
                        throw new PgConnectorException($"Unexpected return type: {resultsParam.Value.GetType().FullName}");
                    }
                }).ConfigureAwait(false);

                return results.ToFrozenDictionary();
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
                await using var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
                await using var command = new NpgsqlCommand(GetDictionariesCommand, connection);

                // Fetching data is a single operation here, so it cannot be parallelized at the DB level
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
                        return new List<string>();
                    }

                    var concurrentResults = new ConcurrentBag<string>();

                    Parallel.ForEach(dictionaries, kvp =>
                    {
                        string category = kvp.Key;
                        List<string> items = kvp.Value;

                        if (items == null) return;

                        foreach (var item in items)
                        {
                            concurrentResults.Add(item);
                        }
                    });

                    return concurrentResults.ToList();
                }

                if (jsonResult == null || jsonResult == DBNull.Value)
                {
                    return new List<string>();
                }

                throw new PgConnectorException($"Database function returned an unexpected type: {jsonResult.GetType().FullName}");
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

        public async Task<List<string>> GetSourcesBaseUrls(CancellationToken cancellationToken = default)
        {
            const string GetSourcesCommand = "SELECT public.get_sources_base_urls();";

            try
            {
                await using (var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(GetSourcesCommand, connection))
                    {
                        var dbResult = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                        if (dbResult is string[] stringArray)
                        {
                            return stringArray.ToList();
                        }

                        if (dbResult is Array array && array.Length == 0)
                        {
                            return [];
                        }

                        if (dbResult == null || dbResult == DBNull.Value)
                        {
                            return [];
                        }

                        throw new PgConnectorException(
                            $"Database function returned an unexpected type: {dbResult.GetType().FullName}");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching sources base urls.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching sources base urls.", ex);
            }
        }

        public async Task<FrozenSet<UOS?>> GetExternalOffers(
            string? search_text = null,
            string? leadingCategory = null,
            decimal? salaryFrom = null,
            decimal? salaryTo = null,
            string? salaryCurrency = null,
            string? locationCity = null,
            bool? isRemote = null,
            bool? isHybrid = null,
            double? userLatitude = null,
            double? userLongitude = null,
            double? distanceLimitKm = null,
            int? limit = null,
            int offset = 0,
            CancellationToken cancellationToken = default)
        {
            const string CommandText = """
            SELECT *
            FROM public.search_external_offers_by_keywords(
                @p_keywords,
                @p_leading_category,
                @p_salary_from,
                @p_salary_to,
                @p_salary_currency,
                @p_location_city,
                @p_is_remote,
                @p_is_hybrid,
                @p_user_latitude,
                @p_user_longitude,
                @p_distance_limit_km,
                @p_limit,
                @p_offset
            );
            """;

            try
            {
                var result = new List<string>();

                await using (var connection = await _dataSource
                           .OpenConnectionAsync(cancellationToken)
                           .ConfigureAwait(false))
                await using (var command = new NpgsqlCommand(CommandText, connection))
                {
                    string[]? keywords = search_text != null ? search_text.Split(' ') : null;
                    command.Parameters.Add(new NpgsqlParameter("p_keywords", NpgsqlDbType.Array | NpgsqlDbType.Text)
                    {
                        Value = (object?)keywords ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_leading_category", NpgsqlDbType.Text)
                    {
                        Value = (object?)leadingCategory ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_salary_from", NpgsqlDbType.Numeric)
                    {
                        Value = (object?)salaryFrom ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_salary_to", NpgsqlDbType.Numeric)
                    {
                        Value = (object?)salaryTo ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_salary_currency", NpgsqlDbType.Text)
                    {
                        Value = (object?)salaryCurrency ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_location_city", NpgsqlDbType.Text)
                    {
                        Value = (object?)locationCity ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_is_remote", NpgsqlDbType.Boolean)
                    {
                        Value = (object?)isRemote ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_is_hybrid", NpgsqlDbType.Boolean)
                    {
                        Value = (object?)isHybrid ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_user_latitude", NpgsqlDbType.Double)
                    {
                        Value = (object?)userLatitude ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_user_longitude", NpgsqlDbType.Double)
                    {
                        Value = (object?)userLongitude ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_distance_limit_km", NpgsqlDbType.Double)
                    {
                        Value = (object?)distanceLimitKm ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_limit", NpgsqlDbType.Integer)
                    {
                        Value = (object?)limit ?? DBNull.Value
                    });

                    command.Parameters.Add(new NpgsqlParameter("p_offset", NpgsqlDbType.Integer)
                    {
                        Value = offset
                    });

                    await using (var reader = await command
                                   .ExecuteReaderAsync(cancellationToken)
                                   .ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            if (!reader.IsDBNull(0))
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                return UOSUtils.BuildFromStringList(result).ToFrozenSet();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while searching external offers.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException)
            {
                throw new PgConnectorException("An unexpected error occurred while searching external offers.", ex);
            }
        }

        public async Task<bool> AddSource(string name, string baseUrl, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(baseUrl))
                return false;

            const string AddSourceCommand = "CALL public.add_source(@name, @base_url);";
            try
            {
                await using (var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(AddSourceCommand, connection))
                    {
                        // Add parameters with explicit types to avoid type mismatch
                        command.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, name);
                        command.Parameters.AddWithValue("base_url", NpgsqlDbType.Varchar, baseUrl);

                        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                    }
                }
                return true;
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                return false;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while adding a new source.", ex);
            }
            catch (Exception ex)
            {
                throw new PgConnectorException("An unexpected error occurred while adding a new source.", ex);
            }
        }

        public async Task<FrozenDictionary<string, FrozenDictionary<int, string>>> GetSimpleLookups(
            List<string> tableNames,
            CancellationToken cancellationToken = default)
        {
            if (tableNames == null)
                return FrozenDictionary<string, FrozenDictionary<int, string>>.Empty;
            
            // 1. Filter duplicates and empty strings immediately
            var uniqueTableNames = tableNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase) // Case-insensitive distinct
                .ToList();

            // Use ConcurrentDictionary for thread-safe insertion as batches run in parallel
            var results = new ConcurrentDictionary<string, FrozenDictionary<int, string>>();

            try
            {
                // 2. Split the list into batches of 5 elements (Feature available in .NET 6+)
                var batches = uniqueTableNames.Chunk(5);

                var parallelOptions = new ParallelOptions
                {
                    CancellationToken = cancellationToken
                    // MaxDegreeOfParallelism = Environment.ProcessorCount // Optional: Limit concurrent batches
                };

                // 3. Process batches in parallel
                await Parallel.ForEachAsync(batches, parallelOptions, async (batch, ct) =>
                {
                    // 4. Process items within the batch sequentially (foreach)
                    foreach (var rawName in batch)
                    {
                        // Open a connection for this specific query
                        // Since this loop is sequential, we occupy only 1 connection per batch at a time.
                        await using var connection = await _dataSource.OpenConnectionAsync(ct).ConfigureAwait(false);
                        await using var command = new NpgsqlCommand();
                        command.Connection = connection;

                        var normalizedName = rawName.Trim().ToLowerInvariant();

                        command.CommandText = normalizedName switch
                        {
                            "currencies" => "SELECT public.get_currencies_dict();",
                            "salary_periods" => "SELECT public.get_salary_periods_dict();",
                            "skills" => "SELECT public.get_skills_dict();",
                            "experience_levels" => "SELECT public.get_experience_levels_dict();",
                            "education_levels" => "SELECT public.get_education_levels_dict();",
                            "languages" => "SELECT public.get_languages_dict();",
                            "language_levels" => "SELECT public.get_language_levels_dict();",
                            "cities" => "SELECT public.get_cities_dict();",
                            "streets" => "SELECT public.get_streets_dict();",
                            "postal_codes" => "SELECT public.get_postal_codes_dict();",
                            "employment_schedules" => "SELECT public.get_employment_schedules_dict();",
                            "employment_types" => "SELECT public.get_employment_types_dict();",
                            "benefits" => "SELECT public.get_benefits_dict();",
                            "leading_categories" => "SELECT public.get_leading_categories_dict();",
                            "sub_categories" => "SELECT public.get_sub_categories_dict();",
                            _ => throw new PgConnectorException($"Unsupported simple lookup table name: {rawName}")
                        };

                        var dbResult = await command.ExecuteScalarAsync(ct).ConfigureAwait(false);

                        // Handle null/empty results
                        if (dbResult == null || dbResult == DBNull.Value)
                        {
                            results.TryAdd(rawName, FrozenDictionary.ToFrozenDictionary<int, string>(new Dictionary<int, string>()));
                            continue; // Move to next item in batch
                        }

                        if (dbResult is not string json)
                        {
                            throw new PgConnectorException(
                                $"Database function returned an unexpected type: {dbResult.GetType().FullName}");
                        }

                        var stringDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                                         ?? new Dictionary<string, string>();

                        if (stringDict.Count == 0)
                        {
                            results.TryAdd(rawName, FrozenDictionary.ToFrozenDictionary<int, string>(new Dictionary<int, string>()));
                            continue; // Move to next item in batch
                        }

                        var temp = new Dictionary<int, string>();

                        foreach (var kv in stringDict)
                        {
                            if (!int.TryParse(kv.Key, out var id))
                            {
                                throw new PgConnectorException($"Database JSON payload contained a non-integer key: '{kv.Key}'.");
                            }

                            temp[id] = kv.Value;
                        }

                        results.TryAdd(rawName, temp.ToFrozenDictionary());
                    }
                }).ConfigureAwait(false);

                return results.ToFrozenDictionary();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching simple lookup dictionaries.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching simple lookup dictionaries.", ex);
            }
        }

        public async Task<FrozenDictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>>> GetComplexLookups(
            List<string> tableNames,
            CancellationToken cancellationToken = default)
        {
            if (tableNames == null)
                return FrozenDictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>>.Empty;

            var uniqueTableNames = tableNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase) // Case-insensitive distinct
                .ToList();

            // Use a standard Dictionary as thread safety is not required for sequential execution
            var results = new Dictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>>();

            try
            {
                foreach (var rawName in tableNames)
                {
                    // Respect cancellation token in the loop
                    cancellationToken.ThrowIfCancellationRequested();

                    if (string.IsNullOrWhiteSpace(rawName))
                    {
                        throw new PgConnectorException("Lookup table name cannot be null or whitespace.");
                    }

                    // Open a dedicated connection for this iteration
                    await using var connection = await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
                    await using var command = new NpgsqlCommand();
                    command.Connection = connection;

                    var normalizedName = rawName.Trim().ToLowerInvariant();

                    command.CommandText = normalizedName switch
                    {
                        "sources" => "SELECT public.get_sources_dict();",
                        "companies" => "SELECT public.get_companies_dict();",
                        _ => throw new PgConnectorException($"Unsupported complex lookup table name: {rawName}")
                    };

                    var dbResult = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                    // Handle null DB results
                    if (dbResult == null || dbResult == DBNull.Value)
                    {
                        results.TryAdd(rawName, FrozenDictionary.ToFrozenDictionary<int, FrozenDictionary<string, string?>>(
                            new Dictionary<int, FrozenDictionary<string, string?>>()));
                        continue;
                    }

                    if (dbResult is not string json)
                    {
                        throw new PgConnectorException(
                            $"Database function returned an unexpected type: {dbResult.GetType().FullName}");
                    }

                    // Deserialize JSON payload
                    var rawDict = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string?>>>(json)
                                  ?? new Dictionary<string, Dictionary<string, string?>>();

                    if (rawDict.Count == 0)
                    {
                        results.TryAdd(rawName, FrozenDictionary.ToFrozenDictionary<int, FrozenDictionary<string, string?>>(
                            new Dictionary<int, FrozenDictionary<string, string?>>()));
                        continue;
                    }

                    var temp = new Dictionary<int, FrozenDictionary<string, string?>>();

                    // Parse inner dictionary structure
                    foreach (var kv in rawDict)
                    {
                        if (!int.TryParse(kv.Key, out var id))
                        {
                            throw new PgConnectorException($"Database JSON payload contained a non-integer key: '{kv.Key}'.");
                        }

                        var innerFrozen = kv.Value.ToFrozenDictionary(inner => inner.Key, inner => inner.Value);
                        temp[id] = innerFrozen;
                    }

                    results.TryAdd(rawName, temp.ToFrozenDictionary());
                }

                return results.ToFrozenDictionary();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching complex lookup dictionaries.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching complex lookup dictionaries.", ex);
            }
        }

        public async Task<bool> DeleteOffersById(
            List<long> offerIds,
            CancellationToken cancellationToken = default)
        {
            // Validate input collection
            if (offerIds == null)
                return false;

            // Ensure materialized array and remove duplicates to avoid unnecessary work
            long[] offerIdArray = offerIds
                .Where(id => id > 0) // basic sanity check, optional
                .Distinct()
                .ToArray();

            if (offerIdArray.Length == 0)
                return false;

            const string DeleteOffersCommand = "CALL public.delete_offers_by_id(@offer_ids);";

            try
            {
                await using (var connection = await _dataSource
                                 .OpenConnectionAsync(cancellationToken)
                                 .ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(DeleteOffersCommand, connection))
                    {
                        // Pass bigint[] array to the stored procedure
                        command.Parameters.AddWithValue(
                            "offer_ids",
                            NpgsqlDbType.Array | NpgsqlDbType.Bigint,
                            offerIdArray);

                        await command
                            .ExecuteNonQueryAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (NpgsqlException ex)
            {
                // Wrap database-specific exception in a custom exception type
                throw new PgConnectorException(
                    "A database error occurred while deleting offers.",
                    ex);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions
                throw new PgConnectorException(
                    "An unexpected error occurred while deleting offers.",
                    ex);
            }
        }

        public async Task<bool> MarkOfferAsSaved(
            long offerId,
            CancellationToken cancellationToken = default)
        {
            if (offerId <= 0)
                return false;

            const string MarkAsSavedCommand = "CALL public.mark_as_saved(@offer_id);";

            try
            {
                await using (var connection = await _dataSource
                                 .OpenConnectionAsync(cancellationToken)
                                 .ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(MarkAsSavedCommand, connection))
                    {
                        // Add parameter with explicit type to avoid type mismatch
                        command.Parameters.AddWithValue("offer_id", NpgsqlDbType.Bigint, offerId);

                        await command
                            .ExecuteNonQueryAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException(
                    "A database error occurred while marking offer as saved.",
                    ex);
            }
            catch (Exception ex)
            {
                throw new PgConnectorException(
                    "An unexpected error occurred while marking offer as saved.",
                    ex);
            }
        }

        public async Task<bool> UnmarkOfferAsSaved(
            long offerId,
            CancellationToken cancellationToken = default)
        {
            if (offerId <= 0)
                return false;

            const string UnmarkAsSavedCommand = "CALL public.mark_as_unsaved(@offer_id);";

            try
            {
                await using (var connection = await _dataSource
                                 .OpenConnectionAsync(cancellationToken)
                                 .ConfigureAwait(false))
                {
                    await using (var command = new NpgsqlCommand(UnmarkAsSavedCommand, connection))
                    {
                        // Add parameter with explicit type to avoid type mismatch
                        command.Parameters.AddWithValue("offer_id", NpgsqlDbType.Bigint, offerId);

                        await command
                            .ExecuteNonQueryAsync(cancellationToken)
                            .ConfigureAwait(false);
                    }
                }

                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException(
                    "A database error occurred while unmarking offer as saved.",
                    ex);
            }
            catch (Exception ex)
            {
                throw new PgConnectorException(
                    "An unexpected error occurred while unmarking offer as saved.",
                    ex);
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
