using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelsOptions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DrakarVpn.Infrastructure.Persistence;

public class MongoLogDbContext
{
    private readonly IMongoDatabase database;
    private readonly MongoSettings settings;

    public MongoLogDbContext(IOptions<MongoSettings> options)
    {
        settings = options.Value;

        var mongoUrl = MongoUrl.Create(settings.ConnectionString);
        var client = new MongoClient(mongoUrl);
        database = client.GetDatabase(settings.Database);
    }

    public IMongoCollection<UserActionLogEntry> UserLogs =>
        database.GetCollection<UserActionLogEntry>(settings.Collections.UserLogs);

    public IMongoCollection<SystemLogEntry> SystemLogs =>
        database.GetCollection<SystemLogEntry>(settings.Collections.SystemLogs);
}
