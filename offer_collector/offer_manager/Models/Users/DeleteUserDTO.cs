namespace offer_manager.Models.Users
{
    public sealed class DeleteUserRequest
    {
        public string Jwt { get; init; } = string.Empty;
    }

    public sealed class DeleteUserResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
