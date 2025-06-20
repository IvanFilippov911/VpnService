using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<PagedResult<UserListItemDto>> GetAllUsersPagedAsync(int offset, int limit)
    {
        var (users, totalCount) = await userRepository.GetAllUsersPagedAsync(offset, limit);

        return new PagedResult<UserListItemDto>
        {
            Items = mapper.Map<List<UserListItemDto>>(users),
            TotalCount = totalCount,
        };
    }


    public async Task<UserDetailsDto?> GetUserByIdAsync(string userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return null;

        var userDto = mapper.Map<UserDetailsDto>(user);
        return userDto;
    }

    public async Task<PagedResult<UserListItemDto>> FilterUsersAsync(UserFilterDto filter)
    {
        var (users, totalCount) = await userRepository.FilterUsersPagedAsync(filter);

        return new PagedResult<UserListItemDto>
        {
            Items = mapper.Map<List<UserListItemDto>>(users),
            TotalCount = totalCount
        };
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        return user == null ? null : mapper.Map<UserProfileDto>(user);
    }

    public async Task<bool> UpdateUserProfileAsync(string userId, UserProfileDto dto)
    {
        return await userRepository.UpdateUserProfileAsync(userId, dto);
    }

}
