using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers;

[Route("api/[controller]")]
public class UserController : WrapperController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(CreateSuccessResponse(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode
                , CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse(user));
    }

    [HttpGet("filter")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FilterUsers([FromQuery] UserFilterDto filter)
    {
        var users = await userService.FilterUsersAsync(filter);
        return Ok(CreateSuccessResponse(users));
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


}
