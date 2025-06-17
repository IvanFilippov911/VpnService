namespace DrakarVpn.Domain.ModelDto.Peers;

public class PeerFilterDto
{
    public bool? IsActive { get; set; }
    public Guid? UserId { get; set; }
    public string? AssignedIp { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }

    
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

