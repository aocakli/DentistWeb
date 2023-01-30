using DentOnline.Application.Features.Users.OtherFeatures.Admins.Commands.CreateAdmin;
using DentOnline.Application.Utilities.Responses.Abstracts;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("admins")]
public class AdminsController : ApiControllerBase
{
    public AdminsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("create"), Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> CreateAsync(CreateAdminCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send<IResponse>(request));
    }
}