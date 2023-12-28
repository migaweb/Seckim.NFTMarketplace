using Microsoft.AspNetCore.Components;
using Seckim.NFTMarketplace.App.Application.Constants;
using Seckim.NFTMarketplace.App.Application.Model;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class MintNewForm : ComponentBase
{
  [Inject] private NavigationManager NavigationManager { get; set; } = null!;
  [Inject] private BusyOverlayService BusyOverlayService { get; set; } = null!;
  public NewNFTModel Model { get; set; } = new() { Name = "One", Uri = "https://bronze-rapid-coyote-625.mypinata.cloud/ipfs/bafybeigkpyg3wub5e6tngf4pm2yk4pltp6e4amq52p6lbjkyu67vmthraq" };

  private async Task OnValidSubmit()
  {
    BusyOverlayService.SetBusy(Constants.MintingProgressMessage);
    await Task.Delay(13000);
    Console.WriteLine($"Name: {Model.Name}, Uri: {Model.Uri}");
    BusyOverlayService.SetNotBusy();

    NavigationManager.NavigateTo("/");
  }

  private void CancelMinting()
  {
    NavigationManager.NavigateTo("/");
  }
}

