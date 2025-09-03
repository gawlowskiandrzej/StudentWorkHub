using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Offer_collector.Models
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
    }
}
