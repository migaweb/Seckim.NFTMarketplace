namespace Seckim.NFTMarketplace.App.Application.StateProviders;
public class BusyOverlayService
{
  public event EventHandler<BusyChangedEventArgs> BusyStateChanged = null!;

  public BusyEnum CurrentBusyState { get; set; }
  public string ProgressMessage { get; set; } = Constants.Constants.DefaulltProgressMessage;

  public void SetBusy(string message)
  {
    ProgressMessage = message;
    SetBusyState(BusyEnum.Busy);
  }

  public void SetNotBusy()
  {
    ProgressMessage = Constants.Constants.DefaulltProgressMessage;
    SetBusyState(BusyEnum.NotBusy);
  }

  private void SetBusyState(BusyEnum busyState)
  {
    CurrentBusyState = busyState;
    var eventArgs = new BusyChangedEventArgs();
    eventArgs.BusyState = CurrentBusyState;
    OnBusyStateChanged(eventArgs);
  }

  protected virtual void OnBusyStateChanged(BusyChangedEventArgs e)
  {
    var handler = BusyStateChanged;

    if (handler is not null)
    {
      handler(this, e);
    }
  }
}


public class BusyChangedEventArgs : EventArgs
{
  public BusyEnum BusyState { get; set; }
}

public enum BusyEnum
{
  Busy = 1,
  NotBusy = 2
}
