namespace Construct.Bgaming.Launching.StartDemoGame;

public interface IStartDemoGameService
{
    public Task<StartDemoResponse> StartDemoAsync(
        StartDemoRequest request,
        CancellationToken cancellationToken = default);
}
