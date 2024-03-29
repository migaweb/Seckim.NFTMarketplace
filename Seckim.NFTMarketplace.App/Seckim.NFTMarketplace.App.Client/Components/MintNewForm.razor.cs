﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Constants;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class MintNewForm : ComponentBase, IDisposable
{
  [Inject] private NavigationManager NavigationManager { get; set; } = null!;
  [Inject] private BusyOverlayService BusyOverlayService { get; set; } = null!;
  [Inject] private WalletStateProvider WalletStateProvider { get; set; } = null!;
  [Inject] private MarketplaceStateProvider MarketplaceStateProvider { get; set; } = null!;
  [Inject] private IJSRuntime IJSRuntime { get; set; } = null!;
  public NewNFTModel Model { get; set; } = new() { Name = "One", Uri = "https://bronze-rapid-coyote-625.mypinata.cloud/ipfs/bafybeigkpyg3wub5e6tngf4pm2yk4pltp6e4amq52p6lbjkyu67vmthraq" };

  protected override void OnInitialized()
  {
    WalletStateProvider.OnChange += OnWalletStateChange;
  }

  private async Task OnWalletStateChange(WalletAccount? account)
  {
    await Task.CompletedTask;
    StateHasChanged();
  }

  private async Task OnValidSubmit()
  {
    try
    {
      await MintNewNFT();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
    }
  }

  private async Task MintNewNFT()
  {
    BusyOverlayService.SetBusy(Constants.MintingProgressMessage);
    var result = await IJSRuntime.InvokeAsync<MintNFTResult>("mintNFT", Model.Uri);

    BusyOverlayService.SetBusy(Constants.ListingProgressMessage);
    await IJSRuntime.InvokeAsync<ListNFTResult>("listNFT", result.TokenId, Model.Name, Model.Uri);

    BusyOverlayService.SetBusy(Constants.MarketUpdateProgressMessage);
    await MarketplaceStateProvider.Refresh();

    BusyOverlayService.SetNotBusy();

    NavigationManager.NavigateTo("/");
  }

  private void CancelMinting()
  {
    NavigationManager.NavigateTo("/");
  }

  public void Dispose()
  {
    WalletStateProvider.OnChange -= OnWalletStateChange;
  }
}

