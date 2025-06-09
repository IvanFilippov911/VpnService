using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.Configs;

public interface IWireGuardConfigService
{
    List<WireGuardPeerInfo> GetCurrentPeers();
    void AddPeer(WireGuardPeerInfo peerInfo);
    void RemovePeer(string publicKey);
    void ReloadWireGuard();
}
