namespace Users
{
    /// <summary>
    /// Shared configuration and utilities related to user encryption functions.
    /// </summary>
    /// <remarks>
    /// WARNING: Any details retrieved from this configuration (like versioning)
    /// should not be exposed directly to end users. Treat them as internal-only.
    /// </remarks>
    public static class SharedUserSettings
    {
        /// <summary>
        /// Latest supported encryption function version.
        /// </summary>
        public static readonly int encryptionFunctionVersion = 1;

        /// <summary>
        /// Returns the encryption function type matching the requested version.
        /// </summary>
        /// <param name="version">Requested encryption function version.</param>
        /// <returns>Type implementing the required encryption/version logic.</returns>
        public static Type GetEncryptionFunctions(int version)
        {
            // Clamp requested version to supported range to avoid invalid values.
            version = Math.Clamp(version, 1, encryptionFunctionVersion);

            return version switch
            {
                1 => typeof(EncryptionFunctionsV1),
                _ => typeof(EncryptionFunctionsV1),
            };
        }

    }
}
