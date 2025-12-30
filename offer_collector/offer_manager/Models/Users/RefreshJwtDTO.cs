namespace offer_manager.Models.Users
{
    public sealed class RefreshJwtRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class RefreshJwtResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public string Jwt { get; init; } = string.Empty;
    }
}
