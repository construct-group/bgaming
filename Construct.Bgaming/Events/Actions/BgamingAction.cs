using Construct.Bgaming.Types;

namespace Construct.Bgaming.Events.Actions;

public record BgamingAction
{
    public string Id { get; init; } = null!;
    public BgamingActionType Action { get; init; }
    public Currency Amount { get; init; } = null!;
}
