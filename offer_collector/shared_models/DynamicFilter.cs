using Offer_collector.Models;

namespace shared_models
{
    public class DynamicFilter
    {
        public List<string> LanguagesNames { get; set; } = new List<string>();
        public List<string> LanguagesLevels { get; set; } = new List<string>();
        public List<int?> ExperienceMonths { get; set; } = new List<int?>();
        public List<string> ExperienceLevels { get; set; } = new List<string>();
        public List<string> EducationNames { get; set; } = new List<string>();

        public List<string> SkillsList { get; set; } = new List<string>();
        public List<string> BenefitsList { get; set; } = new List<string>();

        public bool IsForUkrainians { get; set; }
        public bool IsUrgent { get; set; }
    }
}
