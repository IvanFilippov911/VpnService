

namespace DrakarVpn.Domain.ModelDto.Users;

public class UserListItemDto
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string? CurrentTariffName { get; set; } 
    public string? Country { get; set; }           
    public DateTime? SubscriptionExpiresAt { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastActionDate { get; set; }
    public string AccountStatus { get; set; }     
}
