using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Users
{
    public class UserCryptographicException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCryptographicException"/> class.
        /// </summary>
        public UserCryptographicException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCryptographicException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UserCryptographicException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCryptographicException"/> class with a specified 
        /// error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public UserCryptographicException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Password hashing helper (version 1).
    /// </summary>
    /// <remarks>
    /// Existing password hashes depend on the exact constants and hash layout used here.
    /// Treat this type as immutable; introduce EncryptionFunctionsV{N} for any changes instead of modifying this class.
    /// Future versions should be implemented as separate, non-inheriting classes.
    /// </remarks>
    internal class EncryptionFunctionsV1
    {
        public const string algorithmName = "argon2id";
        public const int encryptionFunctionVersion = 1;

        // 128-bit salt (16 bytes) used by this version.
        private const int _saltSize = 16;

        // 256-bit hash (32 bytes) used by this version.
        private const int _hashSize = 32;

        // Argon2id time cost (number of iterations).
        private const int _iterations = 4;

        // Argon2id memory cost in KiB (64 MiB).
        private const int _memory = 1024 * 64;

        // Argon2id parallelism factor.
        private const int _parallels = 4;

        // Strict UTF-8 encoder without BOM to keep a stable byte representation of passwords.
        private static readonly UTF8Encoding _utf8Strict = new(
            encoderShouldEmitUTF8Identifier: false,
            throwOnInvalidBytes: true
        );

        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[_saltSize];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        /// <summary>
        /// Generates a dummy password hash to be used when there is no real password hash,
        /// in order to match the timing of a costly Argon-based hashing operation.
        /// </summary>
        /// <param name="passwordLength">
        /// Desired length of the randomly generated dummy password used for hashing.
        /// </param>
        /// <returns>
        /// A formatted hash string containing the algorithm name, encryption function version,
        /// base64-encoded salt and base64-encoded dummy hash value.
        /// </returns>
        internal static string GenerateDummyHash(int passwordLength = 12)
        {
            passwordLength = passwordLength <= 512 && passwordLength > 0 ? passwordLength : 512;
            byte[] salt = GenerateSalt();
            return $"{algorithmName}$v={encryptionFunctionVersion}${Convert.ToBase64String(salt)}${Convert.ToBase64String(ComputeHash(RememberMeUtils.Generate(passwordLength).token, salt))}";
        }

        /// <summary>
        /// Computes Argon2id hash bytes for a validated password and salt using this version's parameters.
        /// </summary>
        /// <exception cref="UserCryptographicException">Thrown when the password or salt is invalid or encoding fails.</exception>
        internal static byte[] ComputeHash(string password, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new UserCryptographicException("Password is empty");

            // Hard limit to mitigate abuse of password verification when the original policy is unknown.
            if (password.Length > 512)
                throw new UserCryptographicException("Password is too long");

            if (salt is null)
                throw new UserCryptographicException("Salt is null");

            int saltLength = salt.Length;
            if (saltLength != _saltSize)
                throw new UserCryptographicException($"Wrong salt length. Expected: {_saltSize}, got: {saltLength}");

            byte[]? passwordBytes = null;
            try
            {
                passwordBytes = _utf8Strict.GetBytes(password);
                if (passwordBytes is null)
                    throw new UserCryptographicException("Encoding to UTF-8 bytes returned null");

                using Argon2id argon2 = new(passwordBytes);
                argon2.Salt = salt;
                argon2.Iterations = _iterations;
                argon2.MemorySize = _memory;
                argon2.DegreeOfParallelism = _parallels;

                return argon2.GetBytes(_hashSize);
            }
            catch (EncoderFallbackException ex)
            {
                throw new UserCryptographicException("Encoding password to UTF-8 failed", ex);
            }
            catch (Exception ex) when (ex is not UserCryptographicException)
            {
                throw new UserCryptographicException("Unknown exception while converting password to bytes", ex);
            }
            finally
            {
                // Ensure password bytes are wiped from memory.
                if (passwordBytes is not null)
                    CryptographicOperations.ZeroMemory(passwordBytes);
            }
        }

        /// <summary>
        /// Validates a password against the provided policy and returns a versioned Argon2id hash string.
        /// </summary>
        /// <param name="password">Plain-text password to hash.</param>
        /// <param name="passwordPolicy">Policy used to validate the password before hashing.</param>
        /// <returns>
        /// Formatted hash string:
        /// {algorithm}$v={version}${saltBase64}${hashBase64}.
        /// </returns>
        /// <exception cref="UserCryptographicException">Thrown when the password is invalid or hashing fails.</exception>
        internal static string GetPasswordHash(string? password, UserPasswordPolicy passwordPolicy)
        {
            if (string.IsNullOrEmpty(password))
                throw new UserCryptographicException("Password is empty");

            (bool IsValid, string Error) = passwordPolicy.ValidatePassword(password);
            if (!IsValid)
                throw new UserCryptographicException(Error);

            string saltBase64, hashBase64;
            byte[]? salt = null, hash = null;
            try
            {
                salt = GenerateSalt();
                hash = ComputeHash(password, salt);

                saltBase64 = Convert.ToBase64String(salt);
                hashBase64 = Convert.ToBase64String(hash);

                // Hash format is part of the contract; changing it requires a new EncryptionFunctionsVX implementation.
                return $"{algorithmName}$v={encryptionFunctionVersion}${saltBase64}${hashBase64}";
            }
            catch (Exception ex) when (ex is not UserCryptographicException)
            {
                throw new UserCryptographicException("Unknown exception while hashing password", ex);
            }
            finally
            {
                // Salt and hash must not remain in memory longer than necessary.
                if (salt is not null)
                    CryptographicOperations.ZeroMemory(salt);
                if (hash is not null)
                    CryptographicOperations.ZeroMemory(hash);
            }
        }

        /// <summary>
        /// Verifies a plain-text password against a stored Argon2id hash created by this version.
        /// </summary>
        /// <param name="userPassword">User supplied plain-text password.</param>
        /// <param name="hash">Stored hash string in this version's format.</param>
        /// <returns><c>true</c> if the password matches; otherwise <c>false</c>.</returns>
        /// <exception cref="UserCryptographicException">Thrown when the hash is invalid or verification fails.</exception>
        internal static bool VerifyPassword(string? userPassword, string? hash)
        {
            if (string.IsNullOrWhiteSpace(userPassword))
                throw new UserCryptographicException("Password is empty");

            if (string.IsNullOrWhiteSpace(hash))
                throw new UserCryptographicException("Hash is empty");

            // Expected format: {algorithm}$v={version}${saltBase64}${hashBase64}.
            string[] parts = hash.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4 || parts[0] != algorithmName || parts[1] != $"v={encryptionFunctionVersion}")
                throw new UserCryptographicException("Hash type is invalid");

            byte[]? salt = null, expectedHash = null, computedHash = null;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                if (salt.Length != _saltSize)
                    throw new UserCryptographicException("Salt length is incorrect");

                expectedHash = Convert.FromBase64String(parts[3]);
                if (expectedHash.Length != _hashSize)
                    throw new UserCryptographicException("Hash length is incorrect");


                computedHash = ComputeHash(userPassword, salt);
                bool isMatch = CryptographicOperations.FixedTimeEquals(
                    expectedHash,
                    computedHash
                );

                return isMatch;
            }
            catch (Exception ex) when (ex is not UserCryptographicException)
            {
                throw new UserCryptographicException("Unknown exception while comparing passwords", ex);
            }
            finally
            {
                // Wipe all sensitive data, regardless of the outcome.
                if (salt is not null)
                    CryptographicOperations.ZeroMemory(salt);
                if (expectedHash is not null)
                    CryptographicOperations.ZeroMemory(expectedHash);
                if (computedHash is not null)
                    CryptographicOperations.ZeroMemory(computedHash);
            }
        }
    }
}
