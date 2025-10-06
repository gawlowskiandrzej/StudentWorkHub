using Newtonsoft.Json.Linq;

namespace Offer_collector.Models.Tools
{
    internal class ExportToTxtt
    {
        public static void ExportToTxt<T>(List<T> offers, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                int offerIndex = 1;
                foreach (var offer in offers)
                {
                    JObject json = JObject.FromObject(offer);

                    writer.WriteLine($"Oferta #{offerIndex}");
                    writer.WriteLine(new string('-', 40));

                    foreach (var prop in json.Properties())
                    {
                        writer.WriteLine($"{prop.Name}: {prop.Value}");
                    }

                    writer.WriteLine(); // pusty wiersz
                    offerIndex++;
                }
            }
        }
    }
}
