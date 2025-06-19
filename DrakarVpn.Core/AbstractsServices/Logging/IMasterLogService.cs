using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Logging;


public interface IMasterLogService
{
    Task LogUserActionAsync(string userId, UserActionType actionType, string metadata = "");
    Task<List<UserLogDto>> GetUserLogsAsync(string userId);
    Task LogSystemEventAsync(SystemLogSource source, SystemErrorCode errorCode, string message, string? stackTrace = null);
    Task<List<SystemLogDto>> GetSystemLogsAsync(SystemLogFilterDto filter);

}
