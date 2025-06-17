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

        CreateMap<AppUser, UserProfileDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? ""))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName ?? ""))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country ?? ""))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? ""))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language ?? "ru"));

        
        CreateMap<UserProfileDto, AppUser>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))

            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
            .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
            .ForMember(dest => dest.AdminNote, opt => opt.Ignore())
            .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

    }
}
