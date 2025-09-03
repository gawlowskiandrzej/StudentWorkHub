using System.Text.RegularExpressions;

public abstract class BaseHtmlScraper
{
    protected readonly HttpClient _client;
    private string htmlBody;
    private string jsonFragment;
    
    public BaseHtmlScraper()
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
                           | System.Net.DecompressionMethods.Deflate
                           | System.Net.DecompressionMethods.Brotli
        };
        _client = new HttpClient(handler);

    }

    /// <summary>
    /// Download html source
    /// </summary>
    public async Task<string> GetHtmlAsync(string url)
    {
        htmlBody = await _client.GetStringAsync(url);
        return htmlBody;
    }

    /// <summary>
    /// Get script or some patter from html
    /// </summary>
    protected string GetJsonFragment(string htmlSource, string regexPattern, int index = 0)
    {
        var matches = Regex.Matches(htmlSource, regexPattern,
        RegexOptions.Singleline | RegexOptions.IgnoreCase);

        if (matches.Count > index)
        {
            return matches[index].Groups[1].Value;
        }

        return "";
    }

    /// <summary>
    /// Abstract method for getting offer
    /// </summary>
    public abstract Task<string> GetOfferAsync(string url = "");
    /// <summary>
    /// Decode unnessesary unicode characters
    /// </summary>
    protected string DecodeUnicodeStrict(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
        {
            string hex = match.Groups[1].Value;
            int code = Convert.ToInt32(hex, 16);
            return char.ConvertFromUtf32(code);
        });
    }
}
