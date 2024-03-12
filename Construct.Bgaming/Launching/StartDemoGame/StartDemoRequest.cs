using Construct.Bgaming.Types;
using System.Net;

namespace Construct.Bgaming.Launching.StartDemoGame;

public record StartDemoRequest
{
    public Game Game { get; init; }
    public ClientType ClientType { get; init; }
    public IPAddress Address { get; init; } = null!;
    public RequestUrls RequestUrls { get; init; } = null!;
}
