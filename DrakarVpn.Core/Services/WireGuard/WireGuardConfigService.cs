using DrakarVpn.Core.AbstractsRepositories.WireGuard;
using DrakarVpn.Core.AbstractsServices.Configs;
using DrakarVpn.Domain.Models;
using DrakarVpn.Domain.ModelsOptions;
using Microsoft.Extensions.Options;

namespace DrakarVpn.Core.Services.Configs;

public class WireGuardConfigService : IWireGuardConfigService
{
    private readonly IFileSystem fileSystem;
    private readonly IProcessExecutor processExecutor;
    private readonly string configFilePath;

    private static readonly object fileLock = new();

    public WireGuardConfigService(IFileSystem fileSystem, IProcessExecutor processExecutor, IOptions<WireGuardConfigOptions> options)
    {
        this.fileSystem = fileSystem;
        this.processExecutor = processExecutor;
        configFilePath = options.Value.ConfigFilePath;
    }

    public List<WireGuardPeerInfo> GetCurrentPeers()
    {
        lock (fileLock)
        {
            var lines = fileSystem.ReadAllLines(configFilePath);
            var peers = new List<WireGuardPeerInfo>();

            WireGuardPeerInfo? currentPeer = null;

            foreach (var line in lines)
            {
                if (line.Trim() == "[Peer]")
                {
                    currentPeer = new WireGuardPeerInfo();
                    peers.Add(currentPeer);
                }
                else if (currentPeer != null)
                {
                    if (line.StartsWith("PublicKey ="))
                    {
                        currentPeer.PublicKey = line.Split('=')[1].Trim();
                    }
                    else if (line.StartsWith("AllowedIPs ="))
                    {
                        currentPeer.AllowedIp = line.Split('=')[1].Trim();
                    }
                }
            }

            return peers;
        }
    }

    public void AddPeer(WireGuardPeerInfo peerInfo)
    {
        if (string.IsNullOrWhiteSpace(peerInfo.PublicKey))
            throw new ArgumentException("PublicKey is required.", nameof(peerInfo));

        if (string.IsNullOrWhiteSpace(peerInfo.AllowedIp))
            throw new ArgumentException("AllowedIp is required.", nameof(peerInfo));

        lock (fileLock)
        {
            var existingPeers = GetCurrentPeers();
            if (existingPeers.Any(p => p.PublicKey == peerInfo.PublicKey))
                throw new InvalidOperationException($"Peer with PublicKey {peerInfo.PublicKey} already exists.");

            var peerBlock = new List<string>
            {
                "",
                "[Peer]",
                $"PublicKey = {peerInfo.PublicKey}",
                $"AllowedIPs = {peerInfo.AllowedIp}"
            };

            fileSystem.AppendAllLines(configFilePath, peerBlock);

            ReloadWireGuard();
        }
    }

    public void RemovePeer(string publicKey)
    {
        lock (fileLock)
        {
            var lines = fileSystem.ReadAllLines(configFilePath).ToList();
            var newLines = new List<string>();

            bool inPeerBlock = false;
            bool skipBlock = false;

            foreach (var line in lines)
            {
                if (line.Trim() == "[Peer]")
                {
                    inPeerBlock = true;
                    skipBlock = false;
                    newLines.Add(line);
                }
                else if (inPeerBlock && line.StartsWith("PublicKey ="))
                {
                    var pk = line.Split('=')[1].Trim();
                    if (pk == publicKey)
                    {
                        skipBlock = true;
                        continue; 
                    }
                    else
                    {
                        newLines.Add(line);
                    }
                }
                else if (inPeerBlock && line.StartsWith("["))
                {
                    inPeerBlock = false;
                    if (!skipBlock)
                        newLines.Add(line);
                    else
                        skipBlock = false;
                }
                else
                {
                    if (!skipBlock)
                        newLines.Add(line);
                }
            }

            fileSystem.WriteAllLines(configFilePath, newLines);

            ReloadWireGuard();
        }
    }

    public void ReloadWireGuard()
    {
        processExecutor.Execute("/bin/bash", "-c \"sudo systemctl restart wg-quick@wg0\"");
    }
}
