using DrakarVpn.Core.AbstractsRepositories.VpnDevice;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DrakarVpn.Infrastructure.Repositories;

public class UserVpnDeviceRepository : IUserVpnDeviceRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public UserVpnDeviceRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<UserVpnDevice>> GetAllByUserIdAsync(string userId)
    {
        return await dbContext.UserVpnDevices
            .Where(d => d.UserId == userId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
    }

    public async Task<UserVpnDevice?> GetByIdAsync(Guid id)
    {
        return await dbContext.UserVpnDevices.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(UserVpnDevice device)
    {
        await dbContext.UserVpnDevices.AddAsync(device);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await dbContext.UserVpnDevices
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<int> CountByUserIdAsync(string userId)
    {
        return await dbContext.UserVpnDevices.CountAsync(d => d.UserId == userId);
    }

    public async Task<UserVpnDevice?> FindByPublicKeyAsync(string publicKey)
    {
        var peer = await dbContext.Peers
            .FirstOrDefaultAsync(p => p.PublicKey == publicKey && p.IsActive);

        if (peer == null)
            return null;

        return await dbContext.UserVpnDevices
            .FirstOrDefaultAsync(d => d.PeerId == peer.Id && d.IsActive);
    }

}

