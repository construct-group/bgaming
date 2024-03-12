using Construct.Bgaming.Types;
using Construct.Bgaming.Types.Currency;
using System.Net;


namespace Construct.Bgaming.Launching.CreateSession;

public record CreateSessionRequest
{
    public Guid UserId { get; init; }
    public CurrencyBase Currency { get; init; } 
    public Game Game { get; init; }
    public Guid GameId { get; init; }
    public bool Finished { get; init; } = null!;
    public RequestActions Actions { get; init; } = null!;
}