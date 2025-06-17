using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Peers;
using DrakarVpn.Core.AbstractsServices.Peers;
using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Domain.Models;

public class PeerService : IPeerService
{
    private readonly IPeerRepository peerRepository;
    private readonly IWireGuardIpAllocator ipAllocator;
    private readonly IWireGuardManagementService wireGuardManagementService;
    private readonly IMapper mapper;
    private readonly IWireGuardClientConfigGenerator configGenerator;

    public PeerService(
        IPeerRepository peerRepository,
        IWireGuardIpAllocator ipAllocator,
        IWireGuardManagementService wireGuardManagementService,
        IMapper mapper,
        IWireGuardClientConfigGenerator configGenerator)
    {
        this.peerRepository = peerRepository;
        this.ipAllocator = ipAllocator;
        this.wireGuardManagementService = wireGuardManagementService;
        this.mapper = mapper;
        this.configGenerator = configGenerator;
    }

    public async Task<AddPeerResultDto> AddPeerAsync(Guid userId, string publicKey, string privateKey)
    {
        var existingPeer = await peerRepository.GetActivePeerByUserIdAsync(userId);

        if (existingPeer != null)
        {
            throw new ApplicationException("User already has active VPN peer.");
        }

        var assignedIp = await ipAllocator.AllocateNextIpAsync();

        var wgPeerInfo = new WireGuardPeerInfo
        {
            PublicKey = publicKey,
            AllowedIp = assignedIp
        };

        await using var transaction = await peerRepository.BeginTransactionAsync();

        try
        {
            await wireGuardManagementService.AddPeerAsync(wgPeerInfo);

            var peer = new Peer
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PublicKey = publicKey,
                PrivateKey = privateKey,
                AssignedIP = assignedIp,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await peerRepository.AddPeerAsync(peer);
            await transaction.CommitAsync();
            var clientConfig = configGenerator.GenerateClientConfig(wgPeerInfo);

            return new AddPeerResultDto
            {
                Peer = mapper.Map<PeerResponseDto>(peer),
                ClientConfig = clientConfig
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RemovePeerByPeerIdAsync(Guid peerId)
    {
        var peer = await peerRepository.GetPeerByIdAsync(peerId);

        if (peer == null || !peer.IsActive)
        {
            throw new ApplicationException("No active VPN peer found.");
        }

        await using var transaction = await peerRepository.BeginTransactionAsync();

        try
        {
            await wireGuardManagementService.RemovePeerAsync(peer.PublicKey);
            await peerRepository.MarkPeerAsInactiveAsync(peerId);
            await ipAllocator.ReleaseIpAsync(peer.AssignedIP);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<PeerResponseDto>> GetAllPeersAsync(bool onlyActive = false)
    {
        var peers = await peerRepository.GetAllPeersAsync(onlyActive);
        return mapper.Map<List<PeerResponseDto>>(peers);
    }

    public async Task<List<WireGuardPeerInfo>> GetWireGuardPeersAsync()
    {
        return await wireGuardManagementService.GetPeersAsync();
    }

    public async Task<List<PeerResponseDto>> GetPeersByFilterAsync(PeerFilterDto filter)
    {
        var peers = await peerRepository.GetPeersByFilterAsync(filter);
        return mapper.Map<List<PeerResponseDto>>(peers);
    }
}
