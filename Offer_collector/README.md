# 🧠 OfferScrapper — Technical README

A C# console application for scraping job offers from multiple portals, unifying their data structure, and processing results through an AI module.

---

## ⚙️ Minimum Requirements

* **.NET SDK 8.0** or higher
---

## ▶️ Run Command

```bash
dotnet run <siteTypeId> <offerAmount>
```

### Parameters

| Parameter     | Type  | Description                                                                             |
| ------------- | ----- | --------------------------------------------------------------------------------------- |
| `siteTypeId`  | `int` | Job portal type:<br>1 – Pracuj.pl<br>2 – JustJoin.it<br>3 – OLX Praca<br>4 – Aplikuj.pl |
| `offerAmount` | `int` | Number of job offers to fetch and process                                               |

**Example:**

```bash
dotnet run 1 100
```

Fetches 100% of offers from Pracuj.pl.

```bash
dotnet run 1 50
```

Fetches 50% of offers from Pracuj.pl.

---

## 🧩 General Flow

```text
Program.cs
 ├─ Parse and validate input arguments
 ├─ Initialize proper scraper and URL builder
 ├─ Fetch paginated job listings (PaginationModule)
 ├─ Deserialize raw JSON into schema-specific models
 ├─ Map data to UnifiedOfferSchema
 ├─ Process offers with AIProcessor
 └─ Print or export results
```

---

## 🧱 Core Components

| Class / Module             | Description                                                        |
| -------------------------- | ------------------------------------------------------------------ |
| `Program`                  | Entry point; controls app flow and parameters.                     |
| `OfferSitesTypes`          | Enum defining supported job sites.                                 |
| `FactoryScrapper`          | Creates the appropriate scraper instance.                          |
| `UrlBuilderFactory`        | Creates the appropriate URL builder.                               |
| `PaginationModule`         | Handles pagination and multi-page scraping.                        |
| `OfferMapper`              | Maps source-specific schemas to the unified schema.                |
| `AIProcessor`              | Processes unified offers using AI (classification / text parsing). |

---

## 🧠 Code Breakdown

### 1 Scraper and URL builder selection

```csharp
switch (type) {
    case OfferSitesTypes.Pracujpl:
        scrapper = (PracujplScrapper)FactoryScrapper.CreateScrapper(type);
        urlBuilder = (PracujPlUrlBuilder)UrlBuilderFactory.Create(type);
        break;
    ...
}
```

Creates the appropriate scraper and URL builder depending on the selected portal.

---

### 2 Pagination and data fetching

```csharp
var paginator = new PaginationModule(scrapper, urlBuilder, offerAmount);
(string outputJson, List<string> htmls) = paginator.FetchAllOffersAsync().Result;
```

Fetches job listings across multiple pages.
Returns:

* `outputJson` — raw offers in JSON
* `htmls` — list of HTML contents for each offer

---

### 3 Data mapping

```csharp
List<PracujplSchema> pracujSchemas = OfferMapper.DeserializeJson<List<PracujplSchema>>(outputJson);
foreach (var offer in pracujSchemas) {
    UnifiedOfferSchema unified = OfferMapper.ToUnifiedSchema<List<PracujplSchema>>(offer);
    unifiedOfferSchemas.Add(unified);
}
```

Deserializes portal-specific data and converts it into a **unified format** (`UnifiedOfferSchema`).

---

### 4 AI processing

```csharp
List<UnifiedOfferSchema> processedOffers = GetAiOutput(aiParser, unifiedOfferSchemas).Result;
```

Passes unified data to the AI module for further processing — for example, text cleaning, classification, or information extraction.

---

## 📦 Example Input & Output

**Input:**

```bash
App.exe 2 10
```

**Output (JSON excerpt):**

```json
 {
    "id": 0,
    "source": 1,
    "url": "https://www.pracuj.pl/praca/python-developer-warszawa-pulawska-2,oferta,1004340528",
    "jobTitle": "Python Developer",
    "company": {
      "name": "SQUARE ONE RESOURCES sp. z o.o.",
      "logoUrl": "https://logos.gpcdn.pl/loga-firm/20409928/00230000-56be-0050-c213-08dd5a4ccc03_280x280.png"
    },
    "description": "Your responsibilities, Develop and maintain Python applications, focusing on scalable solutions in the cloud., Work with Azure (Fabric or Synapse) to manage data processing and cloud infrastructure., Integrate Large Language Models (LLM) into the...",
    "salary": {
      "from": 0.0,
      "to": 0.0,
      "currency": null,
      "period": null,
      "type": null
    },
    "location": {
      "buildingNumber": "",
      "street": "120 Moorgate",
      "city": "Londyn",
      "postalCode": "EC2M 6UR",
      "coordinates": {
        "latitude": 51.51887608975094,
        "longitude": -0.08758862516429988
      },
      "isRemote": true,
      "isHybrid": false
    },
    "category": {
      "leadingCategory": "Programming",
      "subCategories": [
        "Administration of systems",
        "Architecture",
        "Programming"
      ]
    },
    "requirements": {
      "skills": [
        {
          "skill": "Python",
          "experienceMonths": 60,
          "experienceLevel": null
        },
        {
          "skill": "Azure",
          "experienceMonths": null,
          "experienceLevel": null
        },
        {
          "skill": "Cloud platforms",
          "experienceMonths": null,
          "experienceLevel": null
        },
        {
          "skill": "Problem-solving",
          "experienceMonths": null,
          "experienceLevel": null
        }
      ],
      "education": null,
      "languages": null
    },
    "employment": {
      "types": [
        "Kontrakt B2B"
      ],
      "schedules": [
        "Pełny etat"
      ]
    },
    "dates": {
      "published": "2025-10-06T14:22:00Z",
      "expires": "2025-10-08T21:59:59Z"
    },
    "benefits": [],
    "isUrgent": true,
    "isForUkrainians": false
  },
  ...
```
---

## 🧑‍💻 Developer Notes

### Adding a New Job Portal

To integrate a new site:

1. Create a scraper: `<PortalName>Scrapper.cs`
2. Create a URL builder: `<PortalName>UrlBuilder.cs`
3. Add entries in:

   * `OfferSitesTypes`
   * `FactoryScrapper`
   * `UrlBuilderFactory`
   * `Program.cs` (switch statement)
