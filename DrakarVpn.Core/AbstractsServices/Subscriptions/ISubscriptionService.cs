using DrakarVpn.Domain.ModelDto.Subscriptions;

namespace DrakarVpn.Core.AbstractsServices.Subscriptions;

public interface ISubscriptionService
{
    Task<SubscriptionMyDto?> GetMySubscriptionAsync(string userId);
    Task PurchaseSubscriptionAsync(string userId, SubscriptionPurchaseDto dto);
    Task DeactivateMySubscriptionAsync(string userId);
    Task UpdateAutoRenewAsync(string userId, bool enable);
}