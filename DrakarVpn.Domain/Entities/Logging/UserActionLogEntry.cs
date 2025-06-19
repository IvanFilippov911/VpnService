using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrakarVpn.Domain.Entities.Logging;

public class UserActionLogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string UserId { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public string ActionType { get; set; } = string.Empty;

    public string Metadata { get; set; } = string.Empty;
}
