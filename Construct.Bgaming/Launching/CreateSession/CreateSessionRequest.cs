using Construct.Bgaming.Types;
using System.Net;


namespace Construct.Bgaming.Launching.CreateSession;

public record CreateSessionRequest
{
    public Game Game { get; init; }
    public Currency Money { get; init; } = null!;
    public IPAddress IPAddress { get; init; } = null!;
    public ClientType ClientType { get; init; }
    public RequestUrls RequestUrls { get; init; } = null!;
    public User User { get; init; } = null!;
}