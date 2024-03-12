using Newtonsoft.Json;

namespace Construct.Bgaming.Types;

public record RequestUrls
{
    [JsonProperty("return_url")] public string Return { get; set; } = null!;
    [JsonProperty("deposit_url")] public string Deposit { get; set; } = null!;
}
