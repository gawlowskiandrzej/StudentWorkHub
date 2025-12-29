namespace offer_manager.Models.Users
{
    public sealed class LogoutRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class LogoutResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
