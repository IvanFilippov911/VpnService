
using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.WireGuard;

public interface IWireGuardClientConfigGenerator
{
    string GenerateClientConfig(WireGuardPeerInfo peerInfo);
}

