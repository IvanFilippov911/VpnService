using DrakarVpn.Core.AbstractsRepositories.Subscriptions;
using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Core.AbstractsServices.Subscriptions;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Subscriptions;

namespace DrakarVpn.Core.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ITariffRepository tariffRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, ITariffRepository tariffRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.tariffRepository = tariffRepository;
        }

        public async Task<SubscriptionMyDto?> GetMySubscriptionAsync(string userId)
        {
            var subscription = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId);
            if (subscription == null) return null;

            return new SubscriptionMyDto
            {
                CurrentTariffName = subscription.Tariff.Name,
                ExpiresAt = subscription.ExpiresAt,
                IsAutoRenew = subscription.IsAutoRenew,
                IsActive = subscription.IsActive
            };
        }

        public async Task PurchaseSubscriptionAsync(string userId, SubscriptionPurchaseDto dto)
        {
            var tariff = await tariffRepository.GetTariffByIdAsync(dto.TariffId);
            if (tariff == null)
            {
                throw new Exception("Invalid Tariff"); 
            }

            var existingSubscription = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId);
            if (existingSubscription != null)
            {
                
                existingSubscription.ExpiresAt = existingSubscription.ExpiresAt > DateTime.UtcNow
                    ? existingSubscription.ExpiresAt.AddDays(tariff.DurationInDays)
                    : DateTime.UtcNow.AddDays(tariff.DurationInDays);

                existingSubscription.IsAutoRenew = dto.EnableAutoRenew;
                existingSubscription.IsActive = true;

                await subscriptionRepository.UpdateSubscriptionAsync(existingSubscription);
            }
            else
            {
                
                var newSubscription = new Subscription
                {
                    UserId = userId,
                    TariffId = tariff.Id,
                    PurchasedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(tariff.DurationInDays),
                    IsAutoRenew = dto.EnableAutoRenew,
                    IsActive = true
                };

                await subscriptionRepository.AddSubscriptionAsync(newSubscription);
            }
        }

        public async Task DeactivateMySubscriptionAsync(string userId)
        {
            var existingSubscription = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId);
            if (existingSubscription != null)
            {
                await subscriptionRepository.DeactivateSubscriptionAsync(existingSubscription.Id);
            }
        }
    }
}
