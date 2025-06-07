using System.Net;

namespace DrakarVpn.API.Contracts;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; }
    public T Result { get; set; }
}
