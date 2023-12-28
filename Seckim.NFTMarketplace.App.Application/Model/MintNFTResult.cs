using System.Text.Json.Serialization;

namespace Seckim.NFTMarketplace.App.Application.Model;
public class MintNFTResult
{
  [JsonPropertyName("tokenId")]
  public string TokenId { get; set; } = string.Empty;
}
