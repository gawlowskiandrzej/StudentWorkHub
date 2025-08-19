using System;
using System.Net.Http;
using System.Threading.Tasks;

public class HtmlScraper
{
    private readonly HttpClient _client;

    public HtmlScraper()
	{
        _client = new HttpClient();
	}

    /// <summary>
    /// Download html source
    /// </summary>
    public async Task<string> GetHtmlAsync(string url)
    {
        return await _client.GetStringAsync(url);
    }

    /// <summary>
    /// Get specific json fragment from html source
    /// </summary>
    public async Task<string> GetJsonFragment(string htmlSource)
    {

    }

    public async Task<string> GetOffer(OfferSitesTypes offerType)
    {

    }
}
