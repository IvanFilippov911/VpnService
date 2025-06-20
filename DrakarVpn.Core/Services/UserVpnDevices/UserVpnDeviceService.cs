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
    private readonly IServiceProvider serviceProvider;
    private readonly IWireGuardClientConfigGenerator configGenerator;
    private IMapper mapper;

    public UserVpnDeviceService(
        IUserVpnDeviceRepository deviceRepo,
        ISubscriptionRepository subscriptionRepository,
        IPeerService peerService,
        IServiceProvider serviceProvider,
        IMapper mapper,
        IWireGuardClientConfigGenerator configGenerator)
    {
        this.deviceRepo = deviceRepo;
        this.subscriptionRepository = subscriptionRepository;
        this.peerService = peerService;
        this.serviceProvider = serviceProvider;
        this.configGenerator = configGenerator;
        this.mapper = mapper;
    }

    public async Task CreateDeviceAsync(string userId, UserVpnDeviceCreateDto dto)
    {
        var sub = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId)
            ?? throw new Exception("No active subscription");

        var currentCount = await deviceRepo.CountByUserIdAsync(userId);
        if (currentCount >= sub.Tariff.MaxDevices)
            throw new Exception("Device limit reached");

        var existingDevice = await deviceRepo.FindByPublicKeyAsync(dto.PublicKey);
        if (existingDevice != null)
            throw new Exception("Device with the same public key already exists");

        var peerResult = await peerService.AddPeerAsync(userId, dto.PublicKey);

        var device = new UserVpnDevice
        {
            UserId = userId,
            PeerId = peerResult.PeerId,
            DeviceName = dto.DeviceName,
            AssignedIp = peerResult.AssignedIp,
            CreatedAt = DateTime.UtcNow
        };

        await deviceRepo.AddAsync(device);
    }

    public async Task<UserVpnDeviceListWithLimitDto> GetDevicesWithConfigAsync(string userId)
    {
        var devices = await deviceRepo.GetAllByUserIdAsync(userId);
        var serverInfo = configGenerator.GetConfig();

        var mappedDevices = mapper.Map<List<UserVpnDeviceResultDto>>(devices, opt =>
        {
            opt.Items["ServiceProvider"] = serviceProvider;
        });

        var maxDevices = 0; 
        var subscription = await subscriptionRepository.GetActiveSubscriptionByUserIdAsync(userId);
        if (subscription != null)
            maxDevices = subscription.Tariff.MaxDevices;

        return new UserVpnDeviceListWithLimitDto
        {
            Devices = mappedDevices,
            MaxDevices = maxDevices
        };
    }


    public async Task<string?> DeleteDeviceAsync(Guid deviceId)
    {
        var device = await deviceRepo.GetByIdAsync(deviceId);
        if (device == null)
            return null;

        await deviceRepo.DeleteAsync(deviceId);
        await peerService.RemovePeerByPeerIdAsync(device.PeerId);

        return device.DeviceName;
    }
}
