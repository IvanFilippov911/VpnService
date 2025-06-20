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

    public PeerService(
        IPeerRepository peerRepository,
        IWireGuardIpAllocator ipAllocator,
        IWireGuardManagementService wireGuardManagementService,
        IMapper mapper)
    {
        this.peerRepository = peerRepository;
        this.ipAllocator = ipAllocator;
        this.wireGuardManagementService = wireGuardManagementService;
        this.mapper = mapper;
    }

    public async Task<PeerAllocationResult> AddPeerAsync(string userId, string publicKey)
    {
        var assignedIp = await ipAllocator.AllocateNextIpAsync();

        var wgPeerInfo = new WireGuardPeerInfo
        {
            PublicKey = publicKey,
            AllowedIp = assignedIp
        };

        await using var tx = await peerRepository.BeginTransactionAsync();

        try
        {
            await wireGuardManagementService.AddPeerAsync(wgPeerInfo);

            var peer = new Peer
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PublicKey = publicKey,
                AssignedIP = assignedIp,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await peerRepository.AddPeerAsync(peer);
            await tx.CommitAsync();

            return new PeerAllocationResult
            {
                PeerId = peer.Id,
                AssignedIp = assignedIp
            };
        }
        catch
        {
            await tx.RollbackAsync();
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

    public async Task<List<PeerDto>> GetAllPeersAsync(bool onlyActive = false)
    {
        var peers = await peerRepository.GetAllPeersAsync(onlyActive);
        return mapper.Map<List<PeerDto>>(peers);
    }

    public async Task<List<WireGuardPeerInfo>> GetWireGuardPeersAsync()
    {
        return await wireGuardManagementService.GetPeersAsync();
    }

    public async Task<List<PeerDto>> GetPeersByFilterAsync(PeerFilterDto filter)
    {
        var peers = await peerRepository.GetPeersByFilterAsync(filter);
        return mapper.Map<List<PeerDto>>(peers);
    }
}
