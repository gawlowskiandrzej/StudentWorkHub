using Offer_collector.Models.OfferScrappers;
using Offer_collector.Models.Tools;
using System.Text.RegularExpressions;

public abstract class BaseHtmlScraper
{
    protected readonly HttpClient _client = new HttpClient();
    private readonly ClientType _clientType = 0;
    private string htmlBody = "";
    protected int maxOfferCount;
    private HeadlessBrowser headlessBrowser = new HeadlessBrowser();
    
    public BaseHtmlScraper(ClientType clientType)
    {
        if (clientType == 0)
        {
            _client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                    "AppleWebKit/537.36 (KHTML, like Gecko) " +
                    "Chrome/122.0.0.0 Safari/537.36");

            _client.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

            _client.DefaultRequestHeaders.Add("Accept-Language", "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
            _client.DefaultRequestHeaders.Add("Referer", "https://www.google.com/");
            _client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            _client.DefaultRequestHeaders.Add("Pragma", "no-cache");
        }
        else
        {
            headlessBrowser = new HeadlessBrowser();
        }

        _clientType = clientType;
    }

    /// <summary>
    /// Download html source
    /// </summary>
    public async Task<string> GetHtmlAsync(string url)
    {
        if (_clientType == 0)
        {
            var cos = await _client.GetAsync(url);
            htmlBody = await cos.Content.ReadAsStringAsync();
        }
        else
        {
            htmlBody = await headlessBrowser.GetWebPageSource(url);
        }
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
    public abstract Task<(string, string, List<string>)> GetOfferAsync(string url = "");
    /// <summary>
    /// Decode unnessesary unicode characters
    /// </summary>

    /// <summary>
    /// Abstract method for getting offer count
    /// </summary>
    public virtual int GetOfferCountFromHtml() => maxOfferCount;
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
