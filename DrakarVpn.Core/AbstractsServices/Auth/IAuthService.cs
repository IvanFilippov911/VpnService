using DrakarVpn.Core.Utils;
using DrakarVpn.Domain.ModelDto.Auth;

namespace DrakarVpn.Core.AbstractsServices.Auth;

public interface IAuthService
{
    Task<ServiceResult<LoginResponseDto>> LoginUserAsync(LoginRequestDto loginRequestDto);
    Task<ServiceResult<RegisterResponseDto>> RegisterUserAsync(RegisterRequestDto registerRequestDto);
}