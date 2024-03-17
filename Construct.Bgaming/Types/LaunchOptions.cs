using Newtonsoft.Json;

namespace Construct.Bgaming.Types;

public record LaunchOptions
{
    [JsonProperty("game_url")] public Uri GameUrl { get; init; } = null!;
    [JsonProperty("strategy")] public LaunchStrategy Strategy { get; init; }
}
