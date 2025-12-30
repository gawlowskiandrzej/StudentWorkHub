namespace offer_manager.Models.Users
{
    public sealed class ChangePasswordRequest
    {
        public string Jwt { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;
    }

    public sealed class ChangePasswordResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
