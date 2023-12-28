using Microsoft.Extensions.DependencyInjection;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Application.Configuration;
public static class Configuration
{
  public static IServiceCollection ConfigureApplication(this IServiceCollection services)
  {
    services.AddScoped<WalletStateProvider>();
    services.AddScoped<BusyOverlayService>();

    return services;
  }
}
