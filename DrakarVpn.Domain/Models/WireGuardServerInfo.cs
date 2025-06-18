namespace DrakarVpn.Domain.Models;

public class WireGuardServerInfo
{
    public string PublicKey { get; set; } = null!;
    public string Endpoint { get; set; } = null!;
    public string AllowedIPs { get; set; } = "0.0.0.0/0, ::/0";
    public int PersistentKeepalive { get; set; } = 25;
}

