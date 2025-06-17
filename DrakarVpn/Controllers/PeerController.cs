using DrakarVpn.Core.AbstractsServices.Peers;
using DrakarVpn.Domain.ModelDto.Peers;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers;

[ApiController]
[Route("api/admin/vpn/peers")]
public class PeerController : ControllerBase
{
    private readonly IPeerService peerService;

    public PeerController(IPeerService peerService)
    {
        this.peerService = peerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPeer([FromBody] AddPeerRequestDto request)
    {
        var result = await peerService.AddPeerAsync(request.UserId, request.PublicKey, request.PrivateKey);
        return Ok(result);
    }


    [HttpDelete("{peerId}")]
    public async Task<IActionResult> RemovePeer(Guid peerId)
    {
        await peerService.RemovePeerByPeerIdAsync(peerId);
        return Ok();
    }


    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllPeers([FromQuery] bool onlyActive = false)
    {
        var peers = await peerService.GetAllPeersAsync(onlyActive);
        return Ok(peers);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetPeersByFilter([FromQuery] PeerFilterDto filter)
    {
        var peers = await peerService.GetPeersByFilterAsync(filter);
        return Ok(peers);
    }

    [HttpGet("wireguard-peers")]
    public async Task<IActionResult> GetWireGuardPeers()
    {
        var peers = await peerService.GetWireGuardPeersAsync();
        return Ok(peers);
    }



}

