using AutoMapper;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Peers;

namespace DrakarVpn.Core.MappingProfiles;

public class PeerProfile : Profile
{
    public PeerProfile()
    {
        CreateMap<Peer, PeerDto>();
    }
}

