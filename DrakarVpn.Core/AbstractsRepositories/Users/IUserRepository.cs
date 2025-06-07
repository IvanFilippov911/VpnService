using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Core.AbstractsRepositories.Users;

public interface IUserRepository
{
    Task<List<AppUser>> GetAllUsersAsync();
    Task<AppUser?> GetUserByIdAsync(string userId);
}