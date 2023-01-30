using System.Net;

namespace DentOnline.Application.Utilities.Responses.Concretes;

public class ErrorResponse : Response
{
    public ErrorResponse(string message, HttpStatusCode statusCode, List<KeyValuePair<string, string>> validationErrors)
        : base(message, statusCode, false)
    {
        ValidationErrors = validationErrors;
    }

    public ErrorResponse(string message) : this(message, HttpStatusCode.BadRequest, null)
    {
    }

    public List<KeyValuePair<string, string>> ValidationErrors { get; set; }
}