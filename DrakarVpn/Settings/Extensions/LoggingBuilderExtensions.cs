namespace DrakarVpn.API.Settings.Extensions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using global::DrakarVpn.Core.Services.Logging;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddMongoLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerProvider, MongoLoggerProvider>();
        return builder;
    }
}