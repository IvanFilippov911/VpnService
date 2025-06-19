

using DrakarVpn.Domain.Enums;

namespace DrakarVpn.Core.Services.Logging;

public static class LogHelper
{
    public static async Task<T> CatchAndLogAsync<T>(
        Func<Task<T>> action,
        IMasterLogService logger,
        SystemLogSource source,
        string contextMessage = "")
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            await logger.LogSystemEventAsync(
                source,
                SystemErrorCode.UnexpectedException,
                $"{contextMessage} - {ex.Message}",
                ex.StackTrace
            );

            throw; 
        }
    }
}

