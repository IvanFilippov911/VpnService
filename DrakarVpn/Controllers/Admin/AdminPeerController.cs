using DrakarVpn.Core.AbstractsServices.Peers;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Peers;
using DrakarVpn.Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.Admin;

[ApiController]
[Route("api/admin/vpn/peers")]
//[Authorize(Roles = "Admin")]
[SetLogSource(SystemLogSource.Peer)]
public class AdminPeerController : ControllerBase
{
    private readonly IPeerService peerService;

    public AdminPeerController(IPeerService peerService)
    {
        this.peerService = peerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPeer([FromBody] PeerAdminCreateDto request)
    {
        var result = await peerService.AddPeerAsync(request.UserId, request.PublicKey);
        return Ok(result);
    }

    [HttpDelete("{peerId}")]
    public async Task<IActionResult> RemovePeer(Guid peerId)
    {
        await peerService.RemovePeerByPeerIdAsync(peerId);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPeers(
    [FromQuery] PaginationQueryDto pagination,
    [FromQuery] bool onlyActive = false)
    {
        var result = await peerService.GetAllPeersAsync(onlyActive, pagination.Offset, pagination.Limit);
        return Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetPeersByFilter(
    [FromQuery] PeerFilterDto filter)
    {
        var result = await peerService.GetPeersByFilterAsync(filter);
        return Ok(result);
    }

    [HttpGet("wireguard-peers")]
    public async Task<IActionResult> GetWireGuardPeers()
    {
        var peers = await peerService.GetWireGuardPeersAsync();
        return Ok(peers);
    }
}

