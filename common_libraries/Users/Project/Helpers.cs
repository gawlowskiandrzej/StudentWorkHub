using System.Security.Cryptography;

namespace Users
{
    /// <summary>
    /// Helper utilities shared across the project.
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// Fixed 64-character alphabet used by <see cref="GenerateRandomString(int)"/> to avoid repeated allocations.
        /// </summary>
        private const string _randomStringsAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        /// <summary>
        /// Generates a cryptographically secure random string using a 64-character URL-safe alphabet.
        /// </summary>
        /// <param name="outputLength">Number of characters to generate. Must be greater than zero.</param>
        /// <returns>A cryptographically secure random string of length <paramref name="outputLength"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="outputLength"/> is less than or equal to zero, or when the alphabet length is not 64.
        /// </exception>
        /// <exception cref="SystemException">Thrown when random string generation fails.</exception>
        public static string GenerateRandomString(int outputLength = 64)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(outputLength, nameof(outputLength));

            // The alphabet must be exactly 64 chars; '& 63' produces indices 0..63 (6-bit mask). If the length changes,
            // indexing becomes invalid (out of range) or the distribution is no longer what we expect.
            ArgumentOutOfRangeException.ThrowIfNotEqual(_randomStringsAlphabet.Length, 64, nameof(_randomStringsAlphabet));
            try
            {
                byte[] randomBytes = new byte[outputLength];
                RandomNumberGenerator.Fill(randomBytes);

                char[] outputChars = new char[outputLength];
                for (int i = 0; i < outputLength; i++)
                    // '& 63' keeps only the lowest 6 bits (0..63), mapping uniformly to the 64-character alphabet.
                    outputChars[i] = _randomStringsAlphabet[randomBytes[i] & 63];

                return new string(outputChars);
            }
            catch (Exception ex)
            {
                throw new SystemException("Random string generation failed", ex);
            }
        }
    }
}
