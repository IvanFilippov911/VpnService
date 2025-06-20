using DrakarVpn.Core.AbstractsRepositories.Logging;
using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelDto.Logging;
using DrakarVpn.Infrastructure.Persistence;
using MongoDB.Driver;

namespace DrakarVpn.Infrastructure.Repositories;

public class MongoLogRepository : IMongoLogRepository
{
    private readonly MongoLogDbContext db;

    public MongoLogRepository(MongoLogDbContext db)
    {
        this.db = db;
    }

    public async Task AddUserLogAsync(UserActionLogEntry entry)
    {
        await db.UserLogs.InsertOneAsync(entry);
    }

    public async Task<(List<UserActionLogEntry> Logs, int TotalCount)> GetUserLogsPagedAsync(string userId, int offset, int limit)
    {
        var filter = Builders<UserActionLogEntry>.Filter.Eq(x => x.UserId, userId);

        var totalCount = (int)await db.UserLogs.CountDocumentsAsync(filter);

        var logs = await db.UserLogs.Find(filter)
            .SortByDescending(x => x.PerformedAt)
            .Skip(offset)
            .Limit(limit)
            .ToListAsync();

        return (logs, totalCount);
    }


    public async Task AddSystemLogAsync(SystemLogEntry entry)
    {
        await db.SystemLogs.InsertOneAsync(entry);
    }


    public async Task<(List<SystemLogEntry> Logs, int TotalCount)> GetSystemLogsPagedAsync(SystemLogFilterDto filter)
    {
        var builder = Builders<SystemLogEntry>.Filter;
        var mongoFilter = builder.Empty;

        if (!string.IsNullOrEmpty(filter.Source))
            mongoFilter &= builder.Eq(x => x.Source, filter.Source);

        if (!string.IsNullOrEmpty(filter.ErrorCode))
            mongoFilter &= builder.Eq(x => x.ErrorCode, filter.ErrorCode);

        if (filter.From.HasValue)
            mongoFilter &= builder.Gte(x => x.Timestamp, filter.From.Value);

        if (filter.To.HasValue)
            mongoFilter &= builder.Lte(x => x.Timestamp, filter.To.Value);

        var totalCount = (int)await db.SystemLogs.CountDocumentsAsync(mongoFilter);

        var logs = await db.SystemLogs.Find(mongoFilter)
            .SortByDescending(x => x.Timestamp)
            .Skip(filter.Offset)
            .Limit(filter.Limit)
            .ToListAsync();

        return (logs, totalCount);
    }


}
