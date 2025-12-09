using System.Security.Cryptography;
using System.Text;

namespace Users
{
    /// <summary>
    /// Provides helper methods for generating and validating remember-me tokens.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: Do NOT expose any error messages, exception messages or returned error values
    /// from this class directly to end users. These values are intended only for logging,
    /// diagnostics and internal debugging. A higher layer MUST translate them into generic,
    /// user-friendly messages without leaking internal details.
    /// </remarks>
    internal static class RememberMeUtils
    {
        /// <summary>
        /// Ensures that the value is at least 4 and aligned to the next multiple of 4.
        /// </summary>
        /// <param name="value">Requested length to be aligned.</param>
        /// <returns>Value rounded up to the nearest multiple of 4 (minimum 4).</returns>
        private static int ClampToNextMultipleOf4(int value)
        {
            if (value < 1)
                return 4;

            if (value % 4 == 0)
                return value;

            // Aligns the value to Base64-friendly chunk size (4 characters per block).
            return ((value / 4) + 1) * 4;
        }

        /// <summary>
        /// Computes a SHA-256 hash for the given input bytes and returns it as Base64.
        /// </summary>
        /// <param name="input">Raw bytes to be hashed.</param>
        /// <returns>Base64-encoded SHA-256 hash.</returns>
        /// <exception cref="UserCryptographicException">
        /// Thrown when the hash computation fails.
        /// </exception>
        private static string ComputeTokenHash(byte[] input)
        {
            byte[]? hashBytes = null;
            try
            {
                hashBytes = SHA256.HashData(input);
                
                return Convert.ToBase64String(hashBytes);
            }
            catch (Exception)
            {
                throw new UserCryptographicException("Failed to generate token hash");
            }
            finally
            {
                if (hashBytes is not null)
                    CryptographicOperations.ZeroMemory(hashBytes);
            }
        }

        /// <summary>
        /// Generates a random remember-me token and its corresponding hash.
        /// </summary>
        /// <param name="length">
        /// Desired length of the Base64 token string. Will be rounded up to the nearest multiple of 4.
        /// </param>
        /// <returns>
        /// A tuple containing the Base64 token and its Base64-encoded SHA-256 hash.
        /// </returns>
        /// <exception cref="UserCryptographicException">
        /// Thrown when token generation or hashing fails, or when validation of the generated token fails.
        /// </exception>
        internal static (string token, string hash) Generate(int length=256)
        {
            length = ClampToNextMultipleOf4(length);

            // Converts desired Base64 string length to raw byte length (3 bytes -> 4 Base64 chars).
            int scaledLength = length * 3 / 4;

            byte[]? randomBytes = null;
            string rememberMeToken = string.Empty, rememberMeTokenHash = string.Empty;
            try
            {
                randomBytes = new byte[scaledLength];

                // Cryptographically strong random generator for token bytes.
                RandomNumberGenerator.Fill(randomBytes);
                rememberMeToken = Convert.ToBase64String(randomBytes);
                rememberMeTokenHash = ComputeTokenHash(randomBytes);
            }
            catch (Exception ex)
            {
                throw new UserCryptographicException("Unknown exception while generating remember-me token bytes", ex);
            }
            finally
            {
                if (randomBytes is not null)
                    CryptographicOperations.ZeroMemory(randomBytes);
            }

            if (string.IsNullOrWhiteSpace(rememberMeToken))
                throw new UserCryptographicException("Generated token is empty");

            if (string.IsNullOrWhiteSpace(rememberMeTokenHash))
                throw new UserCryptographicException("Generated token hash is empty");

            if (rememberMeToken.Length != length)
                throw new UserCryptographicException("Generated token length is incorrect");

            return (rememberMeToken, rememberMeTokenHash);
        }

        /// <summary>
        /// Computes a SHA-256 hash for a Base64-encoded remember-me token string.
        /// </summary>
        /// <param name="token">
        /// Base64-encoded token string to be hashed. Must not be null, empty or whitespace.
        /// </param>
        /// <returns>
        /// Base64-encoded SHA-256 hash of the decoded token bytes.
        /// </returns>
        /// <exception cref="UserCryptographicException">
        /// Thrown when the token is empty, has an invalid Base64 format, or when the hash computation fails.
        /// </exception>
        internal static string GetHash(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new UserCryptographicException("Token is empty");

            byte[]? tokenBytes = null;
            try
            {
                // Decode the Base64 token string into raw bytes.
                tokenBytes = Convert.FromBase64String(token);

                // Compute and return the Base64-encoded SHA-256 hash of the token bytes.
                return ComputeTokenHash(tokenBytes);                
            }
            catch (FormatException ex)
            {
                throw new UserCryptographicException("Token has invalid Base64 format", ex);
            }
            catch (Exception ex)
            {
                throw new UserCryptographicException("Failed to compare hashes", ex);
            }
            finally
            {
                // Ensure raw token bytes are wiped from memory as soon as possible.
                if (tokenBytes is not null)
                    CryptographicOperations.ZeroMemory(tokenBytes);
            }
        }
    }
}
