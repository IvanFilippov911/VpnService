using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Users;

namespace DrakarVpn.Core.AbstractsRepositories.Users;

public interface IUserRepository
{
    Task<List<AppUser>> GetAllUsersAsync();
    Task<AppUser?> GetUserByIdAsync(string userId);
    Task<List<AppUser>> FilterUsersAsync(UserFilterDto filter);
    Task<bool> UpdateUserProfileAsync(string userId, UserProfileDto dto);


}