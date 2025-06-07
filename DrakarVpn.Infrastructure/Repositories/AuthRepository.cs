using DrakarVpn.Domain.Entities;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DrakarVpn.Core.AbstractsRepositories.Auth;

namespace DrakarVpn.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public AuthRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}
