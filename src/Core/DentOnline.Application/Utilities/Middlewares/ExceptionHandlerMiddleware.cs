using System.Net;
using System.Text.Json;
using DentOnline.Application.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using ValidationException = DentOnline.Application.Utilities.Exceptions.ValidationException;

namespace DentOnline.Application.Utilities.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        ErrorResponse response = null;

        if (exception is ValidationException validationException)
            response = new ErrorResponse("Bazı alanlarda veriler eksik. Lütfen bilgileri kontrol edip tekrar dene.",
                HttpStatusCode.BadRequest, validationException.ValidationErrors);
        else if (exception is BusinessException businessException)
            response = new ErrorResponse(businessException.Message, HttpStatusCode.BadRequest, null);
        else if (exception is ErrorException errorException)
            response = new ErrorResponse(errorException.Message, HttpStatusCode.InternalServerError, null);
        else
            response = new ErrorResponse("Bilinmeyen bir hata oluştu: " + exception.Message,
                HttpStatusCode.InternalServerError, null);

        httpContext.Response.StatusCode = (int)response.StatusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)));
    }
}