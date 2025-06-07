using DrakarVpn.Core.AbstractsRepositories.Users;
using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Domain.ModelDto.Users;

namespace DrakarVpn.Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<List<UserListItemDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();

        return users.Select(u => new UserListItemDto
        {
            UserId = u.Id,
            Email = u.Email ?? "",
            CurrentTariffName = "", 
            Country = u.Country ?? "",
            SubscriptionExpiresAt = null, 
            RegistrationDate = u.CreatedAt,
            LastActionDate = u.LastLoginAt,
            AccountStatus = u.IsBlocked ? "Suspended" : (u.IsVerified ? "Active" : "Pending")
        }).ToList();
    }

    public async Task<UserDetailsDto?> GetUserByIdAsync(string userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
            return null;

        return new UserDetailsDto
        {
            UserId = user.Id,
            FullName = user.FullName ?? "",
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? "",
            RegistrationDate = user.CreatedAt,
            LastActionDate = user.LastLoginAt,

            DownloadsCount = 0, 
            SessionsCount = 0,
            TrafficVolumeMb = 0,
            LimitExceededCount = 0,

            LastIpAddress = "", 
            UserAgent = "",

            VerificationStatus = user.IsVerified ? "Verified" : "Not Verified",
            CurrentTariffName = "", 
            Country = user.Country ?? "",
            SubscriptionExpiresAt = null, 

            LifetimeValue = 0, 
            LastPaymentAmount = 0,
            LastPaymentDate = null,

            AdminNote = user.AdminNote ?? ""
        };
    }
}
