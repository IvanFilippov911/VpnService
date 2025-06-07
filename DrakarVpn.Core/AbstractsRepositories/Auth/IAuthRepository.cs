using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Core.AbstractsRepositories.Auth;

public interface IAuthRepository
{
    Task<AppUser?> GetUserByEmailAsync(string email);
}
