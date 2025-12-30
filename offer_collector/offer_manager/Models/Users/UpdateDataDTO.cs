namespace offer_manager.Models.Users
{
    public sealed class UpdateDataRequest
    {
        public string Jwt { get; init; } = string.Empty;

        public string? UserFirstName { get; init; }
        public string? UserSecondName { get; init; }
        public string? UserLastName { get; init; }
        public string? UserPhone { get; init; }
    }

    public sealed class UpdateDataResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;

        public bool UserFirstNameUpdated { get; init; } = false;
        public bool UserSecondNameUpdated { get; init; } = false;
        public bool UserLastNameUpdated { get; init; } = false;
        public bool UserPhoneUpdated { get; init; } = false;
    }
}
