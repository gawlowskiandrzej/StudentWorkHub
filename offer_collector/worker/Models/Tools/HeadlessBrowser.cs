using System.Diagnostics;
using worker.Models.Constants;
using worker.Models.Tools;

namespace Offer_collector.Models.Tools
{
    internal class HeadlessBrowser
    {

        public bool ServerIsRunning { get; set; }
        HttpClient Client { get; set; }

        public HeadlessBrowser()
        {
            ServerIsRunning = false;
            Client = new HttpClient() { BaseAddress = new Uri($"http://{HeadlessbrowserSettings.Host}:{HeadlessbrowserSettings.Port}") };
            
        }
        /// <summary>
        /// Getting web source from webpage driver, bypassing cloudflare
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetWebPageSource(string urlOfScrapPage)
        {

            ServerIsRunning = await StartNodeJsServerAsync();

            if (!ServerIsRunning) return "";
            string urll = $"{Client.BaseAddress}scrape?url={urlOfScrapPage}";
            HttpResponseMessage response = await Client.GetAsync(urll);

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
        //private Task<bool> IsServerRunning()
        //{
        //    return Task.Run(() => true);
        //}
        private async Task<bool> StartNodeJsServerAsync()
        {

            // Spróbuj połączyć się kilka razy co 1 sekundę

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    var resp = await Client.GetAsync($"{Client.BaseAddress}health");
                    if (resp.IsSuccessStatusCode)
                        return true;
                }
                catch
                {
                    // Serwer jeszcze się nie uruchomił
                    return false;
                }
                await Task.Delay(1000);
            }

            return false; // nie wystartował w 10 sekund
        }
    }
}
