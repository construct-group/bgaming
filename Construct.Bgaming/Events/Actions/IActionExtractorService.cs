using Construct.Bgaming.Events.Dispatching;
using Microsoft.AspNetCore.Http;

namespace Construct.Bgaming.Events.Actions;

public interface IActionExtractorService
{
    public BgamingActionRequest ExtractActions(
        DispatchingResult result,
        CancellationToken cancellationToken = default);

    public BgamingRollbackRequest ExtractRollback(
       DispatchingResult result,
       CancellationToken cancellationToken = default);
}
