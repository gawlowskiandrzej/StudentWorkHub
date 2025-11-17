using Ganss.Xss;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace OffersConnector
{
    internal class Helpers
    {
        internal static FrozenDictionary<string, List<string?>?> SplitSkills(List<Skills>? skills)
        {
            if (skills == null || skills.Count < 1)
                return new Dictionary<string, List<string?>?>() { 
                    { "skill", null },
                    { "experienceMonths", null },
                    { "experienceLevel", null }
                }.ToFrozenDictionary();

            List<string> skill_names = [];
            List<string?> skill_experience_months = [];
            List<string?> skill_experience_levels = [];

            foreach (Skills skill in skills)
            {
                skill_names.Add(SanitizeInput(skill.Skill));
                skill_experience_months.Add(SanitizeInput($"{skill.ExperienceMonths}"));
                if (skill.ExperienceLevel != null)
                    skill_experience_levels.Add(skill.ExperienceLevel.Count > 0 ? SanitizeInput(skill.ExperienceLevel[0]) : null);
                else
                    skill_experience_levels.Add(null);
            }

            return new Dictionary<string, List<string?>?>() {
                    { "skill", skill_names },
                    { "experienceMonths", skill_experience_months },
                    { "experienceLevel", skill_experience_levels }
                }.ToFrozenDictionary();
        }

        internal static FrozenDictionary<string, List<string?>?> SplitLanguages(List<Languages>? languages)
        {
            if (languages == null || languages.Count < 1)
                return new Dictionary<string, List<string?>?>() {
                    { "languages", null },
                    { "languageLevels", null }
                }.ToFrozenDictionary();

            List<string> language_names = [];
            List<string?> language_levels = [];

            foreach (Languages language in languages)
            {
                language_names.Add(SanitizeInput(language.Language));
                language_levels.Add(SanitizeInput(language.Level));
            }

            return new Dictionary<string, List<string?>?>() {
                { "languages", language_names },
                { "languageLevels", language_levels }
            }.ToFrozenDictionary();
        }

        internal static List<short?>? MapToShort(List<string?>? inputList)
        {
            if (inputList == null || inputList.Count < 1)
            {
                return null;
            }

            List<short?> resultList = inputList.Select(strValue =>
            {
                if (strValue == null)
                {
                    return (short?)null;
                }

                if (long.TryParse(strValue, out long parsedValue))
                {

                    if (parsedValue > short.MaxValue)
                    {
                        return short.MaxValue;
                    }

                    if (parsedValue < 0)
                    {
                        return (short)0;
                    }

                    return (short)parsedValue;
                }
                else
                {
                    return (short?)null;
                }
            }).ToList();

            return resultList;
        }

        internal static string? SanitizeInput(string? dirtyInput, bool allow_null = true)
        {
            if (string.IsNullOrWhiteSpace(dirtyInput))
            {
                return allow_null ? null : string.Empty;
            }

            HtmlSanitizer htmlSanitizer = new();
            string plainText = htmlSanitizer.Sanitize(dirtyInput);

            return plainText.Trim();
        }

        internal static List<string?>? SanitizeArray(
            List<string?>? dirtyInputs,
            bool allowNullArray = true,
            bool allowNullElements = true)
        {
            if (dirtyInputs == null || dirtyInputs.Count < 1)
            {
                return allowNullArray ? null : new();
            }

            return dirtyInputs
                .Select(input => SanitizeInput(input, allowNullElements))
                .ToList();
        }
    }
}
