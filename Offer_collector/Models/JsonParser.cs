using Newtonsoft.Json.Linq;

namespace Offer_collector.Models
{
    internal class JsonParser
    {
        private JObject _JObject { get; set; }
        public JsonParser(string rawJson)
        {
            _JObject = JObject.Parse(rawJson); 
        }
        public string GetSpecificJsonFragment(string attrPath)
        {
           _JObject.SelectToken(attrPath);
           return _JObject.ToString();
        }
        public List<JToken> GetSpecificJsonFragments(string attrPath)
        {
            List<JToken> offersListJson = _JObject.SelectTokens(attrPath).ToList() ?? new List<JToken>();
            return offersListJson;
        }
    }
}
