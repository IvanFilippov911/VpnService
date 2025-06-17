using DrakarVpn.Core.AbstractsServices.Subscriptions;
using DrakarVpn.Domain.ModelDto.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DrakarVpn.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class SubscriptionController : WrapperController
{
    private readonly ISubscriptionService subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        this.subscriptionService = subscriptionService;
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
        return Ok(CreateSuccessResponse("Subscription purchased successfully"));
    }

    [HttpPost("deactivate")]
    public async Task<IActionResult> DeactivateMySubscription()
    {
        var userId = GetCurrentUserId();
        await subscriptionService.DeactivateMySubscriptionAsync(userId);
        return Ok(CreateSuccessResponse("Subscription deactivated successfully"));
    }

    [Authorize]
    [HttpPut("autorenew")]
    public async Task<IActionResult> UpdateAutoRenewSetting([FromBody] UpdateAutoRenewDto dto)
    {
        var userId = GetCurrentUserId();
        await subscriptionService.UpdateAutoRenewAsync(userId, dto.Enable);

        return Ok(CreateSuccessResponse("Autorenewal setting updated"));
    }

}
