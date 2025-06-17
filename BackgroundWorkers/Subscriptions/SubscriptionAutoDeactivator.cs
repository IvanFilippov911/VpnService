using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace DrakarVpn.Core.Services.Subscriptions;

public class SubscriptionAutoDeactivator : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<SubscriptionAutoDeactivator> logger;

    public SubscriptionAutoDeactivator(IServiceProvider serviceProvider,
        ILogger<SubscriptionAutoDeactivator> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DrakarVpnDbContext>();

                var count = await dbContext.Subscriptions
                    .Where(s => s.IsActive && s.ExpiresAt < DateTime.UtcNow)
                    .ExecuteUpdateAsync(upd => upd
                        .SetProperty(s => s.IsActive, false)
                    );

                logger.LogInformation("Auto-deactivated {Count} expired subscriptions", count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SubscriptionAutoDeactivator");
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}