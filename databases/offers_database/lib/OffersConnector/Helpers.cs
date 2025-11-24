using Ganss.Xss;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace OffersConnector
{
    /// <summary>
    /// Provides helper methods for sanitizing unified offer data and shaping it for database operations.
    /// </summary>
    internal class Helpers
    {
        /// <summary>
        /// Flattens a list of UOS skill objects into three parallel, sanitized lists keyed by column name.
        /// </summary>
        /// <param name="skills">Collection of skills from the unified offer schema; may be null or empty.</param>
        /// <returns>
        /// Frozen dictionary with keys "skill", "experienceMonths" and "experienceLevel".
        /// When <paramref name="skills"/> is null or empty, all keys are present with null values to preserve shape.
        /// Skill entries whose sanitized name is empty are skipped together with corresponding experience data.
        /// </returns>
        internal static FrozenDictionary<string, List<string?>?> SplitSkills(List<Skills>? skills)
        {
            if (skills is null || skills.Count < 1)
                return new Dictionary<string, List<string?>?>() { 
                    { "skill", null },
                    { "experienceMonths", null },
                    { "experienceLevel", null }
                }.ToFrozenDictionary();

            List<string?> skillNames = [];
            List<string?> skillExperienceMonths = [];
            List<string?> skillExperienceLevels = [];

            foreach (Skills skill in skills)
            {
                string? sanitizedSkillName = SanitizeInput(skill.Skill);
                if (string.IsNullOrWhiteSpace(sanitizedSkillName)) 
                    continue;

                skillNames.Add(sanitizedSkillName);

                skillExperienceMonths.Add(SanitizeInput($"{skill.ExperienceMonths}"));

                if (skill.ExperienceLevel is null)
                    skillExperienceLevels.Add(null);
                else
                    skillExperienceLevels.Add(skill.ExperienceLevel.Count > 0 ? SanitizeInput(skill.ExperienceLevel[0]) : null);
            }

            return new Dictionary<string, List<string?>?>() {
                { "skill", skillNames },
                { "experienceMonths", skillExperienceMonths },
                { "experienceLevel", skillExperienceLevels }
            }.ToFrozenDictionary();
        }

        /// <summary>
        /// Flattens a list of language entries into parallel lists of sanitized language names and levels.
        /// </summary>
        /// <param name="languages">Collection of languages from the unified offer schema; may be null or empty.</param>
        /// <returns>
        /// Frozen dictionary with keys "languages" and "languageLevels".
        /// When <paramref name="languages"/> is null or empty, all keys are present with null values to preserve shape.
        /// Language entries whose sanitized name is empty are skipped together with their level.
        /// </returns>
        internal static FrozenDictionary<string, List<string?>?> SplitLanguages(List<Languages>? languages)
        {
            if (languages is null || languages.Count < 1)
                return new Dictionary<string, List<string?>?>() {
                    { "languages", null },
                    { "languageLevels", null }
                }.ToFrozenDictionary();

            List<string?> languageNames = [];
            List<string?> languageLevels = [];

            foreach (Languages language in languages)
            {
                string? sanitizedLanguageName = SanitizeInput(language.Language);
                if (string.IsNullOrWhiteSpace(sanitizedLanguageName))
                    continue;

                languageNames.Add(sanitizedLanguageName);

                languageLevels.Add(SanitizeInput(language.Level));
            }

            return new Dictionary<string, List<string?>?>() {
                { "languages", languageNames },
                { "languageLevels", languageLevels }
            }.ToFrozenDictionary();
        }

        /// <summary>
        /// Extracts latitude and longitude from a location object, returning both keys even when data is missing.
        /// </summary>
        /// <param name="location">Location object from the unified offer schema; may be null.</param>
        /// <returns>
        /// Frozen dictionary with keys "latitude" and "longitude".
        /// When <paramref name="location"/> or its coordinates are null, both values are null.
        /// </returns>
        internal static FrozenDictionary<string, double?> GetCoordinates(Location? location)
        {
            if (location is null)
                return new Dictionary<string, double?>() {
                    { "latitude", null },
                    { "longitude", null }
                }.ToFrozenDictionary();

            Coordinates? coordinates = location.Coordinates;

            if (coordinates is null)
                return new Dictionary<string, double?>() {
                    { "latitude", null },
                    { "longitude", null }
                }.ToFrozenDictionary();

            return new Dictionary<string, double?>() {
                { "latitude", coordinates.Latitude },
                { "longitude", coordinates.Longitude }
            }.ToFrozenDictionary();
        }

        /// <summary>
        /// Attempts to convert a list of numeric strings into a list of nullable <see cref="short"/> values.
        /// </summary>
        /// <param name="inputList">
        /// Optional list of numeric strings. Items that cannot be parsed are mapped to null.
        /// </param>
        /// <returns>
        /// List of nullable <see cref="short"/> values, or null when <paramref name="inputList"/> is null or empty.
        /// Parsed values are clamped to the range [0, <see cref="short.MaxValue"/>] to avoid overflow.
        /// </returns>
        internal static List<short?>? MapToShort(List<string?>? inputList)
        {
            if (inputList is null || inputList.Count < 1)
                return null;

            List<short?> resultList = [.. inputList.Select(strValue =>
            {
                if (strValue is null)
                    return (short?)null;

                if (long.TryParse(strValue, out long parsedValue))
                    return (short)Math.Clamp(parsedValue, 0L, short.MaxValue);
                else
                    return null;
            })];

            return resultList;
        }

        /// <summary>
        /// Sanitizes a potentially unsafe string using an HTML sanitizer and trims the result.
        /// </summary>
        /// <param name="dirtyInput">Input string that may contain HTML or unsafe content.</param>
        /// <param name="allowNull">
        /// When true, whitespace-only or null input is returned as null; when false, an empty string is returned instead.
        /// </param>
        /// <returns>
        /// Sanitized and trimmed string, null, or an empty string depending on <paramref name="allowNull"/> and input content.
        /// </returns>
        internal static string? SanitizeInput(string? dirtyInput, bool allowNull = true)
        {
            if (string.IsNullOrWhiteSpace(dirtyInput))
            {
                return allowNull ? null : string.Empty;
            }

            HtmlSanitizer htmlSanitizer = new();
            string plainText = htmlSanitizer.Sanitize(dirtyInput);

            return plainText.Trim();
        }

        /// <summary>
        /// Sanitizes each element of a string list using <see cref="SanitizeInput(string?, bool)"/> with consistent null handling.
        /// </summary>
        /// <param name="dirtyInputs">List of strings that may contain HTML or unsafe content; may be null.</param>
        /// <param name="allowNullArray">
        /// When true and <paramref name="dirtyInputs"/> is null or empty, the result is null; otherwise an empty list is returned.
        /// </param>
        /// <param name="allowNullElements">
        /// Passed through to <see cref="SanitizeInput(string?, bool)"/> to control per-element null vs empty string behavior.
        /// </param>
        /// <returns>
        /// List of sanitized strings, null, or an empty list, depending on <paramref name="allowNullArray"/> and input content.
        /// </returns>
        internal static List<string?>? SanitizeArray(
            List<string?>? dirtyInputs,
            bool allowNullArray = true,
            bool allowNullElements = true)
        {
            if (dirtyInputs is null || dirtyInputs.Count < 1)
            {
                return allowNullArray ? null : new();
            }

            return [.. dirtyInputs.Select(input => SanitizeInput(input, allowNullElements))];
        }

        /// <summary>
        /// Records an error for a specific batch index and clears the corresponding input slot.
        /// </summary>
        /// <param name="results">
        /// Shared dictionary used to collect <see cref="BatchResult"/> instances from parallel workers.
        /// </param>
        /// <param name="inputData">
        /// Array of inputs passed to the batch operation; the entry at <paramref name="idx"/> is set to null to skip DB processing.
        /// </param>
        /// <param name="idx">Zero-based index of the failed item in the original batch.</param>
        /// <param name="message">Error message describing the failure.</param>
        /// <returns>
        /// A completed <see cref="ValueTask"/> so that callers can short-circuit asynchronous workflows with a single return statement.
        /// </returns>
        internal static ValueTask SetErrorResult(
            ConcurrentDictionary<int, BatchResult> results,
            ExternalOfferUosInput?[] inputData,
            int idx,
            string message)
        {
            results.TryAdd(idx, new BatchResult(idx, null, null, message));
            inputData[idx] = null;
            return ValueTask.CompletedTask;
        }
    }
}
