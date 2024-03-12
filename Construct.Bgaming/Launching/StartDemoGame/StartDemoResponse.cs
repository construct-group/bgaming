using Construct.Bgaming.Types;

namespace Construct.Bgaming.Launching.StartDemoGame;

public record StartDemoResponse
{
    public LaunchOptions LaunchOptions { get; init; } = null!;
}
