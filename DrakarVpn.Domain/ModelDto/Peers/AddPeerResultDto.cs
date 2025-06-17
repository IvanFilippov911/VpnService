namespace DrakarVpn.Domain.ModelDto.Peers;

public class AddPeerResultDto
{
    public PeerResponseDto Peer { get; set; } = null!;
    public string ClientConfig { get; set; } = null!;
}


