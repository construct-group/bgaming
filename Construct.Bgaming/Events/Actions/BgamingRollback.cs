namespace Construct.Bgaming.Events.Actions;

public record BgamingRollback
{
    public BgamingActionType Action { get; init; }
    public string Id { get; init; } = null!;
    public string OriginalId { get; init; } = null!;
}
