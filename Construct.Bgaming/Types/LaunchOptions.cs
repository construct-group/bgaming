namespace Construct.Bgaming.Types;

public record LaunchOptions
{
    public Uri GameUrl { get; init; } = null!;
    public LaunchStrategy Strategy { get; init; }
}
