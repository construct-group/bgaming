using Construct.Bgaming.Launching.CreateSession;
using Construct.Bgaming.Launching.StartDemoGame;
using Construct.Bgaming.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Construct.Bgaming;

public static class BgamingServiceBuilder
{
    public static IServiceCollection AddBgaming(this IServiceCollection services, BgamingConfigurationParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        services.AddSingleton(parameters);
        services.AddSingleton<IBgamingSecurityService, BgamingSecurityService>();
        services.AddSingleton<IStartDemoGameService, StartDemoGameService>();
        services.AddSingleton<ICreateSessionService, CreateSessionService>();
        return services;
    }
}