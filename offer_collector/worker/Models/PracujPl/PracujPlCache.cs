using Newtonsoft.Json.Linq;

namespace Offer_collector.Models.PracujPl
{
    public static class CompanyCache
    {
        private static readonly Dictionary<string, JToken> _cache = new();

        public static async Task<JToken?> GetOrAddAsync(string uri, Func<Task<JToken?>> factory)
        {
            if (_cache.TryGetValue(uri, out var cached))
            {
                return cached;
            }

            var result = await factory();
            if (result != null)
            {
                _cache[uri] = result;
            }

            return result;
        }
    }
}
