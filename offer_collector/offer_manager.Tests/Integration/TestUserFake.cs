using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Users;

namespace offer_manager.Tests.Integration
{
    // Lightweight fake that mimics the behavior used in integration tests.
    public class TestUser : User
    {
        private readonly ConcurrentDictionary<string, byte> _registeredEmails = new();

        public TestUser(string username, string password, string host, int port, string dbName)
            : base(username, password, host, port, dbName) { }

        public override Task<(bool result, string error)> StandardRegisterAsync(UserPasswordPolicy upp, string? email, string? rawPassword, string? firstName, string? lastName = null, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(email)) return Task.FromResult((false, "Email empty"));
            if (!_registeredEmails.TryAdd(email, 0)) return Task.FromResult((false, "Email already exists"));
            return Task.FromResult((true, string.Empty));
        }

        public override Task<(bool result, string error, string? rememberToken, long? userId)> StandardAuthAsync(string? username, string? password, bool? rememberMe, CancellationToken cancellation = default)
        {
            var pass = (password ?? "").ToLower();
            if (pass.Contains("wrong") || pass.Contains("invalid"))
                return Task.FromResult((false, "Invalid credentials", (string?)null, (long?)null));
            return Task.FromResult((true, string.Empty, "token_abc123", (long?)1));
        }

        public override Task<(bool result, string error, string? rememberToken, long? userId)> AuthWithTokenAsync(string? rememberToken, CancellationToken cancellation = default)
        {
            if (string.IsNullOrEmpty(rememberToken) || rememberToken == "invalid_token" || rememberToken.Contains("invalid"))
                return Task.FromResult((false, "Invalid token", (string?)null, (long?)null));
            return Task.FromResult((true, string.Empty, "new_token", (long?)1));
        }
    }
}