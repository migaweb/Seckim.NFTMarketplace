using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Seckim.NFTMarketplace.App.Application.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.ConfigureApplication();

await builder.Build().RunAsync();
