using System.Text.Json.Serialization;

namespace Seckim.NFTMarketplace.App.Application.Model;
public class NFTItem
{
  [JsonPropertyName("price")]
  public string Price { get; set; } = string.Empty;

  [JsonPropertyName("tokenId")]
  public string TokenId { get; set; } = string.Empty;

  [JsonPropertyName("seller")]
  public string Seller { get; set; } = string.Empty;

  [JsonPropertyName("owner")]
  public string Owner { get; set; } = string.Empty;

  [JsonPropertyName("name")]
  public string Name { get; set; } = string.Empty;

  [JsonPropertyName("tokenURI")]
  public string TokenURI { get; set; } = string.Empty;

  [JsonPropertyName("isListed")]
  public bool IsListed { get; set; }
}
