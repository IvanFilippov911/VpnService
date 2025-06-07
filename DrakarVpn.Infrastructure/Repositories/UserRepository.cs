using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Domain.Entities;
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
}