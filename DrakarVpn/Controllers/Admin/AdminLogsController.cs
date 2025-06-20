using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Logging;
using DrakarVpn.Domain.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.Admin;

[ApiController]
[Route("api/admin/logs")]
//[Authorize(Roles = "Admin")]
[SetLogSource(SystemLogSource.Logging)]
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


    [HttpGet("my-actions")]
    public async Task<IActionResult> GetMyActionHistory([FromQuery] PaginationQueryDto pagination)
    {
        var userId = GetCurrentUserId();
        var result = await logService.GetUserLogsPagedAsync(userId, pagination.Offset, pagination.Limit);
        return Ok(CreateSuccessResponse(result));
    }

}
