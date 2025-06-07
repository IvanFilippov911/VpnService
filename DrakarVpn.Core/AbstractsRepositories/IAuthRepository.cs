using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Core.AbstractsRepositories;

public interface IAuthRepository
{
    Task<AppUser?> GetUserByEmailAsync(string email);
}
