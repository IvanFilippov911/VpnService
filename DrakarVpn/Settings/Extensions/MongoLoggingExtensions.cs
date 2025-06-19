using DrakarVpn.Domain.ModelsOptions;
using DrakarVpn.Infrastructure.Persistence;

namespace DrakarVpn.API.Settings.Extensions;

public static class MongoLoggingExtensions
{
    public static IServiceCollection AddMongoLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("Mongo"));
        services.AddSingleton<MongoLogDbContext>();
        return services;
    }
}
