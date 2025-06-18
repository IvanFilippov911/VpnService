using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Models;
using DrakarVpn.Domain.ModelsOptions;
using Microsoft.Extensions.Options;

namespace DrakarVpn.Core.Services.WireGuard;

public class WireGuardClientConfigGenerator : IWireGuardClientConfigGenerator
{
    private readonly WireGuardServerInfo serverInfo;

    public WireGuardClientConfigGenerator(IOptions<WireGuardConfigOptions> options)
    {
        serverInfo = new WireGuardServerInfo
        {
            PublicKey = options.Value.ServerPublicKey,
            Endpoint = options.Value.Endpoint
        };
    }

    public WireGuardServerInfo GetConfig()
    {
        return new WireGuardServerInfo
        {
            PublicKey = serverInfo.PublicKey,
            Endpoint = serverInfo.Endpoint,
            AllowedIPs = "0.0.0.0/0, ::/0",
            PersistentKeepalive = 25
        };
    }
}

