using DrakarVpn.Domain.Models;

namespace WireGuardAgent.API.AbstractsServices;

public interface IWireGuardConfigService
{
    List<WireGuardPeerInfo> GetCurrentPeers();
    void AddPeer(WireGuardPeerInfo peerInfo);
    bool RemovePeer(string publicKey);
    void ReloadWireGuard();
}
