using DrakarVpn.Domain.Models;
using Microsoft.Extensions.Options;
using WireGuardAgent.API.AbstractsRepositories;
using WireGuardAgent.API.AbstractsServices;
using WireGuardAgent.API.ModelOptions;

namespace WireGuardAgent.API.Services;

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
        configFilePath = "/etc/wireguard/wg0.conf";

        Console.WriteLine($"[WireGuardConfigService] Constructor: ConfigFilePath = {configFilePath}");
    }

    public List<WireGuardPeerInfo> GetCurrentPeers()
    {
        lock (fileLock)
        {
            if (!fileSystem.FileExists(configFilePath))
            {
                Console.WriteLine($"[WireGuardConfigService] Config file not found: {configFilePath}");
                return new List<WireGuardPeerInfo>();
            }

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
        Console.WriteLine($"[WireGuardConfigService] Adding peer: PublicKey={peerInfo.PublicKey}, AllowedIp={peerInfo.AllowedIp}");

        if (string.IsNullOrWhiteSpace(peerInfo.PublicKey))
            throw new ArgumentException("PublicKey is required.", nameof(peerInfo));

        if (string.IsNullOrWhiteSpace(peerInfo.AllowedIp))
            throw new ArgumentException("AllowedIp is required.", nameof(peerInfo));

        lock (fileLock)
        {
            var existingPeers = GetCurrentPeers();

            Console.WriteLine($"[WireGuardConfigService] Existing peers count: {existingPeers.Count}");

            if (existingPeers.Any(p => p.PublicKey == peerInfo.PublicKey))
            {
                Console.WriteLine($"[WireGuardConfigService] Peer with PublicKey {peerInfo.PublicKey} already exists — throwing exception.");
                throw new InvalidOperationException($"Peer with PublicKey {peerInfo.PublicKey} already exists.");
            }

            var peerBlock = new List<string>
            {
                "",
                "[Peer]",
                $"PublicKey = {peerInfo.PublicKey}",
                $"AllowedIPs = {peerInfo.AllowedIp}"
            };

            Console.WriteLine($"[WireGuardConfigService] Appending new peer block to {configFilePath}");

            // Всегда просто Append → это правильно и безопасно
            fileSystem.AppendAllLines(configFilePath, peerBlock);

            Console.WriteLine($"[WireGuardConfigService] Reloading WireGuard");

            ReloadWireGuard();

            Console.WriteLine($"[WireGuardConfigService] Peer added successfully.");
        }
    }

    public void RemovePeer(string publicKey)
    {
        lock (fileLock)
        {
            if (!fileSystem.FileExists(configFilePath))
            {
                Console.WriteLine($"[WireGuardConfigService] Config file not found when trying to remove peer: {configFilePath}");
                return;
            }

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

            Console.WriteLine($"[WireGuardConfigService] Reloading WireGuard after peer removal");

            ReloadWireGuard();
        }
    }

    public void ReloadWireGuard()
    {
        processExecutor.Execute("/bin/bash", "-c \"sudo systemctl restart wg-quick@wg0\"");
    }
}
