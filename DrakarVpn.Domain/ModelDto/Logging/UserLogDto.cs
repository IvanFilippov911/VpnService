namespace DrakarVpn.Domain.ModelDto.Logging;

public class UserLogDto
{
    public DateTime PerformedAt { get; set; }
    public string ActionType { get; set; }
    public string Metadata { get; set; }
}
