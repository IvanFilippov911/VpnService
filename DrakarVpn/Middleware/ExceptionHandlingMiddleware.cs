using DrakarVpn.Domain.Enums;
using DrakarVpn.Shared.Constants.Errors;
using System.Security.Claims;
using System.Text.Json;

namespace DrakarVpn.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMasterLogService logService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogAndHandleExceptionAsync(context, ex, logService);
        }
    }

    private static async Task LogAndHandleExceptionAsync(HttpContext context, Exception exception, IMasterLogService logService)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var ua = context.Request.Headers["User-Agent"].ToString();
        var method = context.Request.Method;
        var path = context.Request.Path;
        var traceId = context.TraceIdentifier;
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unauthorized";

        var source = context.Items["SystemLogSource"] as SystemLogSource? ?? SystemLogSource.Unknown;

        var message = $"[{method}] {path} | TraceId: {traceId} | UserId: {userId} | IP: {ip} | UA: {ua} | Error: {exception.Message}";

        await logService.LogSystemEventAsync(
            source,
            SystemErrorCode.UnhandledException,
            message,
            exception.StackTrace
        );

        var appError = AppErrors.Exception(exception.Message);

        var response = new
        {
            isSuccess = false,
            statusCode = (int)appError.StatusCode,
            errorMessages = new List<string> { appError.Message },
            result = (object?)null
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)appError.StatusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
