using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Seckim.NFTMarketplace.App.Application.Configuration;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.ConfigureApplication();

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
