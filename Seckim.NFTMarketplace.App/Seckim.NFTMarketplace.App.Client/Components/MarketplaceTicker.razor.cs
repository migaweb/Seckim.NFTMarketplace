using Microsoft.AspNetCore.Components;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class MarketplaceTicker : ComponentBase, IDisposable
{
  [Inject] public MarketplaceStateProvider MarketplaceStateProvider { get; set; } = null!;

  protected override void OnInitialized()
  {
    MarketplaceStateProvider.OnChange += StateHasChanged;
  }

  public void Dispose()
  {
    MarketplaceStateProvider.OnChange -= StateHasChanged;
  }
}
