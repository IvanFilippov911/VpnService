using DrakarVpn.Core.Services.Subscriptions;

namespace DrakarVpn.API.Settings.Extensions;

public static class BackgroundWorkersRegistration
{
    public static IServiceCollection AddBackgroundWorkers(this IServiceCollection services)
    {
        services.AddHostedService<SubscriptionAutoDeactivator>();
        return services;
    }
}

