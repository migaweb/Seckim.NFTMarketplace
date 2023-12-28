using System.Text.Json.Serialization;

namespace Seckim.NFTMarketplace.App.Application.Model;

public class WalletAccount
{
  [JsonPropertyName("account")]
  public string Account { get; set; } = string.Empty;

  [JsonPropertyName("balance")]
  public string Balance { get; set; } = string.Empty;
}
