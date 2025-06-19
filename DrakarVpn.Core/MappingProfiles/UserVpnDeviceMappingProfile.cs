using AutoMapper;
using DrakarVpn.Core.AbstractsServices.WireGuard;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.UserVpnDevices;
using DrakarVpn.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DrakarVpn.Core.MappingProfiles;

public class UserVpnDeviceMappingProfile : Profile
{
    public UserVpnDeviceMappingProfile()
    {
        CreateMap<UserVpnDevice, UserVpnDeviceResultDto>()
            .AfterMap((src, dest, ctx) =>
            {
                var provider = ctx.Items["ServiceProvider"] as IServiceProvider;
                if (provider == null)
                    throw new InvalidOperationException("ServiceProvider not passed");

                var configService = provider.GetRequiredService<IWireGuardClientConfigGenerator>();
                var server = configService.GetConfig();

                dest.ServerConfig = new WireGuardServerInfo
                {
                    Endpoint = server.Endpoint,
                    PublicKey = server.PublicKey,
                    AllowedIPs = server.AllowedIPs,
                    PersistentKeepalive = server.PersistentKeepalive
                };
            });
    }
}