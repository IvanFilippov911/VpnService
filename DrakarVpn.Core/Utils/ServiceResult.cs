using DrakarVpn.Shared.Constants.Errors;

namespace DrakarVpn.Core.Utils;

public class ServiceResult<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public AppError Error { get; set; }


    public static ServiceResult<T> Success(T data)
    {
        return new ServiceResult<T>
        {
            IsSuccess = true,
            Data = data,
            Error = null
        };
    }


    public static ServiceResult<T> Failure(AppError error)
    {
        return new ServiceResult<T>
        {
            IsSuccess = false,
            Data = default,
            Error = error
        };
    }
}
