using System.Text.RegularExpressions;

public abstract class BaseHtmlScraper
{
    protected readonly HttpClient _client;
    private string htmlBody;
    private string jsonFragment;
    
    public BaseHtmlScraper()
    {
        _client = new HttpClient();

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
    protected string GetJsonFragment(string htmlSource, string regexPattern)
    {
        var match = Regex.Match(htmlSource, regexPattern, RegexOptions.Singleline);
        jsonFragment = match.Success? match.Groups[1].Value : null;
        return jsonFragment;
    }

    /// <summary>
    /// Abstract method for getting offer
    /// </summary>
    public abstract Task<string> GetOfferAsync(string parameters = "", string url = "");
}
