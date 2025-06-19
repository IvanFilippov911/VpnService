using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelDto.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMasterLogService
{
    Task LogUserActionAsync(string userId, string actionType, string metadata = "");
    Task<List<UserLogDto>> GetUserLogsAsync(string userId);

    Task LogSystemEventAsync(string source, string errorCode, string message, string? stackTrace = null);
    Task<List<SystemLogDto>> GetSystemLogsAsync(SystemLogFilterDto filter);

}
