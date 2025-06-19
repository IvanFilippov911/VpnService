using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DrakarVpn.Domain.Entities.Logging;

public class SystemLogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string Source { get; set; } = string.Empty;

    public string ErrorCode { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? StackTrace { get; set; } = null;
}
