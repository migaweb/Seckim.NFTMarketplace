using Microsoft.AspNetCore.Components;
using Seckim.NFTMarketplace.App.Application.StateProviders;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class Progress : ComponentBase, IDisposable
{
  [Inject] public BusyOverlayService BusyOverlayService { get; set; } = null!;
  protected bool IsBusy { get; set; }

  protected override void OnInitialized()
  {
    BusyOverlayService.BusyStateChanged += HandleBusyStateChanged!;

    base.OnInitialized();
  }

  private void HandleBusyStateChanged(object sender, BusyChangedEventArgs e)
  {
    IsBusy = e.BusyState == BusyEnum.Busy;
    StateHasChanged();
  }

  public void Dispose()
  {
    BusyOverlayService.BusyStateChanged -= HandleBusyStateChanged!;
  }
}
