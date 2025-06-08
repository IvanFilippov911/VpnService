using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers;

[Route("api/[controller]")]
public class UsersController : WrapperController
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
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
            return StatusCode((int)AppErrors.InvalidId.StatusCode, CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse(user));
    }

    [HttpGet("filter")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> FilterUsers([FromQuery] UserFilterDto filter)
    {
        var users = await userService.FilterUsersAsync(filter);
        return Ok(CreateSuccessResponse(users));
    }

}
