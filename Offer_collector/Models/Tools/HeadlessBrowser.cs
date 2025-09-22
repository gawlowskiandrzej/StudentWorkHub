using System.Diagnostics;
using System.Threading.Tasks;

namespace Offer_collector.Models.BrowserTools
{
    internal class HeadlessBrowser
    {
        public string Url { get; }
        public bool ServerIsRunning { get; set; }
        HttpClient Client { get; set; }

        public HeadlessBrowser(string url)
        {
            Url = url;
            ServerIsRunning = false;
            Client = new HttpClient() { BaseAddress = new Uri("http://localhost:3000/scrape") };
        }
        /// <summary>
        /// Getting web source from webpage driver, bypassing cloudflare
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetWebPageSource(string urlOfScrapPage)
        {

            if (ServerIsRunning)
                ServerIsRunning = await IsServerRunning();
            else
                ServerIsRunning = await StartNodeJsServer("../../../NodejsScrapper/scrapper-sever.js");

            if (!ServerIsRunning) return "";
            string urll = $"{Client.BaseAddress}?url={urlOfScrapPage}";
            HttpResponseMessage response = await Client.GetAsync(urll);

            string content = await response.Content.ReadAsStringAsync();

            // TODO Implement Send Request to nodeJs server with url
            // Using Puppetter with stealth plugin to bypass cloudflare
            // Add some user agents for better stealth


            return content;
        }
        private Task<bool> IsServerRunning()
        {
            return Task.Run(() => true);
        }
        private async Task<bool> StartNodeJsServer(string scriptPath = "")
        {
            if (string.IsNullOrEmpty(scriptPath))
                throw new ArgumentException("Ścieżka do pliku JS nie może być pusta.");

            Process process = new Process();
            process.StartInfo.FileName = "node"; // lub pełna ścieżka do node.exe np. "C:\\Program Files\\nodejs\\node.exe"
            process.StartInfo.Arguments = $"\"{scriptPath}\""; // uwzględniamy spacje w ścieżce
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            await Task.Delay(2000);

            // opcjonalnie można czytać output
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                return false;
            }
            else
            {
                if (output == "Scraper API running on http://localhost:3000\n")
                    return true;
                else return false;
            }
        }
    }
}
