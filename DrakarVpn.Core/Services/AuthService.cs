using DrakarVpn.Core.AbstractsRepositories;
using DrakarVpn.Core.AbstractsServices;
using DrakarVpn.Core.Auth;
using DrakarVpn.Core.Utils;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto;
using DrakarVpn.Shared.Constants;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Identity;

namespace DrakarVpn.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository repository;
    private readonly UserManager<AppUser> userManager;
    private readonly JwtTokenGenerator jwtTokenGenerator;

    public AuthService(

        IAuthRepository appUserRepository,
        UserManager<AppUser> userManager,
        JwtTokenGenerator jwtTokenGenerator)
    {
        repository = appUserRepository;
        this.userManager = userManager;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ServiceResult<RegisterResponseDto>> RegisterUserAsync(RegisterRequestDto registerRequestDto)
    {
        var userFromDb = await repository.GetUserByEmailAsync(registerRequestDto.Email);

        if (userFromDb != null)
            return ServiceResult<RegisterResponseDto>.Failure(AppErrors.UserAlreadyExists);

        var newAppUser = new AppUser
        {
            UserName = registerRequestDto.Email,
            FullName = registerRequestDto.FullName,
            Email = registerRequestDto.Email,
            NormalizedEmail = registerRequestDto.Email.ToUpper(),
        };

        var result = await userManager.CreateAsync(newAppUser, registerRequestDto.Password);

        if (!result.Succeeded)
        {
            var identityError = result.Errors.FirstOrDefault();
            var errorMessage = identityError != null ? identityError.Description : AppErrors.RegisterError.Message;
            var detailedError = new AppError(AppErrors.RegisterError.ErrorCode, errorMessage, AppErrors.RegisterError.StatusCode);

            return ServiceResult<RegisterResponseDto>.Failure(detailedError);
        }

        var newRoleAppUser = SharedData.Roles.User;

        await userManager.AddToRoleAsync(newAppUser, newRoleAppUser);

        return ServiceResult<RegisterResponseDto>.Success(new RegisterResponseDto
        {
            Email = newAppUser.Email,
            Role = newRoleAppUser
        });

    }

    public async Task<ServiceResult<LoginResponseDto>> LoginUserAsync(LoginRequestDto loginRequestDto)
    {
        var userFromDb = await repository.GetUserByEmailAsync(loginRequestDto.Email);

        if (userFromDb == null
            || !await userManager.CheckPasswordAsync(
                userFromDb,
                loginRequestDto.Password))
        {
            return ServiceResult<LoginResponseDto>.Failure(AppErrors.LoginError);
        }

        var roles = await userManager.GetRolesAsync(userFromDb);
        var token = jwtTokenGenerator.GenerateJwtToken(userFromDb, roles);
        var result = new LoginResponseDto
        {
            Email = userFromDb.Email,
            Token = token
        };

        return ServiceResult<LoginResponseDto>.Success(result);
    }

}
