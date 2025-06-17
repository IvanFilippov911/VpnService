namespace DrakarVpn.Core.AbstractsServices.WireGuard;

public interface IWireGuardIpAllocator
{
    Task<string> AllocateNextIpAsync();
    Task ReleaseIpAsync(string ip);
}