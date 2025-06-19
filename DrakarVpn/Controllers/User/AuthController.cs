using DrakarVpn.Core.AbstractsServices.Auth;
using DrakarVpn.Core.Services.Logging;
using DrakarVpn.Domain.Enums;
using DrakarVpn.Domain.ModelDto.Auth;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers.User;

[Route("api/user/[controller]")]
[SetLogSource(SystemLogSource.Auth)]
public class AuthController : WrapperController
{
    private readonly IAuthService service;
    private readonly IMasterLogService logService;

    public AuthController(IAuthService service, IMasterLogService logService)
    {
        this.service = service;
        this.logService = logService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        if (!ModelState.IsValid)
            return Error(AppErrors.InvalidModel);

        if (registerRequestDto == null)
            return Error(AppErrors.ObjectIsNull);

        var serviceResult = await service.RegisterUserAsync(registerRequestDto);

        if (serviceResult.IsSuccess)
        {
            await logService.LogUserActionAsync(
                serviceResult.Data.UserId,
                UserActionType.Register,
                $"Email: {registerRequestDto.Email}"
            );

            return Ok(CreateSuccessResponse(serviceResult.Data));
        }

        return Error(serviceResult.Error);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var serviceResult = await service.LoginUserAsync(loginRequestDto);

        if (serviceResult.IsSuccess)
        {
            await logService.LogUserActionAsync(
                serviceResult.Data.UserId,
                UserActionType.Login,
                $"Email: {loginRequestDto.Email}"
            );

            return Ok(CreateSuccessResponse(serviceResult.Data));
        }

        return Error(serviceResult.Error);
    }
}
