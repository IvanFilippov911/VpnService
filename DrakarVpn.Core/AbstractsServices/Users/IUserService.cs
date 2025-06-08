using DrakarVpn.Domain.ModelDto.Users;

namespace DrakarVpn.Core.AbstractsServices.Users;

public interface IUserService
{
    Task<List<UserListItemDto>> GetAllUsersAsync();
    Task<UserDetailsDto?> GetUserByIdAsync(string userId);
    Task<List<UserListItemDto>> FilterUsersAsync(UserFilterDto filter);
}