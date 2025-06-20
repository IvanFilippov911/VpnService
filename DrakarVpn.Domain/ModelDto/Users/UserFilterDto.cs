using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Domain.ModelDto.Users;

public class UserFilterDto : IPaginatable
{
    public string? Email { get; set; }
    public bool? IsVerified { get; set; }
    public bool? IsBlocked { get; set; }
    public string? Country { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public bool? HasActiveSubscription { get; set; }
    public Guid? TariffId { get; set; }

    public int Offset { get; set; } = 0;  
    public int Limit { get; set; } = 10;  
}