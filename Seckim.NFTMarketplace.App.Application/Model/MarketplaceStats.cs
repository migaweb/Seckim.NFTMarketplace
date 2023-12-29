using System.Text.Json.Serialization;

namespace Seckim.NFTMarketplace.App.Application.Model;
public class MarketplaceStats
{
  [JsonPropertyName("totalItems")]
  public string TotalItems { get; set; } = string.Empty;

  [JsonPropertyName("itemsSold")]
  public string ItemsSold { get; set; } = string.Empty;

  [JsonPropertyName("marketplaceBalance")]
  public string MarketplaceBalance { get; set; } = string.Empty;

  [JsonPropertyName("listingFee")]
  public string ListingFee { get; set; } = string.Empty;
}
