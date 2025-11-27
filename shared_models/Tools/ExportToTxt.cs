using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Offer_collector.Models.Tools
{
    internal class ExportTo
    {
        public static void ExportToTxt<T>(List<T> offers, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                int offerIndex = 1;
                foreach (T offer in offers)
                {
                    if (offer != null)
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
        public static void ExportToJs(List<UnifiedOfferSchemaClass> offers, string filePath)
        {
            List<UnifiedOfferSchemaClass> existingOffers = new List<UnifiedOfferSchemaClass>();

            // Wczytaj dotychczasowe oferty, jeśli plik istnieje
            if (File.Exists(filePath))
            {
                string existingContent = File.ReadAllText(filePath).Trim();

                // Usuń ewentualny średnik na końcu
                if (existingContent.EndsWith(";"))
                    existingContent = existingContent[..^1];

                try
                {
                    existingOffers = JsonConvert.DeserializeObject<List<UnifiedOfferSchemaClass>>(existingContent)
                                     ?? new List<UnifiedOfferSchemaClass>();
                }
                catch
                {
                    throw new Exception("Error while deserializing JSON object.");
                }
            }

            // Dodaj tylko te oferty, których jeszcze nie ma (porównanie po Id)
            foreach (var offer in offers)
            {
                if (!existingOffers.Any(e => e.jobTitle == offer.jobTitle && e.description == offer.description && e.url == offer.url))
                {
                    existingOffers.Add(offer);
                }
            }

            // Serializuj ponownie i zapisz do pliku
            string json = JsonConvert.SerializeObject(existingOffers, Formatting.Indented);
            File.WriteAllText(filePath, $"{json};");
        }
        public static void ExportToJs(List<string> errors, string filePath)
        {
            // Serializuj ponownie i zapisz do pliku
            //string json = JsonConvert.SerializeObject(errors, Formatting.Indented);
            File.WriteAllText(filePath, $"[{string.Join(',', errors)}]");
        }
        public static List<UnifiedOfferSchemaClass> LoadFromJs(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<UnifiedOfferSchemaClass>();

            string content = File.ReadAllText(filePath).Trim();

            // Usuń ewentualny średnik na końcu
            if (content.EndsWith(";"))
                content = content[..^1];

            try
            {
                var offers = JsonConvert.DeserializeObject<List<UnifiedOfferSchemaClass>>(content)
                             ?? new List<UnifiedOfferSchemaClass>();

                return offers;
            }
            catch
            {
                throw new Exception("Error while deserializing JSON object from JS file.");
            }
        }
    }
}
