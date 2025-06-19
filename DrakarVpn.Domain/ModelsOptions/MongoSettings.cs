namespace DrakarVpn.Domain.ModelsOptions;

public class MongoSettings
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public CollectionNames Collections { get; set; } = new();

    public class CollectionNames
    {
        public string UserLogs { get; set; } = "vpn_user_logs";
        public string SystemLogs { get; set; } = "vpn_system_logs";
    }
}
