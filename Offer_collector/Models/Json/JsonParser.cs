using Newtonsoft.Json.Linq;
using Offer_collector.Models.Tools;
using System.Text.RegularExpressions;

namespace Offer_collector.Models.Json
{
    internal class JsonParser
    {
        private JObject _JObject { get; set; }
        public JsonParser(string rawJson)
        {
            _JObject = JObject.Parse(rawJson); 
        }
        public JToken? GetSpecificJsonFragment(string attrPath)
        {
           JToken? selectedToken = _JObject.SelectToken(attrPath) ?? null;
            return selectedToken;
        }
        public List<JToken> GetSpecificJsonFragments(string attrPath)
        {
            List<JToken> offersListJson = _JObject.SelectTokens(attrPath).ToList() ?? new List<JToken>();
            return offersListJson;
        }
        public static string MakeListFromObject(string json)
        {
            var replaced = json.ReplaceAt(1, 1, "[");
            var tmp1 = replaced.ReplaceAt(json.Length - 2, 1, "]");
            string result = Regex.Replace(tmp1, "\"[0-9]+\":", "");
            string tmp2 = result.Remove(0, 1);
            return tmp2.Remove(tmp2.Length - 1, 1);
        }
    }
}
