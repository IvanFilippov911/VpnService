

using DrakarVpn.Domain.Models;

namespace DrakarVpn.Domain.ModelDto.UserVpnDevices;

public class UserVpnDeviceResponseDto
{
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string AssignedIp { get; set; } = null!;
    public WireGuardServerInfo ServerConfig { get; set; } = null!;
}
