# Usage of UniversalOfferSchema #
## Add USO to project ##
1. Right-click on `Dependencies` -> `add` -> `Project reference`.
2. `Browse` on the bottom panel of `Reference manager`.
3. Select `UnifiedOfferSchema.dll` and click `Add`.
4. Import library inside the code
```c#
using UnifiedOfferSchema;
```
> ℹ Utilities used to manage offers are defined in `UOSUtils` class.

## Code examples ##
### Parsing offers from string to USO ###
#### Single offer ####
```c#
UOS parsedOffer = UOSUtils.BuildFromString(offer);
```

#### Multiple offers ####
> ℹ `BuildFromStringList` is preferred instead of `BuildFromString` when parsing multiple offers.

> ⚠ Every offer that failed parsing will be returned as **null**. If you provide list in `errorMessages` parameter, the error will be listed there.

```c#
List<UOS?> parsedOffersList = UOSUtils.BuildFromStringList([offer1, offer2, ..., offerX]);
```

### Returnig offers from USO ###
#### As string ####
```c#
UOS parsedOffer = UOSUtils.BuildFromString(offer);

string offerString = parsedOffer.AsString();

// Get offer with intendation (pretty)
string offerString = parsedOffer.AsString(true);

// Exclude whole location section, and skill name from every skill in skills array
string offerString = parsedOffer.AsString(true, ["location", "skill"]);
```

#### As JSON ####
```c#
UOS parsedOffer = UOSUtils.BuildFromString(offer);

JsonObject offerJson = parsedOffer.AsJson();

// Get offer with intendation (pretty)
JsonObject offerJson = parsedOffer.AsJson(true);

// Exclude whole location section, and skill name from every skill in skills array
JsonObject offerJson = parsedOffer.AsJson(true, ["location", "skill"]);
```

### Modyfying values ###
> ℹ Attributes have the same names as in UOS but in CamelCase.

```c#
UOS parsedOffer = UOSUtils.BuildFromString(offer);

// Non-nested attribute
parsedOffer.Descrtiption = "New Description";

// Object-nested attribute
parsedOffer.Dates.Published = ""

// Array-nested attribute
parsedOffer.Requirements.Skills[0].Skill = "";
```

### Merging ###
> ℹ Merging allows for joining 2 offers into one, with the latter overwriting the base offer values. Overwriting offer doesn't need to be a full UOS.

#### Single offer ####
```c#
string baseOffer = """
full UOS offer
""";

// Even with UOS part, full JSON format is required
string partialOffer = """
{
"location": {
    "builasdingNumber": "132",
    "street": "Al. Jerozolimskie",
    "city": "Warszawa",
    "coordinates": null,
    "isRemote": true,
    "isHybrid": false
    }
}
""";

UOS parsedOffer = UOSUtils.BuildFromString(baseOffer);

// This will overwrite location keys present in the partialOffer in the baseOffer
parsedOffer.MergeWith(partialOffer);
```

#### Multiple offers ####
> ℹ Lenght of `base offers` must match that of `offer parts`.

> ⚠ Every offer that failed merging will be returned as base offer (or null if base offer was null). If you provide list in `errorMessages` parameter, the error will be listed there.
```c#
List<UOS?> parsedOffersList = UOSUtils.BuildFromStringList([offer1, offer2, ..., offerX]);

UOSUtils.MergeList(parsedOffersList, offerPartsList);
```

### Error handling ###
### Error handling ###
> ℹ Most errors are caught and rethrown as `UOSException`

```c#
try 
{
    // parsing
}
catch (UOSException ex)
{
    // Catch exceptions thrown by parser
}
catch (Exception ex)
{
    // Catch unknown exceptions (doubled security)
}
```