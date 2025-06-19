

using DrakarVpn.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace DrakarVpn.Core.Services.Logging;

public class MongoLogger : ILogger
{
    private readonly string categoryName;
    private readonly IMasterLogService logService;

    public MongoLogger(string categoryName, IMasterLogService logService)
    {
        this.categoryName = categoryName;
        this.logService = logService;
    }

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Warning;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        var stackTrace = exception?.StackTrace;

        logService.LogSystemEventAsync(
            SystemLogSource.Middleware,
            SystemErrorCode.UnhandledException,
            $"[{logLevel}] {categoryName}: {message}",
            stackTrace
        ).Wait();
    }
}

