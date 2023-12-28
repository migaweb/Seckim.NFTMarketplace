using System.ComponentModel.DataAnnotations;

namespace Seckim.NFTMarketplace.App.Application.Model;
public class NewNFTModel
{
  [Required]
  public string? Uri { get; set; }

  [Required]
  public string? Name { get; set; }
}
