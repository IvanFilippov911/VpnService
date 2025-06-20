using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Domain.Models;
using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Core.AbstractsServices.Peers;

public interface IPeerService
{
    Task<PeerAllocationResult> AddPeerAsync(string userId, string publicKey);

    Task<PagedResult<PeerDto>> GetAllPeersAsync(bool onlyActive = false, int offset = 0, int limit = 50);

    Task RemovePeerByPeerIdAsync(Guid peerId);

    Task<PagedResult<PeerDto>> GetPeersByFilterAsync(PeerFilterDto filter);

    Task<List<WireGuardPeerInfo>> GetWireGuardPeersAsync();
}