namespace Seckim.NFTMarketplace.App.Application.Helpers;
public static class StringHelpers
{
  /// <summary>
  /// Shortens the address string by returning the first 7 characters and the last 5 characters with ... in between.
  /// </summary>
  /// <param name="address">The address string to be shortened.</param>
  /// <returns>The shortened address string.</returns>
  public static string ShortenAddress(string address)
  {
    if (address.Length < 10)
    {
      return address;
    }
    return $"{address.Substring(0, 7)}...{address.Substring(address.Length - 5)}";
  }
}
