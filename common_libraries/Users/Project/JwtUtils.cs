using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Users
{
    /// <summary>
    /// Internal helper for issuing and validating JWT access tokens for users.
    /// Uses HMAC-SHA256 and stores the user id in the "sub" claim.
    /// </summary>
    public static class JwtUtils
    {
        /// <summary>
        /// Generates a signed JWT containing the user id in the "UserId" claim.
        /// </summary>
        /// <param name="jwtOptions">JWT configuration (issuer, audience, signing key, TTL).</param>
        /// <param name="userId">User identifier (must be greater than zero).</param>
        /// <returns>A compact serialized JWT string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="jwtOptions"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="userId"/> is less than or equal to zero,
        /// when <c>now.Add(jwtOptions.Ttl)</c> overflows <see cref="DateTime"/>,
        /// or when the signing key is considered too short for the selected HMAC algorithm (e.g., IDX10653). :contentReference[oaicite:2]{index=2}
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown by <see cref="JwtSecurityTokenHandler.WriteToken(SecurityToken)"/> if the token argument is null
        /// (not expected with the current implementation). :contentReference[oaicite:3]{index=3}
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown by <see cref="JwtSecurityTokenHandler.WriteToken(SecurityToken)"/> if the token is not a <see cref="JwtSecurityToken"/>
        /// (not expected with the current implementation). :contentReference[oaicite:4]{index=4}
        /// </exception>
        /// <exception cref="SecurityTokenEncryptionFailedException">
        /// Thrown by <see cref="JwtSecurityTokenHandler.WriteToken(SecurityToken)"/> in encryption/JWE scenarios
        /// (not expected here because the token is signed-only). :contentReference[oaicite:5]{index=5}
        /// </exception>
        public static string Generate(JwtOptions jwtOptions, long userId)
        {
            ArgumentNullException.ThrowIfNull(jwtOptions, nameof(jwtOptions));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId, nameof(userId));

            DateTime now = DateTime.UtcNow;

            // Minimal standard claims: subject, unique token id, issued-at.
            List<Claim> claims =
            [
                new("UserId", $"{userId}"),
            ];

            // HMAC-SHA256 signed token.
            SigningCredentials creds = new(jwtOptions.SigningKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(jwtOptions.Ttl),
                signingCredentials: creds
            );

            // Disable inbound claim type mapping to keep claim names unchanged.
            JwtSecurityTokenHandler handler = new() { MapInboundClaims = false };
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT and extracts the user id from the "UserId" claim.
        /// </summary>
        /// <param name="jwtOptions">JWT validation configuration (issuer, audience, signing key, clock skew).</param>
        /// <param name="jwt">JWT string to validate.</param>
        /// <returns>User id parsed from the "UserId" claim.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="jwtOptions"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="jwt"/> is null/empty/whitespace.</exception>
        /// <exception cref="SecurityTokenExpiredException">
        /// Thrown when the token is expired (intentionally not wrapped to allow separate handling). :contentReference[oaicite:6]{index=6}
        /// </exception>
        /// <exception cref="IncorrectTokenException">
        /// Thrown when validation fails for reasons other than expiration, when the algorithm is unexpected,
        /// or when the "UserId" claim is missing/invalid.
        /// </exception>
        public static long GetUserId(JwtOptions jwtOptions, string jwt)
        {
            ArgumentNullException.ThrowIfNull(jwtOptions, nameof(jwtOptions));
            ArgumentException.ThrowIfNullOrWhiteSpace(jwt, nameof(jwt));

            TokenValidationParameters parameters = new() 
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtOptions.SigningKey,

                ValidateLifetime = true,
                ClockSkew = jwtOptions.Skew,

                // Reject tokens signed with a different algorithm.
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
            };

            try
            {
                JwtSecurityTokenHandler handler = new() { MapInboundClaims = false };
                ClaimsPrincipal principal = handler.ValidateToken(jwt, parameters, out SecurityToken? validatedToken);

                // Enforce expected token type and algorithm (defense-in-depth).
                if (validatedToken is not JwtSecurityToken jwtToken || !string.Equals(jwtToken.Header.Alg, SecurityAlgorithms.HmacSha256, StringComparison.Ordinal))
                    throw new IncorrectTokenException();

                // "UserId" must exist and must be a positive long.
                if (!long.TryParse(principal.FindFirst("UserId")?.Value, out long userId) || userId < 1)
                    throw new IncorrectTokenException();

                return userId;
            }
            catch (Exception ex) when (ex is not IncorrectTokenException && ex is not SecurityTokenExpiredException)
            {
                // Normalize all non-expiration failures into a single domain exception.
                throw new IncorrectTokenException("Exception while token validation occured", ex);
            }
        }
    }
}
