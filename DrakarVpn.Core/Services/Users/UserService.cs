using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Domain.ModelDto.Users;

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

    public async Task<List<UserListItemDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        return mapper.Map<List<UserListItemDto>>(users);
    }

    public async Task<UserDetailsDto?> GetUserByIdAsync(string userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return null;

        var userDto = mapper.Map<UserDetailsDto>(user);
        return userDto;
    }

    public async Task<List<UserListItemDto>> FilterUsersAsync(UserFilterDto filter)
    {
        var users = await userRepository.FilterUsersAsync(filter);
        return mapper.Map<List<UserListItemDto>>(users);
    }

}
