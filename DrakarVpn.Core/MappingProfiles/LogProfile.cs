using AutoMapper;
using DrakarVpn.Domain.Entities.Logging;
using DrakarVpn.Domain.ModelDto.Logging;

namespace DrakarVpn.Core.MappingProfiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<UserActionLogEntry, UserLogDto>();
        CreateMap<SystemLogEntry, SystemLogDto>();
    }
}
