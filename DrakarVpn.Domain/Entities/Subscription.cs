namespace DrakarVpn.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }
    public AppUser User { get; set; }

    public Guid TariffId { get; set; }
    public Tariff Tariff { get; set; }

    public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public bool IsAutoRenew { get; set; } = false;
    public bool IsActive { get; set; } = true;
}
