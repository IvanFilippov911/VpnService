using DrakarVpn.Core.AbstractsRepositories.Auth;
using DrakarVpn.Core.AbstractsRepositories.Logging;
using DrakarVpn.Core.AbstractsRepositories.Peers;
using DrakarVpn.Core.AbstractsRepositories.Subscriptions;
using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsRepositories.VpnDevice;
using DrakarVpn.Core.AbstractsServices.Auth;
using DrakarVpn.Core.AbstractsServices.Peers;
using DrakarVpn.Core.AbstractsServices.Subscriptions;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Core.AbstractsServices.UserVpnDevice;
using DrakarVpn.Core.Services.Auth;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Core.Services.Subscriptions;
using DrakarVpn.Core.Services.Tariffs;
using DrakarVpn.Core.Services.Users;
using DrakarVpn.Core.Services.UserVpnDevices;
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
        services.AddScoped<IUserVpnDeviceService, UserVpnDeviceService>();
        services.AddScoped<IMasterLogService, MasterLogService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IPeerRepository, PeerRepository>();
        services.AddScoped<IPeerService, PeerService>();
        services.AddScoped<IUserVpnDeviceRepository, UserVpnDeviceRepository>();
        services.AddScoped<IMongoLogRepository, MongoLogRepository>();

        return services;
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}
