using System.Text.RegularExpressions;

namespace Offer_collector.Models.Tools
{
    public class RequirementsParser
    {
        public static Requirements ParseRequirements(List<string> bullets)
        {
            var requirements = new Requirements();
            requirements.skills = new List<Skill>();

            foreach (var bullet in bullets)
            {
                requirements?.skills?.Add(new Skill { skill = bullet });
            }

            return requirements ?? new Requirements();
        }
    }
}
