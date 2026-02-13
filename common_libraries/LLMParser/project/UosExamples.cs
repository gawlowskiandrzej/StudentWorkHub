using System.Collections.Frozen;

namespace LLMParser
{
    /// <summary>
    /// Provides text templates for the Universal Offer Schema (UOS): an empty schema, a filled example, and field explanations.
    /// Designed for building LLM prompts.
    /// </summary>
    internal class UosExamples
    {
        /// <summary>
        /// Empty UOS offer skeleton used in prompts.
        /// </summary>
        private static readonly string _emptyUosOffer = """
         {
          "jobTitle": "",
          "company": {
           "name": ""
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
          "category": {
           "leadingCategory": "",
           "subCategories": null
          },
          "requirements": {
           "skills": [
            {
             "skill": "",
             "experienceMonths": null,
             "experienceLevel": null
            }
           ],
           "education": null,
           "languages": [
            {
             "language": "",
             "level": ""
            }
           ]
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
         """;

        /// <summary>
        /// Example of a plausible UOS offer result used to guide the LLM output.
        /// </summary>
        private static readonly string _exampleUosOffer = """
         {
          "jobTitle": "Inżynier /-ka oprogramowania Python",
          "company": {
           "name": "NASK"
          },
          "description": "Twój zakres obowiązków, Implementacja rozwiązań na podstawie wymagań projektu, Implementacja testów autmatycznych, Przeglądy kodu kolegów z zespołu, Aktywny udział w spotkaniach zespołu, w tym proponowanie nowych rozwiązań.",
          "salary": {
           "from": 7000,
           "to": 16000,
           "currency": "PLN",
           "period": "Miesięcznie",
           "type": "brutto"
          },
          "location": {
           "buildingNumber": "11",
           "street": "Krakowska",
           "city": "Warszawa",
           "postalCode": "01-210",
           "coordinates": {
            "latitude": 52.25102615356445,
            "longitude": 20.975749969482422
           },
           "isRemote": null,
           "isHybrid": true
          },
          "category": {
           "leadingCategory": "Informatyka",
           "subCategories": ["Programowanie", "Programowanie Backendowe"]
          },
          "requirements": {
           "skills": [
           {
            "skill": "PHP, HTML, CSS",
            "experienceMonths": null,
            "experienceLevel": ["Początkujący"]
           },
           {
            "skill": "WordPress, Shoper, PrestaShop",
            "experienceMonths": null,
            "experienceLevel": null
           },
           {
            "skill": "Make, N8N",I 
            "experienceMonths": 7,
            "experienceLevel": ["Początkujący"]
           }
           ],
           "education": ["Studia magisterskie", "Certyfikat CCNA"],
           "languages": [
            {
             "language": "Angielski",
             "level": "B2"
            }
           ]
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
          "benefits": [
           "Prywatna opieka medyczna",
           "Ubezpieczenie na życie",
           "Elastyczny czas pracy"
          ],
          "isUrgent": false,
          "isForUkrainians": false
         }
         """;

        /// <summary>
        /// Human-readable explanations for each UOS field to help the LLM understand expected values.
        /// </summary>
        private static readonly string _explanationsUosOffer = """
        `jobTitle`: *string* - Job title listed in offer.
        `company`: *object* - Issuing company details:  
         `name`: *string* - Company name.  
        `description`: *string|null* - Offer description, used for AI-powered tag extraction.
        `salary`: *object* - Job salary details:  
         `from`: *number|null* - Minimum salary.  
         `to`: *number|null* - Maximum salary.  
         `currency`: *string|null* - Salary currency.  
         `period`: *string|null* - Payment period.  
         `type`: *string|null* - Salary type.
        `location`: *object* - Work location:  
         `buildingNumber`: *string|null* - Building number.  
         `street`: *string|null* - Street name.  
         `city`: *string|null* - City.  
         `postalCode`: *string|null* - Postal code.  
         `coordinates`: *object* - Location coordinates:  
          `latitude`: *number|null* - Latitude, used to estimate distance.  
          `longitude`: *number|null* - Longitude, used to estimate distance.  
         `isRemote`: *boolean|null* - Indicates if the work is fully remote.  
         `isHybrid`: *boolean|null* - Indicates if the work is hybrid (partially remote, partially on-site).
        `category`: *object* - Offer category:  
         `leadingCategory`: *string* - Scientific major that the offer will best suit.  
         `subCategories`: *array|null* - Offer categories.
        `requirements`: *object* - Work requirements:  
         `skills`: *array|null* - Required skills.  
          `skill`: *string* - Skill name.  
          `experienceMonths`: *number|null* - Required months of experience.  
          `experienceLevel`: *array|null* - Required experience level.  
         `education`: *array|null* - Required education.  
         `languages`: *array|null* - Required languages.  
          `language`: *string* - Language name.  
          `level`: *string* - Language mastery level.
        `employment`: *object* - Employment details:  
         `types`: *array* - Type of employment contract.  
         `schedules`: *array* - Work schedule.
        `dates`: *object* - Offer lifecycle dates:  
         `published`: *string* - Offer publication date (format: YYYY-MM-DD HH:MI:SS).  
         `expires`: *string|null* - Offer expiration date (format: YYYY-MM-DD HH:MI:SS).
        `benefits`: *array|null* - Employee benefits offered by the company.  
        `isUrgent`: *boolean* - Indicates whether the company needs to hire urgently.  
        `isForUkrainians`: *boolean* - Indicates whether the position is mainly intended for Ukrainian applicants.
        """;

        /// <summary>
        /// Read-only map of UOS prompt resources keyed by identifier.
        /// </summary>
        internal static readonly FrozenDictionary<string, string> uosExamples = new Dictionary<string, string>()
        {
            { "UNIFIED-OFFER-SCHEMA", _emptyUosOffer },
            { "UNIFIED-OFFER-SCHEMA-EXPLANATIONS", _explanationsUosOffer },
            { "RESULT-EXAMPLE", _exampleUosOffer}
        }.ToFrozenDictionary();
    }
}
