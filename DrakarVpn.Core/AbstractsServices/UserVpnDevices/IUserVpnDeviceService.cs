using DrakarVpn.Domain.ModelDto.UserVpnDevices;

namespace DrakarVpn.Core.AbstractsServices.UserVpnDevice;

public interface IUserVpnDeviceService
{
    Task<List<UserVpnDeviceRequestDto>> GetDevicesForUserAsync(string userId);
    Task<UserVpnDeviceResponseDto> CreateDeviceAsync(string userId, UserVpnDeviceRequestDto dto);
    Task<string?> DeleteDeviceAsync(Guid deviceId);
}

