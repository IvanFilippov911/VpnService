using DrakarVpn.Shared.Constants.Errors;
using System.Text.Json;

namespace DrakarVpn.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
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

        var responseBody = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(responseBody);
    }
}
