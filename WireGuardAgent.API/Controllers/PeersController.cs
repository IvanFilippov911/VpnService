using DrakarVpn.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WireGuardAgent.API.AbstractsServices;

namespace WireGuardAgent.API.Controllers;

[ApiController]
[Route("api/internal/wireguard/peers")]
public class PeersController : ControllerBase
{
    private readonly IWireGuardConfigService wireGuardConfigService;

    public PeersController(IWireGuardConfigService wireGuardConfigService)
    {
        this.wireGuardConfigService = wireGuardConfigService;
    }

    [HttpGet]
    public IActionResult GetPeers()
    {
        var peers = wireGuardConfigService.GetCurrentPeers();
        return Ok(peers);
    }

    [HttpPost]

    public IActionResult AddPeer([FromBody] WireGuardPeerInfo peerInfo)
    {
        wireGuardConfigService.AddPeer(peerInfo);
        return Ok();
    }

    [HttpDelete("{publicKey}")]
    public IActionResult RemovePeer(string publicKey)
    {
        wireGuardConfigService.RemovePeer(publicKey);
        return Ok();
    }
}
