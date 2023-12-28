using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class NFTList : ComponentBase, IDisposable
{
  [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
  [Inject] private WalletStateProvider WalletStateProvider { get; set; } = null!;

  public List<NFTItem> NFTItems { get; set; } = [];

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
        StateHasChanged();
      }
    }
  }

  private async Task OnWalletConnectedChange(WalletAccount? account)
  {
    if (WalletStateProvider.IsConnected)
    {
      NFTItems = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");
      Console.WriteLine(NFTItems.Count);
    }
    StateHasChanged();
  }

  public void Dispose()
  {
    WalletStateProvider.OnChange -= OnWalletConnectedChange;
  }
}
