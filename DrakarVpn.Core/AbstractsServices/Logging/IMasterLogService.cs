using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Logging;
using DrakarVpn.Domain.Models.Pagination;

public interface IMasterLogService
{
    Task LogUserActionAsync(string userId, UserActionType actionType, string metadata = "");
    Task<PagedResult<UserLogDto>> GetUserLogsPagedAsync(string userId, int offset, int limit);
    Task LogSystemEventAsync(SystemLogSource source, SystemErrorCode errorCode, string message, string? stackTrace = null);
    Task<PagedResult<SystemLogDto>> GetSystemLogsAsync(SystemLogFilterDto filter);


}
