using Microsoft.AspNetCore.Http;

namespace Construct.Bgaming.Events.Dispatching;

public interface IEventDispatcherService
{
    public Task<DispatchingResult> Dispatch(HttpRequest request);
}
