namespace offer_manager.Models.Users
{
    public sealed class StandardLoginRequest
    {
        public string Login { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public bool RememberMe { get; init; }
    }
    public sealed class StandardLoginResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public string Jwt { get; init; } = string.Empty;
        public string RememberMeToken { get; init; } = string.Empty;
    }
}
