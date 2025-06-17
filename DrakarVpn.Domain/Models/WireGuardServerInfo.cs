namespace DrakarVpn.Domain.Models;

public class WireGuardServerInfo
{
    public string PublicKey { get; set; } = null!;
    public string Endpoint { get; set; } = null!;
}

