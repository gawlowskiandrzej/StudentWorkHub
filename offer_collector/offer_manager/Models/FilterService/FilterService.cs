using shared_models;
using System.Collections.Frozen;
using UnifiedOfferSchema;

namespace offer_manager.Models.FilterService
{
    public class FilterService
    {
        public DynamicFilter GetDynamicFilters(FrozenSet<UOS?> offers)
        {

            return new DynamicFilter
            {
                EducationNames = offers.Where(o => o != null && o.Requirements?.Education != null).SelectMany(o => o.Requirements?.Education!).Distinct().ToList(),
                ExperienceLevels = offers.Where(o => o != null && o.Requirements?.Skills != null).SelectMany(_ => _.Requirements?.Skills!).Where(_ => _.ExperienceLevel != null).SelectMany(p => p.ExperienceLevel!).Distinct().ToList(),
                Languages = offers
                .Where(o => o?.Requirements?.Languages != null)
                .SelectMany(o => o.Requirements!.Languages!)
                .DistinctBy(l => new { l.Language, l.Level })
                .ToList()
            };
        }
    }
}
