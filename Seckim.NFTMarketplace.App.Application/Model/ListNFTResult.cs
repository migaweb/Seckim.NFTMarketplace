using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seckim.NFTMarketplace.App.Application.Model;
public class ListNFTResult
{
  [JsonPropertyName("transaction")]
  public string TransactionJSON { get; set; } = string.Empty;

  [JsonIgnore]
  public TransactionResult Transaction => JsonSerializer.Deserialize<TransactionResult>(TransactionJSON) ?? new();

  public static string TestTransactionResult = @"
  {
  ""_type"": ""TransactionReceipt"",
  ""accessList"": null,
  ""blockNumber"": null,
  ""blockHash"": null,
  ""chainId"": null,
  ""data"": ""0xdbcc7575000000000000000000000000538aab320741ba2685b3754724e6161eee6495d00000000000000000000000000000000000000000000000000000000000000008000000000000000000000000000000000000000000000000000000000000008000000000000000000000000000000000000000000000000000000000000000c000000000000000000000000000000000000000000000000000000000000000034f6e650000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000006f68747470733a2f2f62726f6e7a652d72617069642d636f796f74652d3632352e6d7970696e6174612e636c6f75642f697066732f62616679626569676b70796733777562356536746e676634706d32796b34706c7470366534616d71353270366c626a6b79753637766d74687261710000000000000000000000000000000000"",
  ""from"": ""0x1dCa9aEe3095f7C5B211626615fAD6aDE02BCB8C"",
  ""gasLimit"": ""3000000"",
  ""gasPrice"": ""121717451392"",
  ""hash"": ""0x988e9a8bb2f1ec9a462dd01d01a28ab27e0c98ba3313f907db43b3828d126dff"",
  ""maxFeePerGas"": ""121717451392"",
  ""maxPriorityFeePerGas"": ""1500000000"",
  ""nonce"": 44,
  ""signature"": {
    ""_type"": ""signature"",
    ""networkV"": null,
    ""r"": ""0x40067e123db07f88fa9c9071e801309c06e595255490daa0f7a88ea2934ab90a"",
    ""s"": ""0x412b28b56898143695f3ad9c334eec829d2f7ab598f43de5a5c50f8df6f38016"",
    ""v"": 28
  },
  ""to"": ""0x8d558Ed6d49Cc9d596c5657903E9E867Fa52Eb7b"",
  ""type"": 2,
  ""value"": ""100000000000000""
}
";
}

public class TransactionResult
{
  [JsonPropertyName("_type")]
  public string Type { get; set; } = string.Empty;

  [JsonPropertyName("accessList")]
  public string AccessList { get; set; } = string.Empty;

  [JsonPropertyName("blockNumber")]
  public string BlockNumber { get; set; } = string.Empty;

  [JsonPropertyName("blockHash")]
  public string BlockHash { get; set; } = string.Empty;

  [JsonPropertyName("chainId")]
  public string ChainId { get; set; } = string.Empty;

  [JsonPropertyName("data")]
  public string Data { get; set; } = string.Empty;

  [JsonPropertyName("from")]
  public string From { get; set; } = string.Empty;

  [JsonPropertyName("gasLimit")]
  public string GasLimit { get; set; } = string.Empty;

  [JsonPropertyName("gasPrice")]
  public string GasPrice { get; set; } = string.Empty;

  [JsonPropertyName("hash")]
  public string Hash { get; set; } = string.Empty;

  [JsonPropertyName("maxFeePerGas")]
  public string MaxFeePerGas { get; set; } = string.Empty;

  [JsonPropertyName("maxPriorityFeePerGas")]
  public string MaxPriorityFeePerGas { get; set; } = string.Empty;

  [JsonPropertyName("nonce")]
  public int Nonce { get; set; }

  [JsonPropertyName("signature")]
  public Signature Signature { get; set; } = new();

  [JsonPropertyName("to")]
  public string To { get; set; } = string.Empty;

  [JsonPropertyName("type")]
  public int Type2 { get; set; }

  [JsonPropertyName("value")]
  public string Value { get; set; } = string.Empty;
}

public class Signature
{
  [JsonPropertyName("_type")]
  public string Type { get; set; } = string.Empty;

  [JsonPropertyName("networkV")]
  public string NetworkV { get; set; } = string.Empty;

  [JsonPropertyName("r")]
  public string R { get; set; } = string.Empty;

  [JsonPropertyName("s")]
  public string S { get; set; } = string.Empty;

  [JsonPropertyName("v")]
  public int V { get; set; }
}



