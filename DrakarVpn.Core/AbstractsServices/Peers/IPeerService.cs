using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.Peers;

public interface IPeerService
{
    Task<PeerAllocationResult> AddPeerAsync(string userId, string publicKey);
    Task<List<PeerDto>> GetAllPeersAsync(bool onlyActive = false);
    Task RemovePeerByPeerIdAsync(Guid peerId);
    Task<List<PeerDto>> GetPeersByFilterAsync(PeerFilterDto filter);
    Task<List<WireGuardPeerInfo>> GetWireGuardPeersAsync();
}

