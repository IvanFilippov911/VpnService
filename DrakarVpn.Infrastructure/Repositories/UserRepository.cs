using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DrakarVpn.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public UserRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await dbContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(string userId)
    {
        return await dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<List<AppUser>> FilterUsersAsync(UserFilterDto filter)
    {
        var query = dbContext.Users
            .Include(u => u.Subscriptions) 
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Email))
            query = query.Where(u => u.Email.Contains(filter.Email));
        
        if (filter.IsVerified.HasValue)
            query = query.Where(u => u.IsVerified == filter.IsVerified.Value);
        
        if (filter.IsBlocked.HasValue)
            query = query.Where(u => u.IsBlocked == filter.IsBlocked.Value);

        if (!string.IsNullOrEmpty(filter.Country))
            query = query.Where(u => u.Country == filter.Country);
        
        if (filter.CreatedFrom.HasValue)
            query = query.Where(u => u.CreatedAt >= filter.CreatedFrom.Value);
        
        if (filter.CreatedTo.HasValue)
            query = query.Where(u => u.CreatedAt <= filter.CreatedTo.Value);
        
        if (filter.HasActiveSubscription.HasValue)
        {
            if (filter.HasActiveSubscription.Value)
                query = query.Where(u => u.Subscriptions.Any(s => s.IsActive));
            else
                query = query.Where(u => !u.Subscriptions.Any(s => s.IsActive));
        }

        if (filter.TariffId.HasValue)
            query = query.Where(u => u.Subscriptions.Any(s => s.IsActive && s.TariffId == filter.TariffId));
        
        return await query.AsNoTracking().ToListAsync();
    }
}