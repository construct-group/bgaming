namespace Construct.Bgaming;

public record BgamingConfigurationParameters
{
    public string ID { get; init; } = null!;
    public string Token { get; init; } = null!;
    public Uri URL { get; init; } = null!;
}
