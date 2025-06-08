using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrakarVpn.API.Settings.Extensions;

public static class ApplicationMappingExtensions
{
    public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Core.MappingProfiles.UserProfile).Assembly);
        
        services.AddValidatorsFromAssemblyContaining<Core.Validators.Auth.RegisterRequestDtoValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}
