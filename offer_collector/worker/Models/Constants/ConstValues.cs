using worker.Models.Tools;

namespace worker.Models.Constants
{
    static class ConstValues
    {
        public static readonly int delayBetweenRequests = 100;
        public static List<PlCityObject> polishCities = new();
        public static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.FullName ?? "";
    }
}
