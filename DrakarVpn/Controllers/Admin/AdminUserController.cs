using DrakarVpn.Core.AbstractsServices.Users;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Users;
using DrakarVpn.Domain.Models.Pagination;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.Admin;

//[Authorize(Roles = "Admin")]
[Route("api/admin/users")]
[ApiController]
[SetLogSource(SystemLogSource.User)]
public class AdminUserController : WrapperController
{
    private readonly IUserService userService;

    public AdminUserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginationQueryDto pagination)
    {
        var users = await userService.GetAllUsersPagedAsync(pagination.Offset, pagination.Limit);
        return Ok(CreateSuccessResponse(users));
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return StatusCode((int)AppErrors.InvalidId.StatusCode,
                CreateErrorResponse<object>(AppErrors.InvalidId));
        }

        return Ok(CreateSuccessResponse(user));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterUsers([FromQuery] UserFilterDto filter)
    {
        var users = await userService.FilterUsersAsync(filter);
        return Ok(CreateSuccessResponse(users));
    }

}
