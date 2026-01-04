using OffersConnector;
using shared_models;
using StackExchange.Redis;
using worker;
using worker.Models.Constants;
using worker.Models.Tools;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
var redisConfig = builder.Configuration.GetSection("RedisServer");
var nodeConfig = builder.Configuration.GetSection("NodeServer");
string hostName = redisConfig.GetValue<string>("Host");
int port = redisConfig.GetValue<int>("Port");

var dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

builder.Services.AddSingleton(new PgConnector(
    dbSettings.Username,
    dbSettings.Password,
    dbSettings.Host,
    dbSettings.Port,
    dbSettings.Database
));

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{hostName}:{port}"));
var headlessSettings = builder.Configuration.GetSection("NodeServer");
HeadlessbrowserSettings.Host = headlessSettings.GetValue<string>("Host");
HeadlessbrowserSettings.Port = headlessSettings.GetValue<int>("Port");
ConstValues.delayBetweenRequests = builder.Configuration.GetValue<int>("DelayBetweenRequestsMs");
var host = builder.Build();
host.Run();
