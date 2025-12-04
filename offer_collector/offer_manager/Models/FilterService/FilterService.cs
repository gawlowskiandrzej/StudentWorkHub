using shared_models;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace offer_manager.Models.FilterService
{
    public class FilterService
    {
        public DynamicFilter GetDynamicFilters(FrozenSet<UOS?> offers)
        {
            List<string> skillList = offers
                .Where(o => o?.Requirements?.Skills != null)
                .SelectMany(o => o.Requirements?.Skills!)
                .Where(s => s?.Skill != null)
                .Select(s => s.Skill!)
                .Distinct()
                .ToList();

            List<string> benefitsList = offers.Where(o => o != null && o.Benefits != null)
                .SelectMany(o => o.Benefits!)
                .Distinct()
                .ToList();

            return new DynamicFilter
            {
                SkillsList = skillList,
                BenefitsList = benefitsList,
                EducationNames = offers.Where(o => o != null && o.Requirements?.Education != null).SelectMany(o => o.Requirements?.Education!).Distinct().ToList(),
                ExperienceLevels = offers.Where(o => o != null && o.Requirements?.Skills != null).SelectMany(_ => _.Requirements?.Skills!).Where(_ => _.ExperienceLevel != null).SelectMany(p => p.ExperienceLevel!).Distinct().ToList(),   
                ExperienceMonths = offers
                .Where(o => o != null && o.Requirements?.Skills != null)
                .SelectMany(_ => _.Requirements?.Skills!)
                .Where(p => p.ExperienceMonths != null)
                .Select(p => p.ExperienceMonths!)
                .Distinct()
                .ToList(),
                LanguagesLevels = offers.Where(o => o != null && o.Requirements.Languages != null)
                    .SelectMany(o => o.Requirements?.Languages!)
                    .Select(l => l.Level!)
                    .Where(l => !string.IsNullOrEmpty(l))
                    .Distinct()
                    .ToList(),
                LanguagesNames = offers.Where(o => o != null && o.Requirements?.Languages != null)
                    .SelectMany(o => o.Requirements?.Languages!)
                    .Select(l => l.Language!)
                    .Where(l => !string.IsNullOrEmpty(l))
                    .Distinct()
                    .ToList()
            };
        }
    }
}
