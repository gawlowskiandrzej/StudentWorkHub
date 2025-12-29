namespace offer_manager.Models.Users
{
    public sealed class CheckPermissionRequest
    {
        public string Jwt { get; init; } = string.Empty;
        public string PermissionName { get; init; } = string.Empty;
    }

    public sealed class CheckPermissionResponse
    {
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
