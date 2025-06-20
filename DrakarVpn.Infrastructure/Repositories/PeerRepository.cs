using DrakarVpn.Core.AbstractsRepositories.Peers;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DrakarVpn.Infrastructure.Repositories;

public class PeerRepository : IPeerRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public PeerRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Peer?> GetActivePeerByUserIdAsync(string userId)
    {
        return await dbContext.Peers
            .FirstOrDefaultAsync(p => p.UserId == userId && p.IsActive);
    }

    public async Task<Peer?> GetPeerByIdAsync(Guid peerId)
    {
        return await dbContext.Peers.FirstOrDefaultAsync(p => p.Id == peerId);
    }
    
    public async Task AddPeerAsync(Peer peer)
    {
        dbContext.Peers.Add(peer);
        await dbContext.SaveChangesAsync();
    }

    public async Task MarkPeerAsInactiveAsync(Guid peerId)
    {
        await dbContext.Peers
            .Where(p => p.Id == peerId && p.IsActive)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.IsActive, false));
    }


    public async Task<(List<Peer> Peers, int TotalCount)> GetAllPeersPagedAsync(bool onlyActive, int offset, int limit)
    {
        var query = dbContext.Peers.AsQueryable();

        if (onlyActive)
            query = query.Where(p => p.IsActive);

        var totalCount = await query.CountAsync();

        var peers = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return (peers, totalCount);
    }

    public async Task<(List<Peer> Peers, int TotalCount)> GetPeersByFilterPagedAsync(PeerFilterDto filter)
    {
        var query = dbContext.Peers.AsQueryable();

        if (!string.IsNullOrEmpty(filter.UserId))
            query = query.Where(p => p.UserId == filter.UserId);

        if (filter.IsActive.HasValue)
            query = query.Where(p => p.IsActive == filter.IsActive);

        var totalCount = await query.CountAsync();

        var peers = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip(filter.Offset)
            .Take(filter.Limit)
            .ToListAsync();

        return (peers, totalCount);
    }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await dbContext.Database.BeginTransactionAsync();
    }

}

