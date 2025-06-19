using DrakarVpn.Domain.ModelDto.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.Admin;

[ApiController]
[Route("api/admin/logs")]
[Authorize(Roles = "Admin")]
public class AdminLogsController : WrapperController
{
    private readonly IMasterLogService logService;

    public AdminLogsController(IMasterLogService logService)
    {
        this.logService = logService;
    }

    [HttpGet("system")]
    public async Task<IActionResult> GetSystemLogs([FromQuery] SystemLogFilterDto filter)
    {
        var logs = await logService.GetSystemLogsAsync(filter);
        return Ok(CreateSuccessResponse(logs));
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserLogs([FromQuery] string userId)
    {
        var logs = await logService.GetUserLogsAsync(userId);
        return Ok(CreateSuccessResponse(logs));
    }
}
