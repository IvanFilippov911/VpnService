using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Models;
using DrakarVpn.Domain.ModelsOptions;
using Microsoft.Extensions.Options;
using System.Text;

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

    public string GenerateClientConfig(WireGuardPeerInfo peerInfo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("[Interface]");
        sb.AppendLine("PrivateKey = #INSERT_PRIVATE_KEY_HERE"); 
        sb.AppendLine($"Address = {peerInfo.AllowedIp}");
        sb.AppendLine();
        sb.AppendLine("[Peer]");
        sb.AppendLine($"PublicKey = {serverInfo.PublicKey}");
        sb.AppendLine($"Endpoint = {serverInfo.Endpoint}");
        sb.AppendLine("AllowedIPs = 0.0.0.0/0, ::/0");
        sb.AppendLine("PersistentKeepalive = 25");

        return sb.ToString();
    }
}
