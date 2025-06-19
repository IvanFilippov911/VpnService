

namespace DrakarVpn.Domain.ModelDto.UserVpnDevices;

public class UserVpnDeviceListWithLimitDto
{
    public List<UserVpnDeviceResultDto> Devices { get; set; } = new();
    public int MaxDevices { get; set; }
}

