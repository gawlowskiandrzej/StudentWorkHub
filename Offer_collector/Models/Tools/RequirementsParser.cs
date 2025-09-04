using System.Text.RegularExpressions;

namespace Offer_collector.Models.Tools
{
    public class RequirementsParser
    {
        public static Requirements ParseRequirements(List<string> bullets)
        {
            var requirements = new Requirements();

            foreach (var bullet in bullets)
            {
                // Regex dla experience years, np. "minimum X lat"
                var yearsMatch = Regex.Match(bullet, @"doświadczenie.*minimum (\d+) lat", RegexOptions.IgnoreCase);
                if (yearsMatch.Success)
                {
                    requirements.experienceYears = ushort.Parse(yearsMatch.Groups[1].Value);
                    requirements.experienceLevel.Add("komercyjne"); // Dodaj poziom na podstawie kontekstu
                    continue;
                }

                // Regex dla education, np. słowa kluczowe związane z wykształceniem
                var educationMatch = Regex.Match(bullet, @"wykształcenie|edukacja|studia|dyplom|magister|inżynier", RegexOptions.IgnoreCase);
                if (educationMatch.Success)
                {
                    requirements.education.Add(bullet);
                    continue;
                }

                // Regex dla languages (języki obce), np. "język" ale nie "programowania"
                var languageMatch = Regex.Match(bullet, @"język|language|znajomość języka", RegexOptions.IgnoreCase);
                if (languageMatch.Success && !bullet.ToLower().Contains("programowania"))
                {
                    requirements.languages.Add(bullet);
                    continue;
                }

                // Domyślnie dodaj do skills
                requirements.skills.Add(bullet);
            }

            return requirements;
        }
    }
}
