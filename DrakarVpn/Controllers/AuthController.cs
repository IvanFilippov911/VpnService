using DrakarVpn.Core.AbstractsServices.Auth;
using DrakarVpn.Domain.ModelDto.Auth;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DrakarVpn.API.Controllers;

public class AuthController : WrapperController
{
    private readonly IAuthService service;

    public AuthController(IAuthService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto registerRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return StatusCode((int)AppErrors.InvalidModel.StatusCode,
                CreateErrorResponse<object>((
                AppErrors.InvalidModel.StatusCode, 
                new List<string> { AppErrors.InvalidModel.Message })));
        }

        if (registerRequestDto == null)
        {
            return StatusCode((int)AppErrors.ObjectIsNull.StatusCode,
                CreateErrorResponse<object>((
                AppErrors.ObjectIsNull.StatusCode, 
                new List<string> { AppErrors.ObjectIsNull.Message })));
        }

        var serviceResult = await service.RegisterUserAsync(registerRequestDto);

        if (serviceResult.IsSuccess)
        {
            return Ok(CreateSuccessResponse(serviceResult.Data));
        }

        return StatusCode((int)serviceResult.Error.StatusCode,
        CreateErrorResponse<object>(serviceResult.Error));

    }

    [HttpPost]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto loginRequestDto
    )
    {
        var serviceResult = await service.LoginUserAsync(loginRequestDto);

        if (serviceResult.IsSuccess)
        {
            return Ok(CreateSuccessResponse(serviceResult.Data));
        }

        return StatusCode((int)serviceResult.Error.StatusCode,
            CreateErrorResponse<object>(serviceResult.Error));

    }
}
