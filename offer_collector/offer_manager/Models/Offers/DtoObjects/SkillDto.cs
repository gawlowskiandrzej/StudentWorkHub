namespace offer_manager.Models.Offers.dtoObjects
{
    public class SkillDto
    {
        public string Skill { get; set; } = string.Empty;
        public int? ExperienceMonths { get; set; }
        public List<string>? ExperienceLevel { get; set; }
    }
}