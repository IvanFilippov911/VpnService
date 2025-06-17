using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Models;
using System.Net.Http.Json;

namespace DrakarVpn.Core.Services.WireGuard;


public class WireGuardManagementService : IWireGuardManagementService
{
    private readonly HttpClient httpClient;

    public WireGuardManagementService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<WireGuardPeerInfo>> GetPeersAsync()
    {
        var response = await httpClient.GetAsync("/api/internal/wireguard/peers");

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Failed to get peers. StatusCode: {response.StatusCode}");
        }

        var peers = await response.Content.ReadFromJsonAsync<List<WireGuardPeerInfo>>();
        return peers ?? new List<WireGuardPeerInfo>();
    }

    public async Task AddPeerAsync(WireGuardPeerInfo peerInfo)
    {
        var response = await httpClient.PostAsJsonAsync("/api/internal/wireguard/peers", peerInfo);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Failed to add peer. StatusCode: {response.StatusCode}");
        }
    }

    public async Task RemovePeerAsync(string publicKey)
    {
        var response = await httpClient.DeleteAsync($"/api/internal/wireguard/peers/{publicKey}");

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Failed to remove peer. StatusCode: {response.StatusCode}");
        }
    }

}
