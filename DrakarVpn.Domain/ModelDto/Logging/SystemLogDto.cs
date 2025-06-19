namespace DrakarVpn.Domain.ModelDto.Logging;

public class SystemLogDto
{
    public DateTime Timestamp { get; set; }
    public string Source { get; set; }
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public string? StackTrace { get; set; }
}
