namespace offer_manager.Models.Users
{
    /// <summary>
    /// Configuration model for JWT token generation and validation.
    /// </summary>
    internal class AuthTokenSettings
    {
        /// <summary>
        /// The token issuer (the authority that creates and signs the JWT).
        /// Must match the expected issuer during token validation.
        /// </summary>
        internal string Issuer { get; init; } = "StudentWorkHub";

        /// <summary>
        /// The intended audience of the token (who the token is issued for).
        /// Must match the expected audience during token validation.
        /// </summary>
        internal string Audience { get; init; } = "StudentWorkHub.Api.User";

        /// <summary>
        /// The secret key used to sign JWTs (for symmetric algorithms like HS256).
        /// </summary>
        internal string? SigningKey { get; init; } = null;

        /// <summary>
        /// Access token time-to-live in hours.
        /// Defines how long a generated access token remains valid.
        /// </summary>
        internal double AccessTokenTtlHours { get; init; } = 2.0;

        /// <summary>
        /// Allowed clock skew in seconds when validating token timestamps (e.g., exp, nbf).
        /// Helps tolerate small time differences between systems.
        /// </summary>
        internal double ClockSkewSeconds { get; init; } = 30.0;
    }
}
