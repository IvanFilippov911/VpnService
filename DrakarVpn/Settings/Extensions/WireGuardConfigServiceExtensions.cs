using DrakarVpn.Core.AbstractsRepositories.WireGuard;
using DrakarVpn.Core.AbstractsServices.Configs;
using DrakarVpn.Core.Services.Configs;
using DrakarVpn.Domain.ModelsOptions;

namespace DrakarVpn.API.Settings.Extensions;

public static class WireGuardConfigServiceExtensions
{
    public static IServiceCollection AddWireGuardConfigServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WireGuardConfigOptions>(configuration.GetSection("WireGuardConfig"));

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IProcessExecutor, ProcessExecutor>();
        services.AddSingleton<IWireGuardConfigService, WireGuardConfigService>();

        return services;
    }
}

