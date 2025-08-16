# StudentWorkHub
The application allows searching for job offers and assignments for students by integrating with systems such as Pracuj.pl, OlxPraca and many others, through API methods and WebScraping.

### Documentation online ###
https://docs.google.com/document/d/1mnvTexeT-fP2AOvFTb8ArKLz-O7-UmGf/edit?usp=sharing&ouid=110049717963508518977&rtpof=true&sd=true

### Offer positioning algorithm ###
> ℹ Displayed offers are arranged according to user preferences. Algorithm weights may adjust based on user actions. To establish the initial weights, the user will be asked a few questions.

#### First user survey ####
> ℹ These questions are used to identify the user's basic preferences.

1. Please indicate the city where you would like to work? (select or enter city).
2. Please select the jobs you are interested in (prepared offers, multiple options possible).
3. Please select the industries in which you would like to work (multiple options possible).
4. Please indicate your preferred salary range (two number fields).
5. What mode of work do you prefer (remote, on-site, hybrid) – multiple options possible.
6. Are company perks important to you? (categories e.g. health, sports, general).
7. Please indicate the working hours that suit you (two number fields).
8. Are you in urgent need of work? (checkbox).
9. What form of contract do you prefer (contract of employment, contract of mandate, B2B, contract for specific work, fixed-term contract)?

... possible additional questions for our portal, e.g. when you prefer to receive new job offers (e-mail notification)

#### Algorithm ####

### Unified offer schema ###
> ℹ The `Unified offer schema` standardizes job offers from multiple sources. This transformation enables algorithmic ranking of offers according to user preferences, even when API responses differ in structure. It also simplifies data storage and eliminates redundant site-specific additions.

#### `Unified offer schema` structure: ####
```json
{
    "id": "",
    "source": "",
    "url": "",
    "jobTitle": "",
    "company": {
        "name": "",
        "logoUrl": null,
        "isVerified": null
    },
    "description": null,
    "salary": {
        "from": null,
        "to": null,
        "currency": null,
        "period": null,
        "type": null
    },
    "location": {
        "buildingNumber": null,
        "street": null,
        "city": null,
        "postalCode": null,
        "coordinates": {
            "latitude": null,
            "longitude": null
        },
        "isRemote": null,
        "isHybrid": null
    },
    "requirements": {
        "skills": null,
        "experienceLevel": null,
        "experienceYears": null,
        "education": null,
        "languages": null
    },
    "employment": {
        "types": [],
        "schedules": []
    },
    "dates": {
        "published": "",
        "expires": null
    },
    "benefits": null,
    "isUrgent": false,
    "isForUkrainians": false
}
```

#### `Unified offer schema` fields description ####
- `id`: *string* - Unique offer identifier.
- `source`: *string* - Offer source (e.g. "pracuj.pl", "olx"), used to quickly identify offer provider
- `url`: *string* - Offer url, used to redirect to original offer source
- `jobTitle`: *string* - Job title listed in offer
- `company`: *object* - Issuer company details
    - `name`: *string* - Issuer company name
    - `logoUrl`: *string|null* - Issuer company logo *(optional)*
    - `isVerified`: *boolean|null* - *(optional)* TODO
- `description`: *string|null* - Offer description, used for ai-powered tag extraction *(optional)*
- `salary`: *object* - Job salary details
    - `from`: *number|null* - Minimum salary *(optional)*
    - `to`: *number|null* - Maximum salary *(optional)*
    - `currency`: *string|null* - The currency in which the salary is paid (e.g. "PLN", "EUR") *(optional when `from` and `to` are **null**)*
    - `period`: *string|null* - Payment period (e.g. monthly, weekly, daily) *(optional when `from` and `to` are **null**)*
    - `type`: *string|null* - Salary (gross/net) *(optional when `from` and `to` are **null**)*
- `location`: *object* - Work location
    - `buildingNumber`: *string|null* - Building number of the work location *(optional)*
    - `street`: *string|null* - Street name of the work location *(optional)*
    - `city`: *string|null* - City where the work is located *(optional)*
    - `postalCode`: *string|null* - Postal code of the work location *(optional)*
    - `coordinates`: *object* - Location coordinates
        - `latitude`: *number|null* - Location latitude, used to estimate distance *(is **null** when longitude is **null**)*
        - `longitude`: *number|null* - Location longitude, used to estimate distance *(is **null** when latitude is **null**)*
    - `isRemote`: *boolean|null* - Is work remote or on-site *(is **null** when isHybrid is not **null**)*
    - `isHybrid`: *boolean|null* - Is work hybrid (partially remote, partially on-site) *(is **null** when isRemote is not **null**)*
- `requirements`: *object* - Work requirements
    - `skills`: *array|null* - Skills required for work (e.g. ["C++", "Django", Driver's license]) *(optional)*
    - `experienceLevel`: *array|null* - Experience level required for work (e.g. ["Senior", "Intermediate"]) *(optional)*
    - `experienceYears`: *number|null* - Experience years required for work *(optional)*
    - `education`: *array|null* - Required education (e.g. ["Bachelor's degree", "CCNA"]) *(optional)*
    - `languages`: *array|null* - Required languages (e.g. ["English", "Polish"]) *(optional)*
- `employment`: *object* - Employment type details
    - `types`: *array* - Employment contract (e.g. ["Contract of mandate", "B2B"])
    - `schedules`: *array* - Work schedule (e.g. ["Full-time", "Part-time"])
- `dates`: *object*: - Offer lifecycle dates
    - `published`: *string* - Offer publication date (in format `YYYY-MM-DD HH:MI:SS`)
    - `expires`: *string|null* - Offer expiration date (in format `YYYY-MM-DD HH:MI:SS`) *(optional)*
- `benefits`: *array|null* - Employee benefits offered by the company (e.g. private medical care, sports card) *(optional)*
- `isUrgent`: *boolean* - Does company need an employee urgently
- `isForUkrainians`: *boolean* - This position is mainly intended for Ukrainian applicants

#### Unified offer schema example ####
```json
{
    "id": "1234",
    "source": "pracuj.pl",
    "url": "https://www.pracuj.pl/praca/inzynier-ka-oprogramowania-python-mid-senior-warszawa-kolska-12,oferta,1004183523",
    "jobTitle": "Inżynier /-ka oprogramowania Python (mid/senior)",
    "company": {
        "name": "NASK",
        "logoUrl": "https://logos.gpcdn.pl/loga-firm/20011564/54330000-56be-0050-f1b2-08dd9c317145_280x280.png",
        "isVerified": null
    },
    "description": "Twój zakres obowiązków, Implementacja rozwiązań na podstawie wymagań projektowych, Implementacja testów automatycznych, Przeglądy kodu kolegów z zespołu, Aktywny udział w spotkaniach zespołu projektowego, w tym proponowanie nowych rozwiązań i...",
    "salary": {
        "from": 7000,
        "to": 16000,
        "currency": "PLN",
        "period": "mies.",
        "type": "brutto"
    },
    "location": {
        "buildingNumber": null,
        "street": null,
        "city": "Warszawa",
        "postalCode": null,
        "coordinates": {
            "latitude": 52.25102615356445,
            "longitude": 20.975749969482422
        },
        "isRemote": null,
        "isHybrid": true
    },
    "requirements": {
        "skills": [
            "Python",
            "Django",
            "FastAPI",
            "Web Services",
            "http",
            "REST",
            "SQL",
            "ORM",
            "Celery"
        ],
        "experienceLevel": [
            "Specjalista (Mid / Regular)",
            "Starszy specjalista (Senior)"
        ],
        "experienceYears": null,
        "education": null,
        "languages": null
    },
    "employment": {
        "types": [
            "Umowa o pracę"
        ],
        "schedules": [
            "Pełny etat"
        ]
    },
    "dates": {
        "published": "2025-07-04 14:33:00",
        "expires": "2025-07-24 21:59:59"
    },
    "benefits": null,
    "isUrgent": false,
    "isForUkrainians": false
}
```

### Second revision of application project schema ###
#### With database backup and logging service ####
![App schema v2.1](diagrams/images/app_schema_v2.1.png)

### Second revision of application project schema ###
![App schema v2](diagrams/images/app_schema_v2.png)


#### Offers gathering schema ####
![App schema v2](diagrams/images/app_schema_v2_details.png)

**Icons used in the diagrams:**
[react-wordmark](https://icon-sets.iconify.design/devicon/?icon-filter=react-wordmark), [dotnetcore](https://icon-sets.iconify.design/devicon/?icon-filter=dotnetcore), and [postgresql-wordmark](https://icon-sets.iconify.design/devicon/?icon-filter=postgresql-wordmark) icons by konpa, licensed under the MIT License.


### First revision of application project schema ###
![App schema](diagrams/images/StrukturaAplikacjiTech.png)
