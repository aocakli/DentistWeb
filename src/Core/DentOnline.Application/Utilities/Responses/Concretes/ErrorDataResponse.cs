using System.Net;

namespace DentOnline.Application.Utilities.Responses.Concretes;

public class ErrorDataResponse<T> : DataResponse<T>
{
    public ErrorDataResponse(string message) : base(message, HttpStatusCode.OK, false, default)
    {
    }

    public ErrorDataResponse(string message, T data) : base(message, HttpStatusCode.OK, false, data)
    {
    }

    public ErrorDataResponse(string message, HttpStatusCode statusCode, T data) : base(message, statusCode, false,
        data)
    {
    }
}