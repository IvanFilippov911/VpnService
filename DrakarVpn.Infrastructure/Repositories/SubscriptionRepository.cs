using DrakarVpn.Core.AbstractsRepositories.Subscriptions;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DrakarVpn.Infrastructure.Repositories.Subscriptions;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public SubscriptionRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Subscription?> GetActiveSubscriptionByUserIdAsync(string userId)
    {
        return await dbContext.Subscriptions
            .Include(s => s.Tariff)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive);
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await dbContext.Subscriptions.AddAsync(subscription);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateSubscriptionAsync(Subscription subscription)
    {
        await dbContext.Subscriptions
            .Where(s => s.Id == subscription.Id)
            .ExecuteUpdateAsync(i => i
                .SetProperty(s => s.ExpiresAt, s => subscription.ExpiresAt)
                .SetProperty(s => s.IsAutoRenew, s => subscription.IsAutoRenew)
                .SetProperty(s => s.IsActive, s => subscription.IsActive)
            );
    }

    public async Task DeactivateSubscriptionAsync(Guid subscriptionId)
    {
        await dbContext.Subscriptions
            .Where(s => s.Id == subscriptionId)
            .ExecuteUpdateAsync(i => i
                .SetProperty(s => s.IsActive, s => false)
            );
    }

    public async Task SetAutoRenewStatusAsync(Guid subscriptionId, bool enable)
    {
        await dbContext.Subscriptions
            .Where(s => s.Id == subscriptionId)
            .ExecuteUpdateAsync(upd => upd
                .SetProperty(s => s.IsAutoRenew, enable)
            );
    }

}
