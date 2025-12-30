namespace offer_manager.Models.Users
{
    public sealed class StandardRegisterUserRequest
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }

    public sealed class StandardRegisterUserResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }

}
