namespace DrakarVpn.Domain.ModelDto.Peers;

public class AddPeerRequestDto
{
    public Guid UserId { get; set; }
    public string PublicKey { get; set; } = null!;
    public string PrivateKey { get; set; } = null!;
}

