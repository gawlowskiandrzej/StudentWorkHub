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
            "skill": "Make, N8N", 
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
        /// Read-only map of UOS prompt resources keyed by identifier.
        /// </summary>
        internal static readonly FrozenDictionary<string, string> uosExamples = new Dictionary<string, string>()
        {
            { "RESULT-EXAMPLE", _exampleUosOffer}
        }.ToFrozenDictionary();
    }
}
