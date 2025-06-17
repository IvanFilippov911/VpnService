using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.Peers;

public interface IPeerService
{
    Task<AddPeerResultDto> AddPeerAsync(Guid userId, string publicKey, string privateKey);
    Task<List<PeerResponseDto>> GetAllPeersAsync(bool onlyActive = false);
    Task RemovePeerByPeerIdAsync(Guid peerId);
    Task<List<PeerResponseDto>> GetPeersByFilterAsync(PeerFilterDto filter);
    Task<List<WireGuardPeerInfo>> GetWireGuardPeersAsync();
}

