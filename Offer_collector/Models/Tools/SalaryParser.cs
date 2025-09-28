using System.Globalization;
using System.Text.RegularExpressions;

namespace Offer_collector.Models.Tools
{

    public static class SalaryParser
    {
        // Lista skrótów walut i symboli do wykrycia (możesz rozszerzyć)
        private static readonly string[] CurrencyKeywords = new[]
        {
        "zł", "zl", "pln", "eur", "€", "usd", "$", "gbp", "£", "chf", "sek", "nok", "dkk"
    };

        // Normalizuje i parsuje liczbę w formacie z spacjami / NBSP / przecinkami / kropkami
        private static bool TryParseDecimalNormalised(string s, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            // usuń spacje i NBSP, zamień przecinek na kropkę
            var cleaned = s.Trim()
                           .Replace("\u00A0", "") // NBSP
                           .Replace(" ", "")
                           .Replace(",", ".");    // ujednolicić separator dziesiętny
            return decimal.TryParse(cleaned, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);
        }

        public static AplikujPl.Salary Parse(string input)
        {
            if (input == null) return null;
            var original = input;
            // normalizacja spacji (zamień różne białe znaki na zwykłą spację)
            input = Regex.Replace(input, @"\s+", " ").Trim().ToLowerInvariant();

            var result = new AplikujPl.Salary();

            if (Regex.IsMatch(input, @"\b(brutto|brutto\.)\b"))
                result.type = "brutto";
            else if (Regex.IsMatch(input, @"\b(netto|netto\.)\b"))
                result.type = "netto";
            else
                result.type = "brutto";

            // 1) spróbuj dopasować zakres z patternem "liczba (do|-) liczba" i opcjonalną walutą (np. "3000 do 4000 zł", "3 000-4 000 PLN")
            var rangePattern = new Regex(
                        @"(?<a>\d{1,3}(?:[ \u00A0]\d{3})*(?:[.,]\d+)?|\d+(?:[.,]\d+)?)\s*(?:-|–|—|\bdo\b|\bto\b)\s*(?<b>\d{1,3}(?:[ \u00A0]\d{3})*(?:[.,]\d+)?|\d+(?:[.,]\d+)?)(?:\s*(?<cur>[^\d\s]{1,4}|[a-z]{2,4}))?",
                        RegexOptions.IgnoreCase);
            var mRange = rangePattern.Match(input);
            if (mRange.Success)
            {
                if (TryParseDecimalNormalised(mRange.Groups["a"].Value, out var a) &&
                    TryParseDecimalNormalised(mRange.Groups["b"].Value, out var b))
                {
                    result.from = Math.Min(a, b);
                    result.to = Math.Max(a, b);
                }

                var cur = mRange.Groups["cur"].Value;
                if (!string.IsNullOrWhiteSpace(cur))
                    result.currency = NormalizeCurrency(cur);
            }
            else
            {
                // 2) jeśli powyżej nie zadziała, znajdź wszystkie liczby w tekście i spróbuj przypisać
                var numberPattern = new Regex(@"\d{1,3}(?:[ \u00A0]\d{3})*(?:[.,]\d+)?|\d+(?:[.,]\d+)?");
                var nums = numberPattern.Matches(input).Cast<Match>().Select(mm => mm.Value).ToList();
                if (nums.Count >= 2)
                {
                    if (TryParseDecimalNormalised(nums[0], out var n1) && TryParseDecimalNormalised(nums[1], out var n2))
                    {
                        result.from = Math.Min(n1, n2);
                        result.to = Math.Max(n1, n2);
                    }
                }
                else if (nums.Count == 1)
                {
                    if (TryParseDecimalNormalised(nums[0], out var n))
                    {
                        result.from = n;
                        result.to = n;
                    }
                }

                // spróbuj wyciągnąć walutę blisko liczby: szukamy słowa-symbolu waluty w krótkim window
                foreach (var kw in CurrencyKeywords)
                {
                    if (input.Contains(" " + kw) || input.Contains(kw + " ") || input.EndsWith(kw))
                    {
                        result.currency = NormalizeCurrency(kw);
                        break;
                    }
                }
            }

            // 3) jeśli nadal brak waluty, spróbuj znaleźć dowolny symbol waluty w całym tekście
            if (string.IsNullOrEmpty(result.currency))
            {
                var curAny = Regex.Match(input, @"(pln|zł|zl|eur|€|usd|\$|gbp|£|chf|sek|nok|dkk)", RegexOptions.IgnoreCase);
                if (curAny.Success) result.currency = NormalizeCurrency(curAny.Value);
            }

            // 4) ostateczny fallback: jeśli w tekście pojawia się np. "zł" z diakrytykami poza regexem, uwzględnij
            if (string.IsNullOrWhiteSpace(result.currency))
            {
                if (Regex.IsMatch(original, "zł", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) result.currency = "zł";
            }

            if (String.IsNullOrEmpty(result.type))
            {
                if (result.from > 0 && result.from < 1000)
                    result.period = "hourly";
                else
                    result.period = "monthly";
            }
            return result;
        }

        private static string NormalizeCurrency(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;
            raw = raw.Trim().ToLowerInvariant();
            switch (raw)
            {
                case "zł":
                case "zl":
                case "pln": return "PLN";
                case "€":
                case "eur": return "EUR";
                case "$":
                case "usd": return "USD";
                case "£":
                case "gbp": return "GBP";
                case "chf": return "CHF";
                case "sek": return "SEK";
                case "nok": return "NOK";
                case "dkk": return "DKK";
                default:
                    // jeśli np. "zł." lub inne, usuń kropki i zwróć uppercase
                    var cleaned = raw.Trim('.', ',');
                    return cleaned.Length <= 4 ? cleaned.ToUpperInvariant() : cleaned;
            }
        }
    }
}
