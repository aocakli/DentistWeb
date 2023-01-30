using System.Net;

namespace DentOnline.Application.Utilities.Responses.Abstracts;

public interface IResponse
{
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}