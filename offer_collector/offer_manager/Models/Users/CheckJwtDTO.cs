namespace offer_manager.Models.Users
{
    /// <summary>
    /// Request payload for validating whether a JWT is accepted by the backend.
    /// </summary>
    public sealed class CheckJwtRequest
    {
        /// <summary>
        /// JSON Web Token to validate.
        /// </summary>
        public string Jwt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Response payload for JWT validation.
    /// </summary>
    public sealed class CheckJwtResponse
    {
        /// <summary>
        /// True when the token is valid; false otherwise.
        /// </summary>
        public bool Result { get; set; } = false;
    }
}
