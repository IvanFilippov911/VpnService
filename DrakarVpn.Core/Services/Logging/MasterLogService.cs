using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Logging;
using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Logging;
using DrakarVpn.Domain.Models.Pagination;

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

    public async Task LogUserActionAsync(string userId, UserActionType actionType, string metadata = "")
    {
        var entry = new UserActionLogEntry
        {
            UserId = userId,
            ActionType = actionType.ToString(),
            Metadata = metadata
        };

        await mongoRepo.AddUserLogAsync(entry);
    }


    public async Task<PagedResult<UserLogDto>> GetUserLogsPagedAsync(string userId, int offset, int limit)
    {
        var (logs, totalCount) = await mongoRepo.GetUserLogsPagedAsync(userId, offset, limit);

        return new PagedResult<UserLogDto>
        {
            Items = mapper.Map<List<UserLogDto>>(logs),
            TotalCount = totalCount,
        };
    }



    public async Task LogSystemEventAsync(SystemLogSource source, 
        SystemErrorCode errorCode, 
        string message, 
        string? stackTrace = null)
    {
        var entry = new SystemLogEntry
        {
            Source = source.ToString(),
            ErrorCode = errorCode.ToString(),
            Message = message,
            StackTrace = stackTrace
        };

        await mongoRepo.AddSystemLogAsync(entry);
    }

    public async Task<PagedResult<SystemLogDto>> GetSystemLogsAsync(SystemLogFilterDto filter)
    {
        var (logs, totalCount) = await mongoRepo.GetSystemLogsPagedAsync(filter);

        return new PagedResult<SystemLogDto>
        {
            Items = mapper.Map<List<SystemLogDto>>(logs),
            TotalCount = totalCount,
        };
    }

}