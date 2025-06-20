using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Domain.ModelDto.Peers;

public class PeerFilterDto : IPaginatable
{
    public bool? IsActive { get; set; }
    public string? UserId { get; set; }
    public string? AssignedIp { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }

    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 50;
}

