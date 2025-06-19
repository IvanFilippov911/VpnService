

using DrakarVpn.Domain.Models;

namespace DrakarVpn.Domain.ModelDto.UserVpnDevices;

public class UserVpnDeviceResultDto
{
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AssignedIp { get; set; }
    public WireGuardServerInfo ServerConfig { get; set; } = null!;
}
