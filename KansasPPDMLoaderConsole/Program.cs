using KansasPPDMLoaderConsole;
using KansasPPDMLoaderLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDataTransfer, DataTransfer>();
        services.AddSingleton<App>();
    })
    .Build();

var app = host.Services.GetService<App>();
await app!.Run();

await host.RunAsync();

