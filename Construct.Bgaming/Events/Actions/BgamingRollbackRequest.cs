using Construct.Bgaming.Types;

namespace Construct.Bgaming.Events.Actions;

public record BgamingRollbackRequest
{
    public Guid UserId { get; init; }
    public Guid GameId { get; init; }
    public Guid SessionId { get; init; }
    public Game Game { get; init; }
    public bool Finished { get; init; }
    public string Currency { get; init; } = null!;
    public IEnumerable<BgamingRollback> Actions { get; init; } = null!;
}
