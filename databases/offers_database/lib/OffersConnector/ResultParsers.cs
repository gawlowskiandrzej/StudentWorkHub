namespace OffersConnector
{
    internal class ResultParsers
    {
        internal static List<string> RestrictionsParser(Dictionary<string, List<string>> values)
        {
            if (values.Count < 1)
                return [];

            List<string> result = new();
            string preamble = """
You must follow the rules below with maximum strictness.
For every field that references a list of standardized values, you are required to:

1. The final value for such a field MUST be exactly one of the standardized values explicitly provided for that field.
   - The string you output must be identical to one of the allowed values (string equality).
   - You are not allowed to prepend or append any extra words, symbols, or explanations.

2. For these fields, the list of standardized values has a HIGHER PRIORITY than the raw input text.
   - If the raw input text does not match exactly any value from the list, you MUST transform, shorten or generalize it until it matches one allowed value.
   - It is **forbidden** to simply copy the raw input text if it is not exactly one of the allowed values.

3. When the input for a restricted field contains an allowed standardized value plus additional descriptive words, you MUST keep only the standardized value and DROP the rest.
   - Example: input `"Pełny etat - standardowe godziny pracy"` and allowed value `"Pełny etat"`  
     → you MUST output exactly `"Pełny etat"`.
   - Example: input `"Elastyczne godziny pracy"` and allowed value `"Elastyczny czas pracy"`  
     → you MUST output exactly `"Elastyczny czas pracy"`.

4. You MUST use only the values explicitly provided for each field.
   - Never invent new values that are not present in the allowed list for that field.
   - If you are unsure which standardized value to choose, select the most general or generic option such as `"Inny"` when available.

5. You MUST preserve exact spelling, capitalization, punctuation, and spacing of the standardized values.
   - Do not change their form in any way.

6. If no standardized value clearly applies and the list contains a generic option such as `"Inny"` / `"Other"`, you MUST choose that generic option.
   - If there is no suitable generic option, you MUST leave the field empty or null, according to the output schema.

7. You must follow these rules even when the input text contains phrases that look similar to the standardized values.
   - Example pattern: if the input contains `"Pełny etat - standardowe godziny"` and the allowed value is `"Pełny etat"`,
     you MUST output exactly `"Pełny etat"` and nothing else.

You must adhere to these rules consistently and without exceptions.
""";
            result.Add(preamble);
            foreach (var value in values) 
            {
                result.Add($"\t- `{value.Key}` **MUST** use standarized values {{{string.Join(", ", value.Value)}}}.");
            }

            return result;
        }
    }
}
