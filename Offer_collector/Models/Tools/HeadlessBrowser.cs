using System.Diagnostics;

namespace Offer_collector.Models.Tools
{
    internal class HeadlessBrowser
    {
        public bool ServerIsRunning { get; set; }
        HttpClient Client { get; set; }

        public HeadlessBrowser()
        {
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
                ServerIsRunning = StartNodeJsServer("../../../../Models/Tools/Nodejs/scrapper-sever.js");

            if (!ServerIsRunning) return "";
            string urll = $"{Client.BaseAddress}?url={urlOfScrapPage}";
            HttpResponseMessage response = await Client.GetAsync(urll);

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
        private Task<bool> IsServerRunning()
        {
            return Task.Run(() => true);
        }
        private bool StartNodeJsServer(string scriptPath = "")
        {
            if (string.IsNullOrEmpty(scriptPath))
                throw new ArgumentException("Ścieżka do pliku JS nie może być pusta.");

            Process process = new Process();
            process.StartInfo.FileName = "node"; 
            process.StartInfo.Arguments = $"\"{scriptPath}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

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
