using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Domain.ModelDto.Logging;

public class SystemLogFilterDto : IPaginatable
{
    public string? Source { get; set; }
    public string? ErrorCode { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 100;
}
