namespace OffersConnector
{
    internal class ResultParsers
    {
        internal static List<string> RestrictionsParser(Dictionary<string, List<string>> values)
        {
            if (values.Count < 1)
                return [];

            List<string> result = new();

            foreach (var value in values) 
            {
                result.Add($"- `{value.Key}` **MUST** use standarized values {{{string.Join(", ", value.Value)}}}.");
            }

            return result;
        }
    }
}
