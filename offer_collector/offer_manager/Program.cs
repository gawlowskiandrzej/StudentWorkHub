using Microsoft.Extensions.Options;
using Offer_collector.Models.AI;
using Offer_collector.Models.DatabaseService;
using offer_manager.Interfaces;
using offer_manager.Models.FilterService;
using offer_manager.Models.Others;
using offer_manager.Models.Others.AutoMapper;
using offer_manager.Models.PaginationService;
using offer_manager.Models.Users;
using offer_manager.Models.WorkerService;
using StackExchange.Redis;
using Users;
using LogClient;

namespace offer_manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var redisConfig = builder.Configuration.GetSection("RedisServer");
            var apiKeysConfig = builder.Configuration.GetSection("ApiKeys");
            var frontendConfig = builder.Configuration.GetSection("Frontend");
            var geminiKey = apiKeysConfig.GetValue<string>("GeminiKey");
            builder.Services.Configure<StaticSettings>(
                builder.Configuration.GetSection("StaticSettings")
            );
            string redisHostname = redisConfig.GetValue<string>("Host") ?? "";
            int redisPort = redisConfig.GetValue<int>("Port");
            int portProdApi = frontendConfig.GetValue<int>("PortProdApi");
            int portDevApi = frontendConfig.GetValue<int>("PortDevApi");
            int portDevFront = frontendConfig.GetValue<int>("PortDev");
            string ipDevFront = frontendConfig.GetValue<string>("IpDev") ?? "";
            string ipProdApi = frontendConfig.GetValue<string>("IpProdApi") ?? "";
            string ipDevApi = frontendConfig.GetValue<string>("IpDevApi") ?? "";


            DatabaseSettings dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? new DatabaseSettings();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins($"http://{ipDevApi}:{portDevApi}", $"https://{ipDevApi}:{portDevApi}", $"http://{ipProdApi}:{portProdApi}", $"https://{ipProdApi}:{portProdApi}", $"http://{ipDevFront}:{portDevFront}", $"https://{ipDevFront}:{portDevFront}")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAutoMapper(typeof(OfferProfile));

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<StaticSettings>>().Value
            );
            builder.Services.AddSingleton(new AIProcessor(geminiKey: geminiKey ?? ""));

            builder.Services.AddSingleton<IDatabaseService>(sp => new DBService(dbSettings.Username, dbSettings.Password, dbSettings.Host, dbSettings.Port, dbSettings.Database));
            // register concrete type too for components that depend on DBService directly
            builder.Services.AddSingleton<DBService>(sp => (DBService)sp.GetRequiredService<IDatabaseService>());

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse($"{redisHostname}:{redisPort}");
                options.AllowAdmin = true;

                var redis = ConnectionMultiplexer.Connect(options);
                return redis;
            });

            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<PaginationService>();
            builder.Services.AddScoped<FilterService>();
            builder.Services.AddHostedService<RedisPreloadHostedService>();

            PasswordPolicySettings passwordPolicySettings = builder.Configuration.GetSection("PasswordPolicySettings").Get<PasswordPolicySettings>() ?? new();

            // Password policy used by auth endpoints.
            builder.Services.AddSingleton(new UserPasswordPolicy(
                minLength: passwordPolicySettings.MinLength,
                maxLength: passwordPolicySettings.MaxLength,
                requireUppercase: passwordPolicySettings.RequireUppercase,
                requireLowercase: passwordPolicySettings.RequireLowercase,
                requireDigit: passwordPolicySettings.RequireDigit,
                requireNonAlphanumeric: passwordPolicySettings.RequireNonAlphanumeric,
                requiredUniqueChars: passwordPolicySettings.RequiredUniqueChars,
                allowedSpecialCharacters: passwordPolicySettings.AllowedSpecialCharacters,
                knownPasswordsListPath: passwordPolicySettings.KnownPasswordsListPath
            ));


            AuthTokenSettings authTokenSettings = builder.Configuration.GetSection("AuthTokenSettings").Get<AuthTokenSettings>() ?? new();

            // JWT configuration used for generating access tokens.
            // TODO: Replace 'signingKey: null' with a real secret/private key (and validate it's present on startup).
            builder.Services.AddSingleton(new JwtOptions(
                issuer: authTokenSettings.Issuer,
                audience: authTokenSettings.Audience,
                signingKey: authTokenSettings.SigningKey,
                accessTokenTtl: TimeSpan.FromHours(authTokenSettings.AccessTokenTtlHours),
                clockSkew: TimeSpan.FromSeconds(authTokenSettings.ClockSkewSeconds)
            ));


            GeneralDatabaseSettings generalDbSettings = builder.Configuration.GetSection("GeneralDatabaseSettings").Get<GeneralDatabaseSettings>() ?? new();

            // Service for talking to PostgreSQL / user storage (keeps datasource/connection settings, no per-user state).
            builder.Services.AddSingleton(new User(
                username: generalDbSettings.Username,
                password: generalDbSettings.Password,
                host: generalDbSettings.Host,
                port: generalDbSettings.Port
            ));

            builder.Services.AddSingleton(new Logger(
                "offer-manager",
                "client-api",
                true,
                10,
                true
            ));

            var app = builder.Build();

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
