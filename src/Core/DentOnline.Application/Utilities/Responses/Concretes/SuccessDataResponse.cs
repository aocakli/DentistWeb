using System.Net;

namespace DentOnline.Application.Utilities.Responses.Concretes;

public class SuccessDataResponse<T> : DataResponse<T>
{
    public SuccessDataResponse(string message, T data) : base(message, HttpStatusCode.OK, true, data)
    {
    }

    public SuccessDataResponse(string message, HttpStatusCode statusCode, T data) : base(message, statusCode, true,
        data)
    {
    }
}