using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Peers;
using Microsoft.EntityFrameworkCore.Storage;

namespace DrakarVpn.Core.AbstractsRepositories.Peers;

public interface IPeerRepository
{
    Task<Peer?> GetActivePeerByUserIdAsync(string userId);
    Task AddPeerAsync(Peer peer);
    Task<Peer?> GetPeerByIdAsync(Guid peerId);
    Task MarkPeerAsInactiveAsync(Guid peerId);
    

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task<(List<Peer> Peers, int TotalCount)> GetAllPeersPagedAsync(bool onlyActive, int offset, int limit);
    Task<(List<Peer> Peers, int TotalCount)> GetPeersByFilterPagedAsync(PeerFilterDto filter);

}


