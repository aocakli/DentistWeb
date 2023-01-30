using System.Net;

namespace DentOnline.Application.Utilities.Responses.Abstracts;

public abstract class Response : IResponse
{
    public Response(string message, HttpStatusCode statusCode, bool ısSuccess)
    {
        Message = message;
        StatusCode = statusCode;
        IsSuccess = ısSuccess;
    }

    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}