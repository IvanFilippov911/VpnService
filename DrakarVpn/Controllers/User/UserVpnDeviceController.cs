using DrakarVpn.Core.AbstractsServices.UserVpnDevice;
using DrakarVpn.Domain.ModelDto.UserVpnDevices;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.User;

[Authorize]
[Route("api/user/devices")]
public class UserVpnDeviceController : WrapperController
{
    private readonly IUserVpnDeviceService deviceService;

    public UserVpnDeviceController(IUserVpnDeviceService deviceService)
    {
        this.deviceService = deviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var userId = GetCurrentUserId();
        var result = await deviceService.GetDevicesForUserAsync(userId);
        return Ok(CreateSuccessResponse(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] UserVpnDeviceRequestDto dto)
    {
        var userId = GetCurrentUserId();
        var result = await deviceService.CreateDeviceAsync(userId, dto);
        return Ok(CreateSuccessResponse(result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(Guid id)
    {
        var success = await deviceService.DeleteDeviceAsync(id);
        if (!success)
            return NotFound(CreateErrorResponse<object>(AppErrors.InvalidId));

        return Ok(CreateSuccessResponse("Deleted"));
    }
}

