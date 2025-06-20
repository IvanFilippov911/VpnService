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

    public async Task<(List<AppUser> Users, int TotalCount)> GetAllUsersPagedAsync(int offset, int limit)
    {
        var query = dbContext.Users
            .AsNoTracking()
            .OrderByDescending(u => u.CreatedAt);

        var totalCount = await query.CountAsync();
        var users = await query.Skip(offset).Take(limit).ToListAsync();

        return (users, totalCount);
    }

    public async Task<AppUser?> GetUserByIdAsync(string userId)
    {
        return await dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<(List<AppUser> Users, int TotalCount)> FilterUsersPagedAsync(UserFilterDto filter)
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

        var totalCount = await query.CountAsync();

        var users = await query.AsNoTracking()
            .OrderByDescending(u => u.CreatedAt)
            .Skip(filter.Offset)
            .Take(filter.Limit)
            .ToListAsync();

        return (users, totalCount);
    }

    public async Task<bool> UpdateUserProfileAsync(string userId, UserProfileDto dto)
    {
        var result = await dbContext.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.FullName, dto.FullName)
                .SetProperty(u => u.Country, dto.Country)
                .SetProperty(u => u.PhoneNumber, dto.PhoneNumber)
                .SetProperty(u => u.Language, dto.Language)
            );

        return result > 0; 
    }


}