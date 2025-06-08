using DrakarVpn.API.Contracts;
using DrakarVpn.Shared.Constants.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace DrakarVpn.API.Controllers;

[ApiController]
public class WrapperController : ControllerBase
{
    protected Response<T> CreateSuccessResponse<T>(T result, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new Response<T>
        {
            Result = result,
            StatusCode = status,
            IsSuccess = true,
            ErrorMessages = null
        };
    }

    protected Response<T> CreateErrorResponse<T>((HttpStatusCode statusCode, List<string> errorMessages) error)
    {
        return new Response<T>
        {
            Result = default,
            StatusCode = error.statusCode,
            IsSuccess = false,
            ErrorMessages = error.errorMessages
        };
    }

    protected Response<T> CreateErrorResponse<T>(AppError appError)
    {
        return new Response<T>
        {
            Result = default,
            StatusCode = appError.StatusCode,
            IsSuccess = false,
            ErrorMessages = new List<string> { appError.Message }
        };
    }

    protected string GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("UserId is missing in token"); 
        }

        return userId;
    }


}

