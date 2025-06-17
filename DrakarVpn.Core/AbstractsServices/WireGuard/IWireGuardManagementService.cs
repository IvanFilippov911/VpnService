using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.WireGuard;

public interface IWireGuardManagementService
{
    Task<List<WireGuardPeerInfo>> GetPeersAsync();
    Task AddPeerAsync(WireGuardPeerInfo peerInfo);
    Task RemovePeerAsync(string publicKey);
}

