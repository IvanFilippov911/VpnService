
namespace DrakarVpn.Domain.ModelDto.Users;

public class UserDetailsDto
{
    public string UserId { get; set; }
    public string? FullName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastActionDate { get; set; }

    public int DownloadsCount { get; set; }
    public int SessionsCount { get; set; }
    public long TrafficVolumeMb { get; set; }
    public int LimitExceededCount { get; set; }

    public string? LastIpAddress { get; set; }
    public string? UserAgent { get; set; }

    public string VerificationStatus { get; set; }
    public string? CurrentTariffName { get; set; }
    public string? Country { get; set; }
    public DateTime? SubscriptionExpiresAt { get; set; }
    public decimal LifetimeValue { get; set; }
    public decimal LastPaymentAmount { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    public string? AdminNote { get; set; }
}

