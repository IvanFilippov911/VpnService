

namespace DrakarVpn.Domain.ModelDto.Peers;

public class PeerDto
{
    public Guid UserId { get; set; }
    public string PublicKey { get; set; } = null!;
    public string AssignedIp { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

