using Ganss.Xss;
using System.Collections.Frozen;
using System.Globalization;
using UnifiedOfferSchema;

namespace OffersConnector
{
    internal class Helpers
    {
        internal static FrozenDictionary<string, ValueTuple<string, string>> SplitLinks(List<string> links, bool no_throw = false)
        {
            if (links.Count < 1)
                return FrozenDictionary<string, (string, string)>.Empty;

            List<string> unresolved_links = new();
            Dictionary<string, ValueTuple<string, string>> result = new();
            Dictionary<string, int> part_counters = new();
            int valid_offers = 0;
            foreach (string link in links)
            {
                if (string.IsNullOrWhiteSpace(link)) continue;

                valid_offers++;
                List<string> link_split = [.. link.Trim().Split('/')];
                foreach (string part in link_split)
                {
                    if (!part_counters.ContainsKey(part))
                        part_counters[part] = 1;
                    else
                        part_counters[part]++;
                }

                string base_link = "";
                foreach (string part in link_split)
                {
                    if (part_counters[part] > 1)
                        base_link += $"{part}/";
                    else
                        break;
                }

                if (string.IsNullOrEmpty(base_link))
                    unresolved_links.Add(link);
                else
                    result[link] = (SanitizeInput(base_link), SanitizeInput(link.Replace(base_link, "")));
            }

            foreach (string link in unresolved_links)
            {
                List<string> link_split = [.. link.Trim().Split('/')];
                string base_link = "";
                foreach (string part in link_split)
                {
                    if (part_counters[part] > 1)
                        base_link += $"{part}/";
                    else
                        break;
                }
                if (!string.IsNullOrEmpty(base_link))
                    result[link] = (SanitizeInput(base_link), SanitizeInput(link.Replace(base_link, "")));
            }

            if (result.Count != valid_offers && !no_throw)
                throw new PgConnectorException("Failed to split links");

            return result.ToFrozenDictionary();
        }

        internal static FrozenDictionary<string, List<string?>?> SplitSkills(List<Skills>? skills)
        {
            if (skills == null)
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
                if (skill.ExperienceMonths != null)
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
            if (languages == null)
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
            if (inputList == null)
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
            if (dirtyInputs == null)
            {
                return allowNullArray ? null : new();
            }

            return dirtyInputs
                .Select(input => SanitizeInput(input, allowNullElements))
                .ToList();
        }
    }
}
