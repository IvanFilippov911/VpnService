using DrakarVpn.Core.Utils;
using DrakarVpn.Domain.ModelDto;

namespace DrakarVpn.Core.AbstractsServices;

public interface IAuthService
{
    Task<ServiceResult<LoginResponseDto>> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task<ServiceResult<RegisterResponseDto>> RegisterUserAsync(RegisterRequestDto registerRequestDto);
}