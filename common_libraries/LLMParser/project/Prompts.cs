using System.Collections.Frozen;
using System.Text.RegularExpressions;
using UnifiedOfferSchema;

namespace LLMParser
{
    /// <summary>
    /// Provides predefined prompt templates and helper utilities for building system/user prompts.
    /// Placeholders in the form <c>[{KEY}]</c> are filled with values from predefined components and from <see cref="Settings.Prompt.systemPromptParameters"/> and <see cref="Settings.Prompt.userPromptParameters"/> .
    /// </summary>
    public sealed partial class Prompts
    {
        /// <summary>
        /// Base system prompt template containing placeholders to be replaced with ROLE, RULES, TASK, RESTRICTIONS,
        /// the Unified Offer Schema, its explanations, and a result example.
        /// </summary>
        public static readonly string defaultSystemPromptTemplate = """
        ## ROLE: ##
        [{ROLE}]
    
        ## CONTEXT: ##
        `Unified Offer Schema`:
        [{UNIFIED-OFFER-SCHEMA}]
   
        `Unified Offer Schema field explanations`:
        [{UNIFIED-OFFER-SCHEMA-EXPLANATIONS}]

        `Result Example`:
        [{RESULT-EXAMPLE}]

        ## RULES: ##
        [{RULES}]
        
        ## RESTRICTIONS: ##
        [{RESTRICTIONS}]

        ## TASK: ##
        [{TASK}]
        """;

        /// <summary>
        /// User prompt template; requires <c>[{USER-OFFER}]</c> to be injected with a UOS JSON derived from the raw offer.
        /// </summary>
        public static readonly string descriptionInformationExtratorUserPrompt = """
        ## USER OFFER: ##
        [{USER-OFFER}]
        """;

        /// <summary>
        /// Role description for the information-extractor agent used in the system prompt.
        /// </summary>        
        public static readonly string descriptionInformationExtratorRole = """
        **You are a strict information extractor.**
        You **MUST** read the user-provided offer description and output **ONE raw JSON object** that matches **EXACTLY** the structure of `Unified Offer Schema` below, filled with informations extracted from provided description.
        Do **NOT** use external knowledge.
        Output **MUST** be valid **JSON** with double quotes, no comments, no trailing commas, no markdown fences, no extra text.
        """;


        /// <summary>
        /// Rule set that constrains how the model should parse and normalize the offer content.
        /// </summary>  
        public static readonly string descriptionInformationExtratorRules = """
        - You **MUST** read **CONTEXT** section before proceeding to parse **USER OFFER**.
        - You **MUST** follow any present restrictions in **RESTRICTIONS** section.
        - Use **ONLY** facts present in the description given by the user. Never invent new facts, numbers, dates or entities that are not supported by the text.
        - Do **NOT** infer or guess missing facts for free-text fields (titles, descriptions, benefits, etc.).  
        - **Exception – fields with standardized values (see RESTRICTIONS):**  
         - For these fields you MUST normalize, shorten or generalize the input text so that the final value is exactly one of the allowed standardized values.  
         - This normalization is considered **reformatting**, not inventing, even if the exact standardized value does not appear literally in the text.
        - If a value for a field with standardized values is not explicitly present AND you cannot clearly map the text to any of the allowed values, you MUST use a generic option like "Inny" / "Other" if it exists for that field, otherwise use the default value from `Unified offer schema`.
        - You are **ALLOWED** to reformat facts to match format required by `Unified Offer Schema` (e.g. "zł" -> "PLN").
        - You are **ALLOWED** to provide informations based on context. 
        - When reformating facts analyze them for additional potential informations to fill its surrounding (e.g. 
        "skills":[
        {
         "skill": "Minimum of 5 years of experience as a Python Developer, with a strong foundation in developing scalable backend solutions.",
         "experienceMonths": null,
         "experienceLevel": null
        }] 
        ->
        "skills":[
        {
         "skill": "Python, Backend",
         "experienceMonths": 60,
         "experienceLevel": ["Średniozaawansowany"]
        }]).
        - When reformating facts for fields with standardized values, you MUST prefer a clean standardized label over copying the whole original sentence or phrase.  
        - Example:  
         - Input: `"Pełny etat - standardowe godziny"`  
          Allowed values include `"Pełny etat"`  
          -> You **MUST** output `"Pełny etat"` (without `- standardowe godziny`).
        - Output **MUST** be a **single JSON** object contained in '{}' tags, exactly matching the schema keys and order above. No explanations or additional text.
        - All keys present in the `Unified Offer Schema` must be present, not modified, and not duplicated (except for list's objects).
        - Skill names should be reformated to valid selection of names , instead of whole raw sentences.
        - Diploma requirements should be in `education` section **NOT** in `skills` section.
        - Language requirements should be in `languages` section **NOT** in `skills` section.
        - **All** fields values **MUST** be translated to/written in Polish language **regardless** of source offer language using proper polish specific letters, except for proper names.
        - Offer titles **MUST** also be translated to/written in Polish language **regardless** of source offer language.
        - **ALL** Fields values **MUST** be **SANITIZED** (sanitized meaning, leaving only normal text).
        - If skill `experienceLevel` is not explicitly present, try to determine required skill level using offer `description` and required experienceMonths.
        - **DON'T** attempt to fill experienceMonth if required experience time (years, month, days, etc.) is not present explicitly in the description, or already provided by the user.
        - Each skill and language can be mentioned only **ONCE**.
        - `skill.experienceLevel` must be a **list** or **null** type.
        - In case of multiple levels (skill level or language level) choose the **HIGHEST** one occuring.
        """;

        /// <summary>
        /// Task instructions describing how to fill the Unified Offer Schema from the provided user offer.
        /// </summary>
        public static readonly string descriptionInformationExtratorTask = """
        Read the offer description in the `Unified Offer Schema` provided by the user, and try to fill out **USER OFFER** by following **RULES**. Use **CONTEXT** for support and values guidance.
        """;

        /// <summary>
        /// Additional restrictions enforcing normalized values (e.g., language levels, skill experience).
        /// </summary>
        public static readonly string descriptionInformationExtratorRestrictions = """
        - Language level **MUST** use standarised values { 'A1', 'A2', 'B1', 'B2', 'C1', 'C2' }.
        - Skill experienceLevel **MUST** use standarised values { 'Początkujący', 'Średniozaawansowany', 'Zaawansowany' }.
        - Leading category is a scientific major that the offer will best suit, so it should reflect a real scientific major e.g. 'Informatyka', 'Psychologia', 'Medycyna'. It's **NOT** a field of study e.g. 'Programming', 'Networking'. 
        """;

        /// <summary>
        /// Predefined mapping for system prompt components (ROLE, RULES, TASK, RESTRICTIONS) used to assemble the system prompt.
        /// </summary>
        public static readonly Dictionary<string, string> descriptionInformationExtratorSystemPredefinedSet = new()
        {
            { "ROLE", descriptionInformationExtratorRole },
            { "RULES", descriptionInformationExtratorRules },
            { "TASK", descriptionInformationExtratorTask },
            { "RESTRICTIONS", descriptionInformationExtratorRestrictions}
        };

        /// <summary>
        /// Required reserved system-prompt keys that must appear in the system template:
        /// <c>UNIFIED-OFFER-SCHEMA</c>, <c>UNIFIED-OFFER-SCHEMA-EXPLANATIONS</c>, <c>RESULT-EXAMPLE</c>.
        /// </summary>
        private static readonly FrozenSet<string> _protectedSystemPromptKeys = new[] { "UNIFIED-OFFER-SCHEMA", "UNIFIED-OFFER-SCHEMA-EXPLANATIONS", "RESULT-EXAMPLE" }.ToFrozenSet();

        /// <summary>
        /// Required reserved user-prompt key that must appear in the user template: <c>USER-OFFER</c>.
        /// </summary>
        private static readonly FrozenSet<string> _protectedUserPromptKeys = new[] { "USER-OFFER" }.ToFrozenSet();

        /// <summary>
        /// Builds concrete system and user prompts by substituting all <c>[{KEY}]</c> placeholders with values
        /// from predefined components and the provided <paramref name="promptSettings"/>.
        /// </summary>
        /// <param name="offer">Raw offer text to parse; converted to a UOS JSON (pretty-printed) and injected as <c>[{USER-OFFER}]</c>.</param>
        /// <param name="promptSettings">Prompt configuration with templates and parameter dictionaries for substitution.</param>
        /// <returns>An immutable dictionary with two entries: <c>"system"</c> and <c>"user"</c> prompts.</returns>
        /// <exception cref="ParserException">
        /// Thrown when required placeholders are missing, required parameters are not provided,
        /// or when <paramref name="offer"/> is null/whitespace.
        /// </exception>
        /// <remarks>
        /// Required keys in system template: <c>[{UNIFIED-OFFER-SCHEMA}]</c>, <c>[{UNIFIED-OFFER-SCHEMA-EXPLANATIONS}]</c>, <c>[{RESULT-EXAMPLE}]</c>.
        /// Required key in user template: <c>[{USER-OFFER}]</c>.
        /// Placeholders formatted as <c>[{KEY}]</c> are replaced by values from <see cref="Settings.Prompt.systemPromptParameters"/>
        /// and <see cref="Settings.Prompt.userPromptParameters"/> for non-protected keys.
        /// </remarks>
        internal static FrozenDictionary<string, string> BuildPrompts(string offer, Settings.Prompt promptSettings)
        {
            if (string.IsNullOrWhiteSpace(offer))
                throw new ParserException("Offer is empty.");
            
            string localSystemPrompt = promptSettings.systemPrompt;
            FrozenSet<string> systemPromptKeys = ExtractPromptKeys(promptSettings.systemPrompt);
            foreach (var key in _protectedSystemPromptKeys)
            {
                if (!systemPromptKeys.Contains(key))
                    throw new ParserException($"systemPrompt must contain [{{{key}}}]");

                localSystemPrompt = localSystemPrompt.Replace($"[{{{key}}}]", UosExamples.uosExamples[key]);
            }

            foreach (var key in systemPromptKeys.Except(_protectedSystemPromptKeys))
            {
                if (!promptSettings.systemPromptParameters.TryGetValue(key, out string? value))
                    throw new ParserException($"systemPromptParameters is missing a {key} key.");

                localSystemPrompt = localSystemPrompt.Replace($"[{{{key}}}]", value);
            }

            // Remove keys and use format with intdented format
            string userOffer = MultipleWhitespaceRegex().Replace(UOSUtils.BuildFromString(offer).AsString(true, ["id", "source", "url", "logoUrl"]), " ");
            FrozenDictionary<string, string> internalUserParameters = new Dictionary<string, string>() 
            {
                { "USER-OFFER", userOffer}
            }.ToFrozenDictionary();

            string localUserPrompt = promptSettings.userPrompt;
            FrozenSet<string> userPromptKeys = ExtractPromptKeys(promptSettings.userPrompt);
            foreach (var key in _protectedUserPromptKeys)
            {
                if (!userPromptKeys.Contains(key))
                    throw new ParserException($"userPrompt must contain [{{{key}}}]");

                localUserPrompt = localUserPrompt.Replace($"[{{{key}}}]", internalUserParameters[key]);
            }

            foreach (var key in userPromptKeys.Except(_protectedUserPromptKeys))
            {
                if (!promptSettings.userPromptParameters.TryGetValue(key, out string? value))
                    throw new ParserException($"userPromptParameters is missing a {key} key.");

                localUserPrompt = localUserPrompt.Replace($"[{{{key}}}]", value);
            }

            // Assemble the final prompts as an immutable dictionary.
            return new Dictionary<string, string>()
            {
                { "system", localSystemPrompt },
                { "user", localUserPrompt }
            }.ToFrozenDictionary();
        }

        /// <summary>
        /// Extracts unique placeholder keys from a template string in the form <c>[{KEY}]</c>.
        /// </summary>
        /// <param name="input">Template text to scan.</param>
        /// <returns>A set of distinct keys found in the template (case-sensitive, culture-invariant).</returns>
        private static FrozenSet<string> ExtractPromptKeys(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Enumerable.Empty<string>().ToFrozenSet(StringComparer.Ordinal);

            var matches = PromptKeyFilter().Matches(input);
            return matches
                .Select(m => m.Groups["value"].Value.Trim())
                .Where(v => v.Length > 0)
                .ToFrozenSet(StringComparer.Ordinal);
        }

        /// <summary>
        /// Compiled regular expression that matches prompt placeholders like <c>[{KEY}]</c> and captures <c>KEY</c> as <c>value</c>.
        /// </summary>
        /// <returns>A compiled <see cref="Regex"/> for placeholder extraction.</returns>
        [GeneratedRegex(@"\[\{\s*(?<value>[^{}\[\]]+?)\s*\}\]", RegexOptions.CultureInvariant)]
        private static partial Regex PromptKeyFilter();

        [GeneratedRegex(@"\s+", RegexOptions.CultureInvariant)]
        private static partial Regex MultipleWhitespaceRegex();
    }
}
