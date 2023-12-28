using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Model;

namespace Seckim.NFTMarketplace.App.Client.Components;

public partial class NFTList : ComponentBase
{
  [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      var result = await JSRuntime.InvokeAsync<List<NFTItem>>("fetchAllMarketItems");

      foreach (var item in result)
      {
        Console.WriteLine(item.Name + " " + item.TokenURI);
      } 
    }
  }
}
