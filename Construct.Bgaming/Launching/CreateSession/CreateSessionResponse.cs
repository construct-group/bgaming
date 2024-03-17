using Construct.Bgaming.Types;

namespace Construct.Bgaming.Launching.CreateSession;

public record CreateSessionResponse
{
    public LaunchOptions LaunchOptions { get; init; } = null!;
}