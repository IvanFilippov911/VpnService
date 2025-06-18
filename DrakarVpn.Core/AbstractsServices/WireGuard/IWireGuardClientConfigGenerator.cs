using DrakarVpn.Domain.Models;

namespace DrakarVpn.Core.AbstractsServices.WireGuard;

public interface IWireGuardClientConfigGenerator
{
    WireGuardServerInfo GetConfig();
}

