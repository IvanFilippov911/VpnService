namespace DrakarVpn.Domain.ModelDto.Peers;

public class PeerAllocationResult
{
    public Guid PeerId { get; set; }
    public string AssignedIp { get; set; } = null!;
}
