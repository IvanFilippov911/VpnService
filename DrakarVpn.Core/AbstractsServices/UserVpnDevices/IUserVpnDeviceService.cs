using DrakarVpn.Domain.ModelDto.UserVpnDevices;

namespace DrakarVpn.Core.AbstractsServices.UserVpnDevice;

public interface IUserVpnDeviceService
{
    Task<UserVpnDeviceListWithLimitDto> GetDevicesWithConfigAsync(string userId);
    Task CreateDeviceAsync(string userId, UserVpnDeviceCreateDto dto);
    Task<string?> DeleteDeviceAsync(Guid deviceId);
}

