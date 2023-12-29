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

  public List<NFTItem> NFTItems { get; set; } = [];

  private bool displayCurrentUsersNFTsOnly = false;

  protected override void OnInitialized()
  {
    WalletStateProvider.OnChange += OnWalletConnectedChange;
    base.OnInitialized();
  }

  // TODO: Create state provider for NFTItems

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      if (WalletStateProvider.IsConnected)
      {
        NFTItems = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");
        await JSRuntime.InvokeAsync<MarketplaceStats>("getMarketplaceStats");
        StateHasChanged();
      }
    }
  }

  private async Task SellNFT(NFTItem item)
  {
    try
    {
      BusyOverlayService.SetBusy("Selling ...");
      await JSRuntime.InvokeVoidAsync("resellNFT", item.TokenId);
      BusyOverlayService.SetNotBusy();

      NFTItems = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");
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
      await JSRuntime.InvokeVoidAsync("buyNFT", item.TokenId);
      BusyOverlayService.SetNotBusy();

      NFTItems = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");
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
    if (WalletStateProvider.IsConnected)
    {
      NFTItems = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");
      Console.WriteLine(NFTItems.Count);
      await JSRuntime.InvokeAsync<MarketplaceStats>("getMarketplaceStats");
    }
    StateHasChanged();
  }

  public void Dispose()
  {
    WalletStateProvider.OnChange -= OnWalletConnectedChange;
  }
}
