using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Core.AbstractsServices.Users;

public interface IUserService
{
    Task<PagedResult<UserListItemDto>> GetAllUsersPagedAsync(int offset, int limit);
    Task<UserDetailsDto?> GetUserByIdAsync(string userId);
    Task<PagedResult<UserListItemDto>> FilterUsersAsync(UserFilterDto filter);
    Task<UserProfileDto?> GetUserProfileAsync(string userId);
    Task<bool> UpdateUserProfileAsync(string userId, UserProfileDto dto);
}