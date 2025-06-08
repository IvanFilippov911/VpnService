using DrakarVpn.Core.AbstractsRepositories.Auth;
using DrakarVpn.Core.AbstractsRepositories.Subscriptions;
using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsServices.Auth;
using DrakarVpn.Core.AbstractsServices.Subscriptions;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Core.Services.Auth;
using DrakarVpn.Core.Services.Subscriptions;
using DrakarVpn.Core.Services.Tariffs;
using DrakarVpn.Core.Services.Users;
using DrakarVpn.Infrastructure.Repositories;
using DrakarVpn.Infrastructure.Repositories.Subscriptions;

namespace DrakarVpn.API.Settings.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        return services;
    }
}
