using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Subscriptions;
using DrakarVpn.Core.AbstractsRepositories.VpnDevice;
using DrakarVpn.Core.AbstractsServices.Peers;
using DrakarVpn.Core.AbstractsServices.UserVpnDevice;
using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.UserVpnDevices;

namespace DrakarVpn.Core.Services.UserVpnDevices;

public class UserVpnDeviceService : IUserVpnDeviceService
{
    private readonly IUserVpnDeviceRepository deviceRepo;
    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IPeerService peerService;
    private readonly IMapper mapper;
    private readonly IWireGuardClientConfigGenerator configGenerator;

    public UserVpnDeviceService(
        IUserVpnDeviceRepository deviceRepo,
        ISubscriptionRepository subscriptionRepository,
        IPeerService peerService,
        IMapper mapper,
        IWireGuardClientConfigGenerator configGenerator)
    {
        this.deviceRepo = deviceRepo;
        this.subscriptionRepository = subscriptionRepository;
        this.peerService = peerService;
        this.mapper = mapper;
        this.configGenerator = configGenerator;
    }

    public async Task<List<UserVpnDeviceRequestDto>> GetDevicesForUserAsync(string userId)
    {
        var devices = await deviceRepo.GetAllByUserIdAsync(userId);
        return mapper.Map<List<UserVpnDeviceRequestDto>>(devices);
    }

    public async Task<UserVpnDeviceResponseDto> CreateDeviceAsync(string userId, UserVpnDeviceRequestDto dto)
    {
        var sub = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId)
            ?? throw new Exception("No active subscription");

        var maxDevices = sub.Tariff.MaxDevices;
        var currentCount = await deviceRepo.CountByUserIdAsync(userId);
        if (currentCount >= maxDevices)
            throw new Exception("Device limit reached");

        var peerResult = await peerService.AddPeerAsync(userId, dto.PublicKey);

        var device = new UserVpnDevice
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PeerId = peerResult.PeerId,
            DeviceName = dto.DeviceName,
            CreatedAt = DateTime.UtcNow
        };

        await deviceRepo.AddAsync(device);

        return new UserVpnDeviceResponseDto
        {
            DeviceId = device.Id,
            DeviceName = device.DeviceName,
            CreatedAt = device.CreatedAt,
            AssignedIp = peerResult.AssignedIp,
            ServerConfig = configGenerator.GetConfig()
        };
    }


    public async Task<bool> DeleteDeviceAsync(Guid deviceId)
    {
        var device = await deviceRepo.GetByIdAsync(deviceId);
        if (device == null) return false;

        await deviceRepo.DeleteAsync(deviceId);
        await peerService.RemovePeerByPeerIdAsync(device.PeerId);
        return true;
    }
}

