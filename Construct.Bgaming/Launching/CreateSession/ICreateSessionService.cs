namespace Construct.Bgaming.Launching.CreateSession;

public interface ICreateSessionService
{
    public Task<CreateSessionResponse> CreateSessionAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default);
}