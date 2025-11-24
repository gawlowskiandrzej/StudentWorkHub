# Usage of OffersConnector #
## Add OffersConnector to project ##
> ℹ **info** This library is dependent on: \
>NuGet packages:
>- HtmlSanitizer(9.0.886) by Michael Ganss
>- Npgsql(9.0.3) by Shay Rojansky,Nikita Kazmin,Brar Piening,Nino Floris,Yoh Deadfall,Austin Drenski,Emil Lenngren,Francisco Figueiredo Jr.,Kenji Uno
>
>Internal libraries:
>- UnifiedOfferSchema


1. Right-click on `Dependencies` -> `add` -> `Project reference`.
2. `Browse` on the bottom panel of `Reference manager`.
3. Select `OffersConnector.dll` and click `Add`.
4. Import library inside the code
```c#
using OffersConnector;
```

## Code examples ##
### Creating PgConnector object ###
> ℹ **info** `PgConnector` is an object used to manage database operations.

> ⚠ **warning** Connection parameters should not be hardcoded! Use credentials for user with **ONLY the required permissions** in the database.

```c#
// Define required connection parameters
string host = "127.0.0.1";
int port = 5432;
string dbName = "offers";
string username = "postgres";
string password = "1qazXSW@";

// Create object
await using PgConnector connector = new(username, password, host, port, dbName);
```

### Get restrictions string ###
> ℹ **info** Restrictions are returned as `List<string>` instead of `string` to allow for easier replacing/removing certain restrictions.

> ℹ **info** First returned string in list is a preamble not a restriction

```c#
/* Creation of PgConnector object */

List<string> restrictions = await connector.GetRestrictions();

/* You may remove or replace restrictions freely here */

// Join restrictions to required end format
string restrictionsString = string.Join(Environment.NewLine, restrictions);
```

#### Example Result ####
> ℹ **info** **[...]** are used as result was pretty long.
```text
You must follow the rules below with maximum strictness.
[...]
You must adhere to these rules consistently and without exceptions.
 - `salary.period` **MUST** use standarized values {Godzinowo, [...], Inny}.
 [...]
 - `category.leadingCategory` **MUST** use standarized values {Architektura, [...], Inna}.
```

#### Integration with LLMParser ####
```c#
/* Preparation of restrictionsString */

// Build custom system prompt parameters dictionary
// We use default values for ROLE, RULES and TASK keys, with custom value for RESTRICTIONS key
Dictionary<string, string> systemPromptParams = new()
{
    { "ROLE", Prompts.descriptionInformationExtratorRole },
    { "RULES", Prompts.descriptionInformationExtratorRules },
    { "TASK", Prompts.descriptionInformationExtratorTask },
    { "RESTRICTIONS", restrictionsString }
};

// Build Prompt object
Settings.Prompt promptSettings = new(
    Prompts.defaultSystemPromptTemplate,
    Prompts.descriptionInformationExtratorUserPrompt,
    systemPromptParams,
    null
);
```

### Add new source ###
> ℹ **info** Sources are offer providers e.g. "Pracuj.pl". When adding offers if source isn't declared in the database offer will be rejected.

> ℹ **info** If adding new source failed e.g. for improper/empty values, or source already existing in the database the function will return false.

```c#
/* Creation of PgConnector object */

// Add new source, and check the result.
bool result = await connector.AddSource("test", "https://test.example/offer/");

if (result)
    Console.WriteLine("Success!");
else
    Console.WriteLine("Failure!");
```

### Add new offers ###
```c#
/* Creation of PgConnector object */

// Assuming offers are in json string stored as List<string> offers.
List<string> errors = []; // Parsing errors

// Parsing offers to UOS list, while removing empty elements.
List<UOS> offersToDB = [.. UOSUtils.BuildFromStringList([.. offers.Where(name => name != null)], errors).Where(offer => offer != null)];

// Add offers to database
FrozenDictionary<int, BatchResult> batchResults =
    await connector.AddExternalOffersBatch(offersToDB, CancellationToken.None);
```

#### Check results ####
Results are returned as `BatchResult`
```c#
public class BatchResult(int Idx, long? OfferId, string? Action, string? Error)
{
    public int Idx { get; } = Idx; // Id in the batch (starts at 0)
    public long? OfferId { get; } = OfferId; // Id in the database or null on error (starts at 1)
    public string? Action { get; } = Action; // Action if offer was added or updated or null on error
    public string? Error { get; } = Error; // Returned error, null on success
}
```

Display simple statistics
```c#
foreach (BatchResult result in batchResults)
{
    Console.WriteLine($"\nResult for Item Index: {result.Idx}");
    if (result.Error != null)
    {
        Console.WriteLine($"\tStatus: FAILED");
        Console.WriteLine($"\tError: {result.Error}");
    }
    else
    {
        Console.WriteLine($"\tStatus: SUCCESS");
        Console.WriteLine($"\tOfferID: {result.OfferId}");
        Console.WriteLine($"\tAction: {result.Action}");
    }
}
```

### Get offers ###
```c#
/* Creation of PgConnector object */

// Get all offers from database
FrozenSet<UOS?> results = await connector.GetExternalOffers();

// Get first 10 offers from database
FrozenSet<UOS?> resultsLimit = await connector.GetExternalOffers(limit: 10);

// Skip first 10 offers from database
FrozenSet<UOS?> resultsOffser = await connector.GetExternalOffers(offset: 10);

// Use leadingCategory filter
FrozenSet<UOS?> resultsFiler = await connector.GetExternalOffers(leadingCategory: "Informatyka");

// Using multiple filters
FrozenSet<UOS?> resultsFiler = await connector.GetExternalOffers(leadingCategory: "Informatyka", salaryFrom: 10, isRemote: true, limit: 10, offset: 10);
```

### Get lookup tables ###
> ℹ **info** Table names corresponds to database table names.

```c#
/* Creation of PgConnector object */

// Get simple lookup tables (id + value)
FrozenDictionary<string, FrozenDictionary<int, string>> simpleLookups = await connector.GetSimpleLookups(["currencies", "skill", "cities"]);

// Get complex lookup tables (id + multiple value fields)
FrozenDictionary<string, FrozenDictionary<int, FrozenDictionary<string, string?>>> complexLookups = await connector.GetComplexLookups(["sources", "companies"]);

```

### Delete offers ###
> ℹ **info** Offers marked as saved are omitted.

```c#
/* Creation of PgConnector object */

// Remove offers with ids 1 - 4
bool result = await connector.DeleteOffersById([1, 2, 3, 4]);

// Result might be false when null was passed or no ids passed validation, true otherwise
if (result)
    Console.WriteLine("Success!");
else
    Console.WriteLine("Failure!");
```

### Saving offers ###
> ℹ **info** To avoid removing offers from database you may mark them as saved.

> ⚠ **warning** Offers will be removed anyways when they reach expiration date

```c#
/* Creation of PgConnector object */

// Save offer with id 1. 
bool result = await connector.MarkOfferAsSaved(1);

// Result might be false when id failed validation, true otherwise
if (result)
    Console.WriteLine("Success!");
else
    Console.WriteLine("Failure!");

// Unsave offer with id 1. 
bool result = await connector.UnmarkOfferAsSaved(1);

// Result might be false when id failed validation, true otherwise
if (result)
    Console.WriteLine("Success!");
else
    Console.WriteLine("Failure!");
```
### Error handling ###
> ℹ **info** Most errors are caught and rethrown as `PgConnectorException`

```c#
try 
{
    // operations
}
catch (PgConnectorException ex)
{
    // Catch exceptions thrown by connector
}
catch (OperationCanceledException ex)
{
    // Catch exceptions generated by cancellation action
}
catch (Exception ex)
{
    // Catch unknown exceptions (doubled security)
}
```