using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Model;
using System.Collections.ObjectModel;

namespace Seckim.NFTMarketplace.App.Application.StateProviders;
public class MarketplaceStateProvider(IJSRuntime jSRuntime)
{
  private MarketplaceStats? _marketplaceStats;
  private ObservableCollection<NFTItem> _nftItems = [];
  private readonly IJSRuntime _jSRuntime = jSRuntime;

  public event Action? OnChange;
  public void NotifyStateChanged() => OnChange?.Invoke();

  public async Task Init()
  {
    if (MarketplaceStats == null)
      await FetchMarketplaceStats();

    if (NFTItems.Count == 0)
      await FetchAllMarketItems();
  }

  public async Task FetchMarketplaceStats()
  {
    MarketplaceStats = await _jSRuntime.InvokeAsync<MarketplaceStats>("getMarketplaceStats");
  }

  public async Task FetchAllMarketItems()
  {
    NFTItems = await _jSRuntime.InvokeAsync<ObservableCollection<NFTItem>>("fetchAllMarketItems");
  }

  public async Task BuyNFT(NFTItem item)
  {
    await _jSRuntime.InvokeVoidAsync("buyNFT", item.TokenId);
    await FetchAllMarketItems();
    await FetchMarketplaceStats();
  }

  public async Task SellNFT(NFTItem item)
  {
    await _jSRuntime.InvokeVoidAsync("resellNFT", item.TokenId);
    await FetchAllMarketItems();
    await FetchMarketplaceStats();
  }

  public MarketplaceStats MarketplaceStats
  {
    get => _marketplaceStats ?? new();
    set
    {
      _marketplaceStats = value;
      NotifyStateChanged();
    }
  }

  public ObservableCollection<NFTItem> NFTItems
  {
    get => _nftItems;
    set
    {
      _nftItems = value;
      NotifyStateChanged();
    }
  }
}
