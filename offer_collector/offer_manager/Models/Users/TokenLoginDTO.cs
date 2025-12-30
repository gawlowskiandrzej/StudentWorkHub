namespace offer_manager.Models.Users
{
    public sealed class TokenLoginRequest
    {
        public string Token { get; init; } = string.Empty;
    }
    public sealed class TokenLoginResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public string Jwt { get; init; } = string.Empty;
        public string RememberMeToken { get; init; } = string.Empty;
    }
}
