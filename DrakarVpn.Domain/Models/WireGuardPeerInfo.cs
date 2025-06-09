namespace DrakarVpn.Domain.Models;

public class WireGuardPeerInfo
{
    public string PublicKey { get; set; } = default!;
    public string AllowedIp { get; set; } = default!;
}
