using Microsoft.AspNetCore.Components;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class MarketplaceTicker : ComponentBase, IDisposable
{
  [Inject] public MarketplaceStateProvider MarketplaceStateProvider { get; set; } = null!;
  [Inject] private WalletStateProvider WalletStateProvider { get; set; } = null!;

  protected override void OnInitialized()
  {
    MarketplaceStateProvider.OnChange += StateHasChanged;
    WalletStateProvider.OnChange += OnWalletConnectedChange;
  }

  private async Task OnWalletConnectedChange(WalletAccount? account)
  {
    if (WalletStateProvider.IsConnected)
    {
      await MarketplaceStateProvider.Init();
    }
    StateHasChanged();
  }

  public void Dispose()
  {
    WalletStateProvider.OnChange -= OnWalletConnectedChange;
    MarketplaceStateProvider.OnChange -= StateHasChanged;
    GC.SuppressFinalize(this);
  }
}
