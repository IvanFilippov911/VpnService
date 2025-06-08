using AutoMapper;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Users;

namespace DrakarVpn.Core.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, UserListItemDto>()
            .ForMember(dest => dest.AccountStatus,
                opt => opt.MapFrom(src => src.IsBlocked ? "Suspended" : (src.IsVerified ? "Active" : "Pending")))
            .ForMember(dest => dest.CurrentTariffName, opt => opt.MapFrom(_ => ""))
            .ForMember(dest => dest.SubscriptionExpiresAt, opt => opt.MapFrom(_ => (DateTime?)null));

        CreateMap<AppUser, UserDetailsDto>()
            .ForMember(dest => dest.VerificationStatus,
                opt => opt.MapFrom(src => src.IsVerified ? "Verified" : "Not Verified"))
            .ForMember(dest => dest.CurrentTariffName, opt => opt.MapFrom(_ => ""))
            .ForMember(dest => dest.SubscriptionExpiresAt, opt => opt.MapFrom(_ => (DateTime?)null))
            .ForMember(dest => dest.LifetimeValue, opt => opt.MapFrom(_ => 0m))
            .ForMember(dest => dest.LastPaymentAmount, opt => opt.MapFrom(_ => 0m))
            .ForMember(dest => dest.LastPaymentDate, opt => opt.MapFrom(_ => (DateTime?)null))
            .ForMember(dest => dest.DownloadsCount, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.SessionsCount, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.TrafficVolumeMb, opt => opt.MapFrom(_ => 0L))
            .ForMember(dest => dest.LimitExceededCount, opt => opt.MapFrom(_ => 0))
            .ForMember(dest => dest.LastIpAddress, opt => opt.MapFrom(_ => ""))
            .ForMember(dest => dest.UserAgent, opt => opt.MapFrom(_ => ""));
    }
}
