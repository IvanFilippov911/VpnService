using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Core.Services.WireGuard;
using DrakarVpn.Domain.ModelsOptions;
using DrakarVpn.Infrastructure.Persistence;

namespace DrakarVpn.API.Settings.Extensions;

public static class WireGuardConfigServiceExtensions
{
    public static IServiceCollection AddWireGuardConfigServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WireGuardConfigOptions>(configuration.GetSection("WireGuardConfig"));
        services.AddSingleton<IWireGuardClientConfigGenerator, WireGuardClientConfigGenerator>();
        services.AddHttpClient<IWireGuardManagementService, WireGuardManagementService>(client =>
        {
            client.BaseAddress = new Uri("http://77.221.153.119:5000");
            client.Timeout = TimeSpan.FromSeconds(5); 
        });

        services.AddScoped<IWireGuardIpAllocator>(provider =>
        {
            var dbContext = provider.GetRequiredService<DrakarVpnDbContext>();

            var alreadyAllocatedIps = dbContext.Peers
                .Where(p => p.IsActive)
                .Select(p => p.AssignedIP)
                .ToList();

            return new WireGuardIpAllocator(alreadyAllocatedIps);
        });


        return services;
    }
}

