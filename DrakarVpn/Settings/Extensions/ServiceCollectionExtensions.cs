using DrakarVpn.Core.AbstractsRepositories;
using DrakarVpn.Core.AbstractsServices;
using DrakarVpn.Core.Services;
using DrakarVpn.Infrastructure.Repositories;

namespace DrakarVpn.API.Settings.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();

        return services;
    }
}
