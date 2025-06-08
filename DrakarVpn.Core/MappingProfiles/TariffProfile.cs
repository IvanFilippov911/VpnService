using AutoMapper;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Tariffs;


namespace DrakarVpn.Core.MappingProfiles;

public class TariffProfile : Profile
{
    public TariffProfile()
    {
        CreateMap<Tariff, TariffDto>();
    }
}
