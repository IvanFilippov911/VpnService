using DrakarVpn.Core.Auth;

namespace DrakarVpn.API.Settings.Extensions
{
    public static class JwtTokenGeneratorServiceExtensions
    {
        public static IServiceCollection AddJwtTokenGenerator(
            this IServiceCollection services
        )
        {
            return services.AddScoped<JwtTokenGenerator>();
        }
    }
}
