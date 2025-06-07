using System.Net;

namespace DrakarVpn.Shared.Constants.Errors;

public class AppError
{
    public string ErrorCode { get; }
    public string Message { get; }
    public HttpStatusCode StatusCode { get; }

    public AppError(string errorCode, string message, HttpStatusCode statusCode)
    {
        ErrorCode = errorCode;
        Message = message;
        StatusCode = statusCode;
    }
}