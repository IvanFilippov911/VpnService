using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DrakarVpn.API.Settings.Extensions
{
    public static class PostgreSqlServiceExtensions
    {
        public static IServiceCollection AddPostgreSqlContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<DrakarVpnDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
