using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelDto.Logging;

namespace DrakarVpn.Core.AbstractsRepositories.Logging;

public interface IMongoLogRepository
{
    Task AddUserLogAsync(UserActionLogEntry entry);
    Task<(List<UserActionLogEntry> Logs, int TotalCount)> GetUserLogsPagedAsync(string userId, int offset, int limit);
    Task AddSystemLogAsync(SystemLogEntry entry);
    Task<(List<SystemLogEntry> Logs, int TotalCount)> GetSystemLogsPagedAsync(SystemLogFilterDto filter);

}
