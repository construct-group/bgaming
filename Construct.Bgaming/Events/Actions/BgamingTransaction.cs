using Newtonsoft.Json;

namespace Construct.Bgaming.Events.Actions;

public record BgamingTransaction
{
    [JsonProperty("action_id")] public string ActionId { get; init; }
    [JsonProperty("tx_id")] public Guid TransationId { get; init; }
}
