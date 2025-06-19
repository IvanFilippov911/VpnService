using DrakarVpn.Core.AbstractsServices.Subscriptions;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.User;

[Authorize]
[Route("api/user/[controller]")]
public class SubscriptionController : WrapperController
{
    private readonly ISubscriptionService subscriptionService;
    private readonly IMasterLogService logService;

    public SubscriptionController(
        ISubscriptionService subscriptionService,
        IMasterLogService logService)
    {
        this.subscriptionService = subscriptionService;
        this.logService = logService;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMySubscription()
    {
        var userId = GetCurrentUserId();
        var subscription = await subscriptionService.GetMySubscriptionAsync(userId);
        return Ok(CreateSuccessResponse(subscription));
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseSubscription([FromBody] SubscriptionPurchaseDto dto)
    {
        var userId = GetCurrentUserId();
        await subscriptionService.PurchaseSubscriptionAsync(userId, dto);

        await logService.LogUserActionAsync(
            userId,
            UserActionType.SubscriptionPurchased,
            $"TariffId: {dto.TariffId}"
        );

        return Ok(CreateSuccessResponse("Subscription purchased successfully"));
    }

    [HttpPost("deactivate")]
    public async Task<IActionResult> DeactivateMySubscription()
    {
        var userId = GetCurrentUserId();
        await subscriptionService.DeactivateMySubscriptionAsync(userId);

        await logService.LogUserActionAsync(
            userId,
            UserActionType.SubscriptionDeactivated,
            ""
        );

        return Ok(CreateSuccessResponse("Subscription deactivated successfully"));
    }

    [HttpPut("autorenew")]
    public async Task<IActionResult> UpdateAutoRenewSetting([FromBody] UpdateAutoRenewDto dto)
    {
        var userId = GetCurrentUserId();
        await subscriptionService.UpdateAutoRenewAsync(userId, dto.Enable);

        await logService.LogUserActionAsync(
            userId,
            UserActionType.AutoRenewChanged,
            $"Enabled: {dto.Enable}"
        );

        return Ok(CreateSuccessResponse("Autorenewal setting updated"));
    }
}
