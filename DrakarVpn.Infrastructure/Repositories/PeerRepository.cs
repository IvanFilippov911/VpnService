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

    public async Task<Peer?> GetActivePeerByUserIdAsync(Guid userId)
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


    public async Task<List<Peer>> GetAllPeersAsync(bool onlyActive = false)
    {
        if (onlyActive)
        {
            return await dbContext.Peers.Where(p => p.IsActive).ToListAsync();
        }
        else
        {
            return await dbContext.Peers.ToListAsync();
        }
    }

    public async Task<List<Peer>> GetPeersByFilterAsync(PeerFilterDto filter)
    {
        var query = dbContext.Peers.AsQueryable();

        if (filter.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == filter.IsActive.Value);
        }

        if (filter.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == filter.UserId.Value);
        }

        if (!string.IsNullOrEmpty(filter.AssignedIp))
        {
            query = query.Where(p => p.AssignedIP == filter.AssignedIp);
        }

        if (filter.CreatedAfter.HasValue)
        {
            query = query.Where(p => p.CreatedAt >= filter.CreatedAfter.Value);
        }

        if (filter.CreatedBefore.HasValue)
        {
            query = query.Where(p => p.CreatedAt <= filter.CreatedBefore.Value);
        }

        return await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await dbContext.Database.BeginTransactionAsync();
    }

}

