using AutoMapper;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Subscriptions;

namespace DrakarVpn.Core.MappingProfiles;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription, SubscriptionMyDto>()
            .ForMember(dest => dest.CurrentTariffName, opt => opt.MapFrom(src => src.Tariff.Name))
            .ForMember(dest => dest.MaxDevices, opt => opt.MapFrom(src => src.Tariff.MaxDevices));
    }
}