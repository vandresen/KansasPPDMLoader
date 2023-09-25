using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();
var connectionString = config["ConnectionString"];
Console.WriteLine(connectionString);