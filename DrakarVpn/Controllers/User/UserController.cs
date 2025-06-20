using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Domain.Models.Pagination;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.User;

[Route("api/user/[controller]")]
[SetLogSource(SystemLogSource.User)]
public class UserController : WrapperController
{
    private readonly IUserService userService;
    private readonly IMasterLogService logService;

    public UserController(IUserService userService, IMasterLogService logService)
    {
        this.userService = userService;
        this.logService = logService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetCurrentUserId();
        var profile = await userService.GetUserProfileAsync(userId);
        if (profile == null)
            return StatusCode((int)AppErrors.InvalidId.StatusCode
                , CreateErrorResponse<object>(AppErrors.InvalidId));

        return Ok(CreateSuccessResponse(profile));
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UserProfileDto dto)
    {
        var userId = GetCurrentUserId();
        var updated = await userService.UpdateUserProfileAsync(userId, dto);

        if (!updated)
            return StatusCode((int)AppErrors.InvalidId.StatusCode
                , CreateErrorResponse<object>(AppErrors.InvalidId));

        return Ok(CreateSuccessResponse("Profile updated successfully"));
    }

    [HttpGet("my-actions")]
    public async Task<IActionResult> GetMyActionHistory([FromQuery] PaginationQueryDto pagination)
    {
        var userId = GetCurrentUserId();
        var result = await logService.GetUserLogsPagedAsync(userId, pagination.Offset, pagination.Limit);
        return Ok(CreateSuccessResponse(result));
    }




}
