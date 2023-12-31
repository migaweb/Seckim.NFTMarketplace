using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class NFTList : ComponentBase, IDisposable
{
  [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
  [Inject] private WalletStateProvider WalletStateProvider { get; set; } = null!;
  [Inject] private BusyOverlayService BusyOverlayService { get; set; } = null!;
  [Inject] private MarketplaceStateProvider MarketplaceStateProvider { get; set; } = null!;

  public List<NFTItem> NFTItems { get; set; } = [];

  private bool _displayCurrentUsersNFTsOnly = false;

  protected override void OnInitialized()
  {
    MarketplaceStateProvider.OnChange += StateHasChanged;
    WalletStateProvider.OnChange += OnWalletConnectedChange;
    base.OnInitialized();
  }

  private async Task SellNFT(NFTItem item)
  {
    try
    {
      BusyOverlayService.SetBusy("Selling ...");
      await MarketplaceStateProvider.SellNFT(item);
    }
    catch (Exception ex)
    {
      Console.Write(ex.Message);
    }
    finally
    {
      BusyOverlayService.SetNotBusy();
    }
  }

  private async Task BuyNFT(NFTItem item)
  {
    try
    {
      BusyOverlayService.SetBusy("Buying ...");
      await MarketplaceStateProvider.BuyNFT(item);
    } 
    catch (Exception ex)
    {
      Console.Write(ex.Message);
    }
    finally
    {
      BusyOverlayService.SetNotBusy();
    }
  }

  private async Task OnWalletConnectedChange(WalletAccount? account)
  {
    await Task.CompletedTask;
    StateHasChanged();
  }

  public void Dispose()
  {
    MarketplaceStateProvider.OnChange -= StateHasChanged;
    WalletStateProvider.OnChange -= OnWalletConnectedChange;
  }
}
