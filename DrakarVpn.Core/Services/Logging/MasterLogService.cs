using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Logging;
using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelDto.Logging;

namespace DrakarVpn.Core.Services.Logging;

public class MasterLogService : IMasterLogService
{
    private readonly IMongoLogRepository mongoRepo;
    private readonly IMapper mapper;

    public MasterLogService(IMongoLogRepository mongoRepo, IMapper mapper)
    {
        this.mongoRepo = mongoRepo;
        this.mapper = mapper;
    }

    public async Task LogUserActionAsync(string userId, string actionType, string metadata = "")
    {
        var entry = new UserActionLogEntry
        {
            UserId = userId,
            ActionType = actionType,
            Metadata = metadata
        };

        await mongoRepo.AddUserLogAsync(entry);
    }

    public async Task<List<UserLogDto>> GetUserLogsAsync(string userId)
    {
        var logs = await mongoRepo.GetUserLogsAsync(userId);
        return mapper.Map<List<UserLogDto>>(logs);
    }

    public async Task LogSystemEventAsync(string source, string errorCode, string message, string? stackTrace = null)
    {
        var entry = new SystemLogEntry
        {
            Source = source,
            ErrorCode = errorCode,
            Message = message,
            StackTrace = stackTrace
        };

        await mongoRepo.AddSystemLogAsync(entry);
    }

    public async Task<List<SystemLogDto>> GetSystemLogsAsync(SystemLogFilterDto filter)
    {
        var logs = await mongoRepo.GetSystemLogsAsync(filter);
        return mapper.Map<List<SystemLogDto>>(logs);
    }
}