using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Peers;
using Microsoft.EntityFrameworkCore.Storage;

namespace DrakarVpn.Core.AbstractsRepositories.Peers;

public interface IPeerRepository
{
    Task<Peer?> GetActivePeerByUserIdAsync(Guid userId);
    Task AddPeerAsync(Peer peer);
    Task<List<Peer>> GetAllPeersAsync(bool onlyActive = false);
    Task<Peer?> GetPeerByIdAsync(Guid peerId);
    Task MarkPeerAsInactiveAsync(Guid peerId);
    Task<List<Peer>> GetPeersByFilterAsync(PeerFilterDto filter);

    Task<IDbContextTransaction> BeginTransactionAsync();
}


