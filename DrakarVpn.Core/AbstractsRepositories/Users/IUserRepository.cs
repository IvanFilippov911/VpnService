using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Users;

namespace DrakarVpn.Core.AbstractsRepositories.Users;

public interface IUserRepository
{
    Task<(List<AppUser> Users, int TotalCount)> GetAllUsersPagedAsync(int offset, int limit);
    Task<AppUser?> GetUserByIdAsync(string userId);
    Task<(List<AppUser> Users, int TotalCount)> FilterUsersPagedAsync(UserFilterDto filter);
    Task<bool> UpdateUserProfileAsync(string userId, UserProfileDto dto);


}