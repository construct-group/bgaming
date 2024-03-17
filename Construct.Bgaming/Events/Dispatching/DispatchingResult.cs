using Construct.Bgaming.Types;
using Newtonsoft.Json.Linq;

namespace Construct.Bgaming.Events.Dispatching;

public record DispatchingResult
{
    public DispatchingResult(BgamingEventType eventType, Error? error, JObject? @object = null)
    {
        this.EventType = eventType;
        this.Error = error;
        this.Object = @object;
    }

    public BgamingEventType EventType { get; init; }
    public JObject? Object { get; init; }
    public Error? Error { get; init; }
}
