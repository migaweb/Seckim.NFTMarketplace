using Microsoft.AspNetCore.Components;
using Seckim.NFTMarketplace.App.Application.Enums;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class Wallet : ComponentBase, IDisposable
{
  [Inject] public WalletStateProvider WalletStateProvider { get; set; } = null!;
  public WalletAccount? Account { get; set; }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      await WalletStateProvider.ConnectWallet();
    }
    await base.OnAfterRenderAsync(firstRender);
  }

  protected override void OnInitialized()
  {
    WalletStateProvider.OnChange += AccountInfoUpdated;
    WalletStateProvider.OnWalletStateChange += WalletStateUpdated;
    base.OnInitialized();
  }

  private async Task ConnectWallet()
  {
    await WalletStateProvider.ConnectWallet();
  }

  private void WalletStateUpdated(WalletState walletState)
  {
    StateHasChanged();
  }

  private async Task AccountInfoUpdated(WalletAccount? accountInfo)
  {
    Account = accountInfo;
    await Task.CompletedTask;

    StateHasChanged();
  }

  #region IDisposable Support
  public void Dispose()
  {
    WalletStateProvider.OnChange -= AccountInfoUpdated;
    WalletStateProvider.OnWalletStateChange += WalletStateUpdated;

    GC.SuppressFinalize(this);
  }
  #endregion
}
