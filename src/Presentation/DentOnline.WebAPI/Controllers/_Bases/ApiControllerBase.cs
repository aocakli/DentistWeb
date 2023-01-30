using DentOnline.Application.Utilities.Responses.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers._Bases;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ApiControllerBase : ControllerBase
{
    protected IMediator Mediator;

    public ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    [NonAction]
    protected IActionResult GenerateResponse(IResponse response)
    {
        return new JsonResult(response) { StatusCode = (int)response.StatusCode };
    }
}