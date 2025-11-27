using worker;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
var redisConfig = builder.Configuration.GetSection("RedisServer");
string hostName = redisConfig.GetValue<string>("Host");
int port = redisConfig.GetValue<int>("Port");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{hostName}:{port}"));
var host = builder.Build();
host.Run();
