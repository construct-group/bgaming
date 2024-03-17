using Newtonsoft.Json;

namespace Construct.Bgaming.Events.Actions;

public class BgamingActionResponse
{
    [JsonProperty("balance")] public long Balance { get; init; }
    [JsonProperty("game_id")] public Guid GameId { get; init; }
    [JsonProperty("transactions")] public IEnumerable<BgamingTransaction> Transactions { get; init; } = null!;
}
