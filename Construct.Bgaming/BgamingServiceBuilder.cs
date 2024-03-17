using Construct.Bgaming.Events.Actions;
using Construct.Bgaming.Events.Dispatching;
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
        services.AddTransient<IBgamingSecurityService, BgamingSecurityService>();
        services.AddTransient<IStartDemoGameService, StartDemoGameService>();
        services.AddTransient<ICreateSessionService, CreateSessionService>();
        services.AddTransient<IEventDispatcherService, EventDispatcherService>();
        services.AddTransient<IActionExtractorService, ActionExtractorService>();
        return services;
    }
}