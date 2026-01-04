using System.Security.Cryptography;
using System.Text;

namespace worker.Models.Tools
{
    static class Cryptography
    {
        public static string ComputeUrlHash(string url)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(url);
            byte[] hashBytes = sha.ComputeHash(bytes);
            // konwertujemy do hex string dla Redis SET
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
