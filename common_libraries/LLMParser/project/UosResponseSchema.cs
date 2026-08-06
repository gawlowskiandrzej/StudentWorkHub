using System.Collections.Frozen;

namespace LLMParser
{
    /// <summary>
    /// Builds the Gemini <c>responseSchema</c> describing a Unified Offer Schema (UOS) offer.
    /// Fields backed by a database dictionary are constrained with an <c>enum</c>, so the model
    /// cannot emit a value outside the allowed set. Fields without a dictionary stay free-form.
    /// </summary>
    internal static class UosResponseSchema
    {
        /// <summary>
        /// Dictionary keys as returned by the <c>public.get_dictionaries_text()</c> database function.
        /// </summary>
        private const string experienceLevelKey = "skill.experienceLevel";
        private const string languageKey = "languages.language";
        private const string languageLevelKey = "languages.level";
        private const string currencyKey = "salary.currency";
        private const string salaryPeriodKey = "salary.period";
        private const string employmentTypeKey = "employment.types";
        private const string employmentScheduleKey = "employment.schedules";
        private const string leadingCategoryKey = "category.leadingCategory";

        /// <summary>
        /// Builds the full UOS response schema.
        /// </summary>
        /// <param name="dictionaries">
        /// Standardized values keyed by UOS field path, as returned by the database connector.
        /// Missing or empty entries leave the matching field unconstrained.
        /// </param>
        /// <returns>Anonymous-style object graph to be serialized as <c>generationConfig.responseSchema</c>.</returns>
        internal static object Build(FrozenDictionary<string, List<string>>? dictionaries)
        {
            Dictionary<string, object> salary = Obj(
                "Job salary details.",
                new()
                {
                    ["from"] = Number("Minimum salary.", nullable: true),
                    ["to"] = Number("Maximum salary.", nullable: true),
                    ["currency"] = Text("Salary currency.", nullable: true, allowedValues: AllowedValues(dictionaries, currencyKey)),
                    ["period"] = Text("Payment period.", nullable: true, allowedValues: AllowedValues(dictionaries, salaryPeriodKey)),
                    ["type"] = Text("Salary type, for example gross or net.", nullable: true)
                });

            Dictionary<string, object> location = Obj(
                "Work location.",
                new()
                {
                    ["buildingNumber"] = Text("Building number.", nullable: true),
                    ["street"] = Text("Street name.", nullable: true),
                    ["city"] = Text("City.", nullable: true),
                    ["postalCode"] = Text("Postal code.", nullable: true),
                    ["coordinates"] = Obj(
                        "Location coordinates.",
                        new()
                        {
                            ["latitude"] = Number("Latitude.", nullable: true),
                            ["longitude"] = Number("Longitude.", nullable: true)
                        }),
                    ["isRemote"] = Bool("True when the work is fully remote.", nullable: true),
                    ["isHybrid"] = Bool("True when the work is hybrid, partially remote and partially on-site.", nullable: true)
                });

            // leadingCategory is generated before subCategories so the model picks sub categories that fit it.
            Dictionary<string, object> category = Obj(
                "Offer category.",
                new()
                {
                    ["leadingCategory"] = Text(
                        "Scientific major the offer best suits, for example Informatyka or Medycyna. Not a narrow field such as Programowanie.",
                        allowedValues: AllowedValues(dictionaries, leadingCategoryKey)),
                    ["subCategories"] = Array("Narrower categories belonging to the leading category.", Text("Sub category name."), nullable: true)
                },
                required: ["leadingCategory"]);

            Dictionary<string, object> skill = Obj(
                "Single required skill.",
                new()
                {
                    ["skill"] = Text("Skill name or a short comma-separated selection of names, not a full sentence."),
                    ["experienceMonths"] = Number("Required experience in months. Null unless the offer states an amount of time.", nullable: true),
                    ["experienceLevel"] = Array(
                        "Required experience level. Derived from experienceMonths and description when the offer does not name it.",
                        Text("Experience level.", allowedValues: AllowedValues(dictionaries, experienceLevelKey)),
                        nullable: true)
                },
                required: ["skill"]);

            Dictionary<string, object> language = Obj(
                "Single required language.",
                new()
                {
                    ["language"] = Text("Language name.", allowedValues: AllowedValues(dictionaries, languageKey)),
                    ["level"] = Text("Language mastery level.", allowedValues: AllowedValues(dictionaries, languageLevelKey))
                },
                required: ["language", "level"]);

            Dictionary<string, object> requirements = Obj(
                "Work requirements.",
                new()
                {
                    ["skills"] = Array("Required skills, each listed once.", skill, nullable: true),
                    ["education"] = Array("Required diplomas, degrees and certificates.", Text("Education requirement."), nullable: true),
                    ["languages"] = Array("Required languages, each listed once.", language, nullable: true)
                });

            Dictionary<string, object> employment = Obj(
                "Employment details.",
                new()
                {
                    ["types"] = Array("Employment contract types.", Text("Contract type.", allowedValues: AllowedValues(dictionaries, employmentTypeKey))),
                    ["schedules"] = Array("Work schedules.", Text("Work schedule.", allowedValues: AllowedValues(dictionaries, employmentScheduleKey)))
                });

            Dictionary<string, object> dates = Obj(
                "Offer lifecycle dates.",
                new()
                {
                    ["published"] = Text("Publication date in YYYY-MM-DD HH:MI:SS format."),
                    ["expires"] = Text("Expiration date in YYYY-MM-DD HH:MI:SS format.", nullable: true)
                },
                required: ["published"]);

            return Obj(
                description: "",
                new()
                {
                    ["jobTitle"] = Text("Job title, written in Polish."),
                    ["company"] = Obj(
                        "Issuing company details.",
                        new() { ["name"] = Text("Company name.") },
                        required: ["name"]),
                    ["description"] = Text("Offer description. Primary source for fields the offer leaves empty.", nullable: true),
                    ["salary"] = salary,
                    ["location"] = location,
                    ["category"] = category,
                    ["requirements"] = requirements,
                    ["employment"] = employment,
                    ["dates"] = dates,
                    ["benefits"] = Array("Employee benefits offered by the company.", Text("Benefit name."), nullable: true),
                    ["isUrgent"] = Bool("True when the company needs to hire urgently."),
                    ["isForUkrainians"] = Bool("True when the position is mainly intended for Ukrainian applicants.")
                },
                required: ["jobTitle", "company", "category", "requirements", "employment", "dates", "isUrgent", "isForUkrainians"]);
        }

        /// <summary>
        /// Returns the standardized values for a dictionary key, or null when the key is absent or empty.
        /// </summary>
        private static IReadOnlyList<string>? AllowedValues(FrozenDictionary<string, List<string>>? dictionaries, string key)
        {
            if (dictionaries is null || !dictionaries.TryGetValue(key, out List<string>? values) || values is null || values.Count < 1)
                return null;

            return values;
        }

        private static Dictionary<string, object> Text(string description, bool nullable = false, IReadOnlyList<string>? allowedValues = null)
        {
            Dictionary<string, object> field = Leaf("STRING", description, nullable);
            if (allowedValues is not null)
                field["enum"] = allowedValues;

            return field;
        }

        private static Dictionary<string, object> Number(string description, bool nullable = false)
        {
            return Leaf("NUMBER", description, nullable);
        }

        private static Dictionary<string, object> Bool(string description, bool nullable = false)
        {
            return Leaf("BOOLEAN", description, nullable);
        }

        private static Dictionary<string, object> Leaf(string type, string description, bool nullable)
        {
            Dictionary<string, object> field = new() { ["type"] = type };
            if (!string.IsNullOrWhiteSpace(description))
                field["description"] = description;
            if (nullable)
                field["nullable"] = true;

            return field;
        }

        private static Dictionary<string, object> Array(string description, Dictionary<string, object> items, bool nullable = false)
        {
            Dictionary<string, object> field = Leaf("ARRAY", description, nullable);
            field["items"] = items;

            return field;
        }

        private static Dictionary<string, object> Obj(string description, Dictionary<string, object> properties, IReadOnlyList<string>? required = null, bool nullable = false)
        {
            Dictionary<string, object> field = Leaf("OBJECT", description, nullable);
            // Properties are generated in this order, so fields that inform later ones are declared first.
            field["propertyOrdering"] = properties.Keys.ToArray();
            field["properties"] = properties;
            if (required is not null)
                field["required"] = required;

            return field;
        }
    }
}