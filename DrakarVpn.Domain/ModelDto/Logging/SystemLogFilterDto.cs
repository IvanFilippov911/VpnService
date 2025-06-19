namespace DrakarVpn.Domain.ModelDto.Logging;

public class SystemLogFilterDto
{
    public string? Source { get; set; }
    public string? ErrorCode { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
