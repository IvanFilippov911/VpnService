namespace DrakarVpn.Domain.Entities;

public class Peer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string PublicKey { get; set; } = null!;
    public string PrivateKey { get; set; } = null!;
    public string AssignedIP { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

