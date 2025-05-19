using KansasPPDMLoaderConsole;
using KansasPPDMLoaderLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;

var rootCommand = new RootCommand("Kansas PPDMLoader");

var connectionOption = new Option<string>(
    name: "--connection",
    description: "Database connection string")
{ IsRequired = true };

var datatypeOption = new Option<string>(
    name: "--datatype",
    description: "Data type to process: Wellbore or Markerpick")
    .FromAmong("Wellbore", "Markerpick"); // Restrict to specific values
datatypeOption.IsRequired = true;

rootCommand.AddOption(connectionOption);
rootCommand.AddOption(datatypeOption);

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDataTransfer, DataTransfer>();
        services.AddSingleton<App>();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();

rootCommand.SetHandler(async (string connection, string datatype) =>
{
    await app.Run(connection, datatype);
}, connectionOption, datatypeOption);

// Run the command parser (this replaces app.Run and host.RunAsync)
return await rootCommand.InvokeAsync(args);
