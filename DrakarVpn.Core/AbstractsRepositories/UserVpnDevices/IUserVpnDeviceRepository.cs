using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Core.AbstractsRepositories.VpnDevice;

public interface IUserVpnDeviceRepository
{
    Task<List<UserVpnDevice>> GetAllByUserIdAsync(string userId);
    Task<UserVpnDevice?> GetByIdAsync(Guid id);
    Task AddAsync(UserVpnDevice device);
    Task DeleteAsync(Guid id);
    Task<int> CountByUserIdAsync(string userId);
    Task<UserVpnDevice?> FindByPublicKeyAsync(string publicKey);

}
