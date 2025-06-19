namespace DrakarVpn.Domain.ModelDto.Subscriptions;

public class SubscriptionMyDto
{
    public string CurrentTariffName { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsAutoRenew { get; set; }
    public bool IsActive { get; set; }
    public int MaxDevices { get; set; }
}