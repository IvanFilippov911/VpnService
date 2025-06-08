using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Core.AbstractsRepositories.Subscriptions;


public interface ISubscriptionRepository
{
    Task<Subscription?> GetActiveSubscriptionByUserIdAsync(string userId);
    Task AddSubscriptionAsync(Subscription subscription);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeactivateSubscriptionAsync(Guid subscriptionId);

}