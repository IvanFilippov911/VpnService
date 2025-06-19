using DrakarVpn.Domain.ModelsOptions;
using WireGuardAgent.API.AbstractsRepositories;
using WireGuardAgent.API.AbstractsServices;
using WireGuardAgent.API.Repositories;
using WireGuardAgent.API.Services;

namespace WireGuardAgent.API.Settings.Extensions;

public static class WireGuardAgentServiceExtensions
{
    public static IServiceCollection AddWireGuardAgentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WireGuardConfigOptions>(
            configuration.GetSection("WireGuardConfigOptions"));

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IProcessExecutor, ProcessExecutor>();
        services.AddScoped<IWireGuardConfigService, WireGuardConfigService>();


        return services;
    }
}
