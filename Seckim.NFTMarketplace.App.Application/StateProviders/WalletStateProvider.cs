using Microsoft.JSInterop;
using Seckim.NFTMarketplace.App.Application.Enums;
using Seckim.NFTMarketplace.App.Application.Model;

namespace Seckim.NFTMarketplace.App.Application.StateProviders;
public class WalletStateProvider : IDisposable
{
  public Func<WalletAccount?, Task>? OnChange { get; set; }
  public Action<WalletState>? OnWalletStateChange { get; set; }
  public WalletAccount? WalletAccount => _walletAccount;
  public WalletState WalletState => _state;

  private static Func<WalletAccount?, Task>? _onWalletUpdated;
  private readonly IJSRuntime _jsRuntime;
  private WalletAccount? _walletAccount;
  private WalletState _state = WalletState.NotConnected;

  public WalletStateProvider(IJSRuntime jsRuntime)
  {
    _jsRuntime = jsRuntime;
    _onWalletUpdated += WalletUpdated;
  }

  public bool IsConnected => _state == WalletState.Connected;

  public async Task ConnectWallet()
  {
    _state = WalletState.Connecting;
    WalletStateUpdated();
    try
    {
      _walletAccount = await _jsRuntime.InvokeAsync<WalletAccount?>("connectWallet");

      if (_walletAccount != null && !string.IsNullOrWhiteSpace(_walletAccount.Account))
      {
        _state = WalletState.Connected;
        WalletStateUpdated();
      }
    }
    catch (Exception ex)
    {
#if DEBUG
      Console.WriteLine(ex);
#endif
      _state = WalletState.NotConnected;
      WalletStateUpdated();
    }
    finally
    {
      await WalletUpdated(_walletAccount);
    }
  }

  [JSInvokable("UpdateAccountInfo")]
  public static async Task UpdateAccountInfo(WalletAccount walletAccount)
  {
    if (_onWalletUpdated is { } actionAsync)
    {
      await actionAsync(walletAccount);
    }
  }

  private void WalletStateUpdated()
  {
    OnWalletStateChange?.Invoke(_state);
  }

  private async Task WalletUpdated(WalletAccount? walletAccount)
  {
    await Task.CompletedTask;

    _walletAccount = walletAccount;

    OnChange?.Invoke(walletAccount);
  }

  #region IDisposable Support
  public void Dispose()
  {
    _onWalletUpdated -= WalletUpdated;

    GC.SuppressFinalize(this);
  }

  #endregion
}
