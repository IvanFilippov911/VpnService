namespace DrakarVpn.Domain.ModelDto.Subscriptions;

public class SubscriptionPurchaseDto
{
    public Guid TariffId { get; set; }
    public bool EnableAutoRenew { get; set; }
}
