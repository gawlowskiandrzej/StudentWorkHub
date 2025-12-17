using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Users
{
    public sealed record JwtOptions
    {
        /// <summary>
        /// JWT issuer (iss claim). Default: "StudentWorkHub".
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// JWT audience (aud claim). Default: "StudentWorkHub.Api.User".
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// Symmetric key used to sign JWTs.
        /// Prefer loading the key from a config file (appsettings / env vars) instead of generating it at runtime.
        /// </summary>
        public SymmetricSecurityKey SigningKey { get; }

        /// <summary>
        /// Access token time-to-live. Default: 10 minutes.
        /// </summary>
        public TimeSpan Ttl { get; }

        /// <summary>
        /// Allowed clock misalingment for token validation. Default: 30 seconds.
        /// </summary>
        public TimeSpan Skew { get; }

        /// <summary>
        /// True if <see cref="SigningKey"/> was auto-generated because input key was null/empty/whitespace.
        /// Use this to verify whether the configured key was overridden at runtime.
        /// Default: false (when a valid key is provided).
        /// </summary>
        public bool IsKeyGenerated { get; init; }

        /// <summary>
        /// Creates JWT options with sensible defaults.
        /// If <paramref name="signingKey"/> is null/empty/whitespace, a cryptographically secure random key is generated.
        /// </summary>
        /// <param name="issuer">Token issuer. Default when null/blank: "StudentWorkHub".</param>
        /// <param name="audience">Token audience. Default when null/blank: "StudentWorkHub.Api.User".</param>
        /// <param name="signingKey">Signing key (should typically come from config). If null/blank, a random key is generated.</param>
        /// <param name="accessTokenTtl">Token TTL. Default when null: 10 minutes.</param>
        /// <param name="clockSkew">Validation clock skew. Default when null: 30 seconds.</param>
        public JwtOptions(string? issuer, string? audience, string? signingKey, TimeSpan? accessTokenTtl, TimeSpan? clockSkew)
        {
            Issuer = string.IsNullOrWhiteSpace(issuer) ? "StudentWorkHub" : issuer;
            Audience = string.IsNullOrWhiteSpace(audience) ? "StudentWorkHub.Api.User" : audience;
            
            if (string.IsNullOrWhiteSpace(signingKey))
            {
                // Generates a 512-bit (64-byte) random key when none is provided.
                byte[] randomBytes = new byte[64];
                RandomNumberGenerator.Fill(randomBytes);
                SigningKey = new SymmetricSecurityKey(randomBytes);
                IsKeyGenerated = true;
            }
            else
            {
                // Uses the provided key (expected to be loaded from config).
                SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                IsKeyGenerated = false;
            }

            Ttl = accessTokenTtl ?? TimeSpan.FromMinutes(10);
            Skew = clockSkew ?? TimeSpan.FromSeconds(30);
        }
    };
}
