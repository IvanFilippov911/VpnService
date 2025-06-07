using DrakarVpn.Core.AbstractsRepositories.Auth;
using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsServices.Auth;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Core.Services.Auth;
using DrakarVpn.Core.Services.Users;
using DrakarVpn.Infrastructure.Repositories;

namespace DrakarVpn.API.Settings.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
