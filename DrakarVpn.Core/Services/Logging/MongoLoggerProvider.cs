

using Microsoft.Extensions.Logging;

namespace DrakarVpn.Core.Services.Logging;

public class MongoLoggerProvider : ILoggerProvider
{
    private readonly IMasterLogService logService;

    public MongoLoggerProvider(IMasterLogService logService)
    {
        this.logService = logService;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MongoLogger(categoryName, logService);
    }

    public void Dispose() { }
}

