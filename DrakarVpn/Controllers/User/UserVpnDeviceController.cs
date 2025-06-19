using DrakarVpn.Core.AbstractsServices.UserVpnDevice;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.UserVpnDevices;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.User;

[Authorize]
[Route("api/user/devices")]
[SetLogSource(SystemLogSource.Device)]
public class UserVpnDeviceController : WrapperController
{
    private readonly IUserVpnDeviceService deviceService;
    private readonly IMasterLogService logService;

    public UserVpnDeviceController(
        IUserVpnDeviceService deviceService,
        IMasterLogService logService)
    {
        this.deviceService = deviceService;
        this.logService = logService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var userId = GetCurrentUserId();
        var result = await deviceService.GetDevicesWithConfigAsync(userId);
        return Ok(CreateSuccessResponse(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] UserVpnDeviceCreateDto dto)
    {
        var userId = GetCurrentUserId();
        await deviceService.CreateDeviceAsync(userId, dto);

        await logService.LogUserActionAsync(
            userId,
            UserActionType.DeviceCreated,
            $"DeviceName: {dto.DeviceName}"
        );

        return Ok(CreateSuccessResponse("Device created"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(Guid id)
    {
        var userId = GetCurrentUserId();
        var deviceName = await deviceService.DeleteDeviceAsync(id);

        if (deviceName == null)
            return NotFound(CreateErrorResponse<object>(AppErrors.InvalidId));

        await logService.LogUserActionAsync(
            userId,
            UserActionType.DeviceDeleted,
            $"DeviceName: {deviceName}"
        );

        return Ok(CreateSuccessResponse("Deleted"));
    }
}
