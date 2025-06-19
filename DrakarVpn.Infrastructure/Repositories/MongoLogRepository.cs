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

    public async Task<List<UserActionLogEntry>> GetUserLogsAsync(string userId)
    {
        var filter = Builders<UserActionLogEntry>.Filter.Eq(x => x.UserId, userId);
        return await db.UserLogs.Find(filter)
            .SortByDescending(x => x.PerformedAt)
            .Limit(100)
            .ToListAsync();
    }

    public async Task AddSystemLogAsync(SystemLogEntry entry)
    {
        await db.SystemLogs.InsertOneAsync(entry);
    }

    public async Task<List<SystemLogEntry>> GetSystemLogsAsync(string? source = null, string? errorCode = null)
    {
        var filter = Builders<SystemLogEntry>.Filter.Empty;

        if (!string.IsNullOrEmpty(source))
            filter &= Builders<SystemLogEntry>.Filter.Eq(x => x.Source, source);

        if (!string.IsNullOrEmpty(errorCode))
            filter &= Builders<SystemLogEntry>.Filter.Eq(x => x.ErrorCode, errorCode);

        return await db.SystemLogs.Find(filter)
            .SortByDescending(x => x.Timestamp)
            .Limit(100)
            .ToListAsync();
    }

    public async Task<List<SystemLogEntry>> GetSystemLogsAsync(SystemLogFilterDto filter)
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

        return await db.SystemLogs.Find(mongoFilter)
            .SortByDescending(x => x.Timestamp)
            .Limit(100)
            .ToListAsync();
    }

}
