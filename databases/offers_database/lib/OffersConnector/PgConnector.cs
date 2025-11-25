using Npgsql;
using NpgsqlTypes;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Data;
using System.Text.Json;
using UnifiedOfferSchema;

namespace OffersConnector
{
    /// <summary>
    /// Provides asynchronous PostgreSQL access for importing, searching and managing offers, normalizing failures into <see cref="PgConnectorException"/>.
    /// </summary>
    public sealed class PgConnector : IAsyncDisposable
    {
        private static readonly int OFFER_EXPIRATION_MINUTES = 1440; // Default soft TTL for imported offers (in minutes)
        private static readonly JsonSerializerOptions _jOptionsCi = new() { PropertyNameCaseInsensitive = true }; // Shared JSON options for case-insensitive dictionary payloads
        private readonly NpgsqlDataSource _dataSource; // Pooled PostgreSQL data source shared by all operations
        private bool _disposed; // Tracks whether the connector has already been asynchronously disposed

        /// <summary>
        /// Initializes a new instance configured to connect to the specified PostgreSQL database using a pooled data source.
        /// </summary>
        /// <param name="username">Database user name used for all connections.</param>
        /// <param name="password">Database user password.</param>
        /// <param name="host">Database host name or IP address.</param>
        /// <param name="port">TCP port of the PostgreSQL instance.</param>
        /// <param name="dbName">Target database name.</param>
        /// <exception cref="PgConnectorException">
        /// Thrown when input parameters are invalid or when the underlying Npgsql data source cannot be created.
        /// </exception>
        public PgConnector(string username, string password, string host = "127.0.0.1", int port = 5432, string dbName = "offers")
        {
            if (string.IsNullOrEmpty(username)) throw new PgConnectorException("username must not be empty");
            if (string.IsNullOrEmpty(password)) throw new PgConnectorException("password must not be empty");
            if (string.IsNullOrEmpty(host)) throw new PgConnectorException("host must not be empty");
            if (port < 1024 || port > 65535) throw new PgConnectorException("port must be in range <1024; 65535>");
            if (string.IsNullOrEmpty(dbName)) throw new PgConnectorException("dbName must not be empty");

            // Build the PostgreSQL connection string with pooling enabled
            NpgsqlConnectionStringBuilder builder = new()
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = dbName,
                Pooling = true,
                MinPoolSize = 20, 
                MaxPoolSize = 100,
                Timeout = 15,
                CommandTimeout = 60
            };

            try
            {
                // Attempt to create the data source using the validated builder
                NpgsqlDataSourceBuilder dataSourceBuilder = new(builder.ConnectionString);

                // Map C# types to PostgreSQL composite types
                dataSourceBuilder.MapComposite<ExternalOfferUosInput>("public.external_offer_uos_input");
                dataSourceBuilder.MapComposite<BatchResult>("public.batch_result");

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

        /// <summary>
        /// Imports a batch of external offers, normalizes them to <see cref="ExternalOfferUosInput"/> and upserts them using the database batch procedure.
        /// </summary>
        /// <param name="uosOffers">Offers to import; the index of each element is preserved in the returned dictionary.</param>
        /// <param name="cancellationToken">Token used to cancel preparation or database work.</param>
        /// <returns>
        /// An immutable mapping from the original zero-based offer index to the database processing result.
        /// Returns an empty dictionary when <paramref name="uosOffers"/> is null or empty.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when source configuration is missing or when a database or transformation error occurs.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<FrozenDictionary<int, BatchResult>> AddExternalOffersBatch(List<UOS> uosOffers, CancellationToken cancellationToken = default)
        {
            if (uosOffers is null || uosOffers.Count < 1)
                return FrozenDictionary<int, BatchResult>.Empty;

            ExternalOfferUosInput?[] inputData = new ExternalOfferUosInput[uosOffers.Count];
            ConcurrentDictionary<int, BatchResult> results = []; // Holds per-index status returned from the batch procedure
            try
            {
                FrozenSet<string> baseSources = await GetSourcesBaseUrls(cancellationToken).ConfigureAwait(false);
                if (baseSources == FrozenSet<string>.Empty)
                    throw new PgConnectorException("No sources baseUrl found. Try using `AddSource` first");

                ParallelOptions parallelOptions = new() { CancellationToken = cancellationToken };
                await Parallel.ForAsync(0, uosOffers.Count, parallelOptions, (i, _) =>
                {
                    UOS uos = uosOffers[i];
                    if (uos is null)
                        return Helpers.SetErrorResult(results, inputData, i, "Offer is null");

                    // Validate presence of critical nested fields before building the composite input
                    Dictionary<string, object?> requiredElements = new()
                    {
                        { "url", uos.Url},
                        { "company", uos.Company },
                        { "salary", uos.Salary },
                        { "location", uos.Location },
                        { "category", uos.Category },
                        { "requirements", uos.Requirements },
                        { "dates", uos.Dates },
                        { "dates.published", uos.Dates.Published },
                        { "dates.expires", uos.Dates.Expires },
                    };

                    foreach (KeyValuePair<string, object?> element in requiredElements)
                        if (element.Value is null)
                            return Helpers.SetErrorResult(results, inputData, i, "Offer has no required key, or key is null: `{element.Key}`");

                    string? matchedPrefix = baseSources.FirstOrDefault(baseUrl =>
                            uos.Url.StartsWith(baseUrl, StringComparison.Ordinal));

                    if (string.IsNullOrWhiteSpace(matchedPrefix))
                        return Helpers.SetErrorResult(results, inputData, i, "No matching base URL found. Try using `AddSource` first");

                    try
                    {
                        FrozenDictionary<string, List<string?>?> skills = (uos.Requirements is not null) ? Helpers.SplitSkills(uos.Requirements.Skills) : Helpers.SplitSkills(null); // Żeby od razu zwróciło pusty poprawny słownik
                        FrozenDictionary<string, List<string?>?> languages = (uos.Requirements is not null) ? Helpers.SplitLanguages(uos.Requirements.Languages) : Helpers.SplitLanguages(null); // Żeby od razu zwróciło pusty poprawny słownik
                        FrozenDictionary<string, double?> coordinates = Helpers.GetCoordinates(uos.Location);

                        inputData[i] = new ExternalOfferUosInput(
                            Helpers.SanitizeInput(uos.Source),
                            Helpers.SanitizeInput(matchedPrefix),
                            Helpers.SanitizeInput(uos.Url.Replace(matchedPrefix, string.Empty)),
                            Helpers.SanitizeInput(uos.JobTitle),
                            Helpers.SanitizeInput(uos.Company?.Name),
                            Helpers.SanitizeInput(uos.Company?.LogoUrl),
                            Helpers.SanitizeInput(uos.Description),
                            uos.Salary?.From,
                            uos.Salary?.To,
                            Helpers.SanitizeInput(uos.Salary?.Currency),
                            Helpers.SanitizeInput(uos.Salary?.Period),
                            uos.Salary?.Type is not null ? uos.Salary?.Type == "gross" : null,
                            Helpers.SanitizeInput(uos.Location?.BuildingNumber),
                            Helpers.SanitizeInput(uos.Location?.Street),
                            Helpers.SanitizeInput(uos.Location?.City),
                            Helpers.SanitizeInput(uos.Location?.PostalCode),
                            coordinates["latitude"],
                            coordinates["longitude"],
                            uos.Location?.IsHybrid != true ? uos.Location?.IsRemote : null,
                            uos.Location?.IsRemote != true ? uos.Location?.IsHybrid : null,
                            Helpers.SanitizeInput(uos.Category?.LeadingCategory),
                            Helpers.SanitizeArray(uos.Category?.SubCategories)?.Where(x => x != null).Select(x => x!).ToList(),
                            skills["skill"], // Tu nie występują wartości null bo są wycięte w SplitSkills, ale deklaracja typu jest inna wiec wyswietla sie jako blad
                            Helpers.MapToShort(skills["experienceMonths"]),
                            skills["experienceLevel"],
                            Helpers.SanitizeArray(uos.Requirements?.Education)?.Where(x => x != null).Select(x => x!).ToList(),
                            languages["languages"]?.Where(x => x != null).Select(x => x!).ToList(),
                            languages["languageLevels"],
                            Helpers.SanitizeArray(uos.Employment?.Types)?.Where(x => x != null).Select(x => x!).ToList(),
                            Helpers.SanitizeArray(uos.Employment?.Schedules)?.Where(x => x != null).Select(x => x!).ToList(),
                            DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates?.Published)).ToUniversalTime(),
                            DateTimeOffset.Parse(Helpers.SanitizeInput(uos.Dates?.Expires)).ToUniversalTime(),
                            Helpers.SanitizeArray(uos.Benefits)?.Where(x => x != null).Select(x => x!).ToList(),
                            uos.IsUrgent,
                            uos.IsForUkrainians,
                            DateTimeOffset.UtcNow.AddMinutes(OFFER_EXPIRATION_MINUTES)
                        );
                    }
                    catch (Exception ex)
                    {
                        return Helpers.SetErrorResult(results, inputData, i, $"Failed to parse item data: {ex.Message}");
                    }

                    return ValueTask.CompletedTask;
                });
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred during parallel data preparation.", ex);
            }

            // Chunk indices to keep single procedure invocations reasonably small
            IEnumerable<int[]> indexChunks = Enumerable.Range(0, inputData.Length).Chunk(20);
            try
            {
                ParallelOptions dbParallelOptions = new() { CancellationToken = cancellationToken };

                await Parallel.ForEachAsync(indexChunks, dbParallelOptions, async (indices, ct) =>
                {
                    List<ExternalOfferUosInput?> batchPayload = new(indices.Length);

                    foreach (int idx in indices)
                        batchPayload.Add(inputData[idx]);

                    if (batchPayload.Count == 0) return;

                    await using NpgsqlConnection connection = 
                        await _dataSource.OpenConnectionAsync(ct).ConfigureAwait(false);

                    await using NpgsqlCommand command = 
                        new("public.upsert_external_offers_batch", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new NpgsqlParameter<List<ExternalOfferUosInput?>>("p_inputs", batchPayload));

                    NpgsqlParameter resultsParam = new("o_results", NpgsqlDbType.Array | NpgsqlDbType.Unknown)
                    {
                        Direction = ParameterDirection.Output,
                        DataTypeName = "public.batch_result"
                    };
                    command.Parameters.Add(resultsParam);

                    await command.ExecuteNonQueryAsync(ct).ConfigureAwait(false);

                    if (resultsParam.Value is not BatchResult[] resultArray)
                    {
                        // Defensive: if the procedure returns nothing, emit synthetic error results
                        foreach (int idx in indices)
                        {
                            results.TryAdd(idx, new BatchResult(idx, null, null, "Database did not return any results"));
                        }
                        return;
                    }

                    int resultEnum = 0;
                    foreach (int idx in indices)
                    {                        
                        results.TryAdd(idx, new BatchResult(idx, resultArray[resultEnum].OfferId, resultArray[resultEnum].Action, resultArray[resultEnum].Error));
                        resultEnum++;
                    }
                }).ConfigureAwait(false);

                return results.ToFrozenDictionary();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred during the upsert batch operation.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred during the upsert batch operation.", ex);
            }
        }

        /// <summary>
        /// Fetches standardized dictionary values from the database and converts them into textual restriction prompts for the LLM.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// A list of instruction strings describing how to use dictionary-based values; returns an empty list when the database does not return any data.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails or when the JSON payload has an unexpected shape.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<List<string>> GetRestrictions(CancellationToken cancellationToken = default)
        {
            try
            {
                await using NpgsqlConnection connection = 
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command = 
                    new("SELECT public.get_dictionaries_text();", connection);

                object? jsonResult = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
                if (jsonResult is string jsonString)
                {
                    Dictionary<string, List<string>>? dictionaries = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString, _jOptionsCi);

                    if (dictionaries is null)
                        return [];

                    return ResultParsers.RestrictionsParser(dictionaries);
                }

                return [];
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching dictionaries.", ex);
            }
            catch (JsonException ex)
            {
                throw new PgConnectorException("Failed to parse JSON response from database. Check JSON structure.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching dictionaries.", ex);
            }
        }

        /// <summary>
        /// Retrieves and normalizes base URLs for all configured sources used to split offer URLs into base and query parts.
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// An immutable set of trimmed base URLs; returns an empty set when no sources are configured.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>        
        private async Task<FrozenSet<string>> GetSourcesBaseUrls(CancellationToken cancellationToken = default)
        {
            try
            {
                await using NpgsqlConnection connection = 
                        await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command = 
                        new("SELECT public.get_sources_base_urls();", connection);

                string[]? dbResult = 
                        (string[]?)await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false); // Typ zwracany przez procedure to ARRAY[]::text[], nulle i ich sprawdzanie są dla poprawności kodu chociaż logika ich nie wymaga

                if (dbResult is null)
                    return FrozenSet<string>.Empty;

                string[] normalizedResult = [.. dbResult
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Select(value => value!.Trim())];

                if (normalizedResult.Length < 1)
                    return FrozenSet<string>.Empty;

                return normalizedResult.ToFrozenSet();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while fetching sources base urls.", ex);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while fetching sources base urls.", ex);
            }
        }

        /// <summary>
        /// Searches external offers using the <c>search_external_offers_by_keywords</c> database function and converts the results to <see cref="UOS"/> objects.
        /// </summary>
        /// <param name="search_text">Free-text query that will be split into individual keywords.</param>
        /// <param name="leadingCategory">Optional leading category filter.</param>
        /// <param name="salaryFrom">Optional lower bound for salary (monthly).</param>
        /// <param name="salaryTo">Optional upper bound for salary (monthly).</param>
        /// <param name="salaryCurrency">Currency code used when filtering by salary.</param>
        /// <param name="locationCity">Optional city name filter.</param>
        /// <param name="isRemote">Filter by remote flag.</param>
        /// <param name="isHybrid">Filter by hybrid flag.</param>
        /// <param name="userLatitude">Latitude of the user used for distance filtering.</param>
        /// <param name="userLongitude">Longitude of the user used for distance filtering.</param>
        /// <param name="distanceLimitKm">Maximum distance in kilometers from the user location.</param>
        /// <param name="limit">Maximum number of rows to return; null lets the database decide.</param>
        /// <param name="offset">Offset of the first row for pagination.</param>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// A frozen set of deserialized offers. Returns an empty set when a <see cref="PgConnectorException"/> is raised during result processing
        /// or when the query yields no rows.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails or when an unexpected error occurs.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
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
                List<string> result = [];

                NpgsqlConnection connection = 
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command = 
                    new(CommandText, connection);

                // Split the free-text query into tokens because the database function expects text[]
                string[]? keywords = search_text?.Split(' ');
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

                await using NpgsqlDataReader reader = 
                    await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                    {
                        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                        {
                            if (!reader.IsDBNull(0))
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }

                return UOSUtils.BuildFromStringList(result).ToFrozenSet();
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while searching external offers.", ex);
            }
            catch (PgConnectorException)
            {
                return FrozenSet<UOS?>.Empty;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while searching external offers.", ex);
            }
        }

        /// <summary>
        /// Registers a new offer source in the database.
        /// </summary>
        /// <param name="name">Human readable name of the source.</param>
        /// <param name="baseUrl">Base URL used later to split full offer URLs.</param>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// <c>true</c> when the source was inserted; <c>false</c> when input is invalid or a duplicate source is detected.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when a database error other than a uniqueness violation occurs.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<bool> AddSource(string name, string baseUrl, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(baseUrl))
                return false;

            try
            {
                await using NpgsqlConnection connection = 
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command = 
                    new("CALL public.add_source(@name, @base_url);", connection);

                command.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, name);
                command.Parameters.AddWithValue("base_url", NpgsqlDbType.Varchar, baseUrl);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
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
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while adding a new source.", ex);
            }
        }

        /// <summary>
        /// Loads flat lookup dictionaries (id to display name) for the requested tables by calling dictionary functions in the database.
        /// </summary>
        /// <param name="tableNames">Logical lookup names (e.g. "skills", "languages").</param>
        /// <param name="cancellationToken">Token used to cancel lookup processing.</param>
        /// <returns>
        /// Immutable mapping from table name to its (id, label) dictionary.
        /// Returns an empty dictionary when <paramref name="tableNames"/> is null or empty.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails or when an unsupported lookup name is requested.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<FrozenDictionary<string, FrozenDictionary<int, string>>> GetSimpleLookups(
            List<string> tableNames,
            CancellationToken cancellationToken = default)
        {
            if (tableNames is null)
                return FrozenDictionary<string, FrozenDictionary<int, string>>.Empty;

            // Filter duplicates and empty names early to reduce database work
            List<string> uniqueTableNames = [.. tableNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)];

            // Use ConcurrentDictionary for thread-safe insertion as batches run in parallel
            ConcurrentDictionary<string, FrozenDictionary<int, string>> results = new();
            try
            {
                // Limit batch size to bound the number of concurrent connections
                IEnumerable<string[]> batches = uniqueTableNames.Chunk(5);

                ParallelOptions parallelOptions = new() { CancellationToken = cancellationToken };

                // Process batches in parallel; each batch processes its tables sequentially on a single connection
                await Parallel.ForEachAsync(batches, parallelOptions, async (batch, ct) =>
                {
                    foreach (string rawName in batch)
                    {
                        await using NpgsqlConnection connection = 
                            await _dataSource.OpenConnectionAsync(ct).ConfigureAwait(false);

                        await using NpgsqlCommand command = new();
                            command.Connection = connection;

                        string normalizedName = rawName.Trim().ToLowerInvariant();
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

                        object? dbResult = await command.ExecuteScalarAsync(ct).ConfigureAwait(false);
                        if (dbResult is null || dbResult is DBNull || dbResult is not string json)
                        {
                            results.TryAdd(rawName, FrozenDictionary<int, string>.Empty);
                            continue;
                        }

                        Dictionary<string, string> stringDict = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? [];
                        if (stringDict.Count == 0)
                        {
                            results.TryAdd(rawName, FrozenDictionary<int, string>.Empty);
                            continue;
                        }

                        Dictionary<int, string> temp = [];
                        foreach (KeyValuePair<string, string> kv in stringDict)
                        {
                            if (!int.TryParse(kv.Key, out var id))
                            {
                                results.TryAdd(rawName, FrozenDictionary<int, string>.Empty);
                                continue;
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

        /// <summary>
        /// Loads nested lookup dictionaries (id to column/value pairs) for the requested tables.
        /// </summary>
        /// <param name="tableNames">Logical lookup names (e.g. "sources", "companies").</param>
        /// <param name="cancellationToken">Token used to cancel lookup processing.</param>
        /// <returns>
        /// Immutable mapping from table name to its nested dictionaries.
        /// Returns an empty dictionary when <paramref name="tableNames"/> is null or empty.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails, when an unsupported lookup name is requested,
        /// or when JSON payload keys cannot be parsed as integer identifiers.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<FrozenDictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>>> GetComplexLookups(
             List<string> tableNames,
             CancellationToken cancellationToken = default)
        {
            if (tableNames is null)
                return FrozenDictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>>.Empty;

            List<string> uniqueTableNames = [.. tableNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)];

            Dictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>> results = [];

            try
            {
                foreach (string rawName in uniqueTableNames)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (string.IsNullOrWhiteSpace(rawName))
                        throw new PgConnectorException("Lookup table name cannot be null or whitespace.");

                    await using NpgsqlConnection connection =
                        await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                    await using NpgsqlCommand command = new();
                    command.Connection = connection;

                    string normalizedName = rawName.Trim().ToLowerInvariant();

                    command.CommandText = normalizedName switch
                    {
                        "sources" => "SELECT public.get_sources_dict();",
                        "companies" => "SELECT public.get_companies_dict();",
                        _ => throw new PgConnectorException($"Unsupported complex lookup table name: {rawName}")
                    };

                    object? dbResult = await command
                        .ExecuteScalarAsync(cancellationToken)
                        .ConfigureAwait(false);

                    if (dbResult is null || dbResult is DBNull || dbResult is not string json)
                    {
                        results.TryAdd(rawName, FrozenDictionary<int, FrozenDictionary<string, string?>>.Empty);
                        continue;
                    }

                    Dictionary<string, Dictionary<string, string?>> rawDict =
                        JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string?>>>(json) ?? [];

                    if (rawDict.Count == 0)
                    {
                        results.TryAdd(rawName, FrozenDictionary<int, FrozenDictionary<string, string?>>.Empty);
                        continue;
                    }

                    Dictionary<int, FrozenDictionary<string, string?>> temp = [];

                    foreach (KeyValuePair<string, Dictionary<string, string?>> kv in rawDict)
                    {
                        if (!int.TryParse(kv.Key, out int id))
                        {
                            throw new PgConnectorException(
                                $"Database JSON payload contained a non-integer key: '{kv.Key}'.");
                        }

                        FrozenDictionary<string, string?> innerFrozen = kv.Value.ToFrozenDictionary(
                            inner => inner.Key,
                            inner => inner.Value);

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

        /// <summary>
        /// Deletes offers with the specified identifiers by calling the corresponding stored procedure.
        /// Offers marked as saved are ommited.
        /// </summary>
        /// <param name="offerIds">Collection of offer identifiers to delete.</param>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// <c>true</c> when at least one valid identifier was sent to the database; <c>false</c> when the input is null or contains no positive ids.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<bool> DeleteOffersById(
            List<long> offerIds,
            CancellationToken cancellationToken = default)
        {
            if (offerIds is null)
                return false;

            // Remove non-positive and duplicate identifiers before hitting the database
            long[] offerIdArray = [.. offerIds
                .Where(id => id > 0)
                .Distinct()];

            if (offerIdArray.Length < 1)
                return false;

            try
            {
                await using NpgsqlConnection connection = 
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command = 
                    new("CALL public.delete_offers_by_id(@offer_ids);", connection);

                command.Parameters.AddWithValue(
                    "offer_ids",
                    NpgsqlDbType.Array | NpgsqlDbType.Bigint,
                    offerIdArray);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while deleting offers.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while deleting offers.", ex);
            }
        }

        /// <summary>
        /// Marks an offer as saved by calling the corresponding stored procedure.
        /// </summary>
        /// <param name="offerId">Identifier of the offer that should be marked as saved.</param>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// <c>true</c> when the stored procedure completes successfully; <c>false</c> when <paramref name="offerId"/> is not a positive value.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<bool> MarkOfferAsSaved(
            long offerId,
            CancellationToken cancellationToken = default)
        {
            if (offerId <= 0)
                return false;

            try
            {
                await using NpgsqlConnection connection =
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command =
                    new("CALL public.mark_as_saved(@offer_id);", connection);

                command.Parameters.AddWithValue(
                    "offer_id",
                    NpgsqlDbType.Bigint,
                    offerId);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while marking offer as saved.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while marking offer as saved.", ex);
            }
        }

        /// <summary>
        /// Clears the saved flag on an offer by calling the corresponding stored procedure.
        /// </summary>
        /// <param name="offerId">Identifier of the offer that should be unmarked.</param>
        /// <param name="cancellationToken">Token used to cancel the database call.</param>
        /// <returns>
        /// <c>true</c> when the stored procedure completes successfully; <c>false</c> when <paramref name="offerId"/> is not a positive value.
        /// </returns>
        /// <exception cref="PgConnectorException">
        /// Thrown when the database call fails.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if <paramref name="cancellationToken"/> is signaled during execution.
        /// </exception>
        public async Task<bool> UnmarkOfferAsSaved(
            long offerId,
            CancellationToken cancellationToken = default)
        {
            if (offerId <= 0)
                return false;

            try
            {
                await using NpgsqlConnection connection =
                    await _dataSource.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

                await using NpgsqlCommand command =
                    new("CALL public.mark_as_unsaved(@offer_id);", connection);

                command.Parameters.AddWithValue(
                    "offer_id",
                    NpgsqlDbType.Bigint,
                    offerId);

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }
            catch (NpgsqlException ex)
            {
                throw new PgConnectorException("A database error occurred while unmarking offer as saved.", ex);
            }
            catch (Exception ex) when (ex is not PgConnectorException && ex is not OperationCanceledException)
            {
                throw new PgConnectorException("An unexpected error occurred while unmarking offer as saved.", ex);
            }
        }

        /// <summary>
        /// Asynchronously disposes the underlying <see cref="NpgsqlDataSource"/> and releases pooled connections.
        /// Safe to call multiple times.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            await _dataSource.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}
