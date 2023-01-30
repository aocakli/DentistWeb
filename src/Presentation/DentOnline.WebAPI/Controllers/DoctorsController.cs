using DentOnline.Application.Features.Users.OtherFeatures.Doctors.Commands.CreateDoctor;
using DentOnline.Application.Utilities.Responses.Abstracts;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("doctors")]
public class DoctorsController : ApiControllerBase
{
    public DoctorsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync(CreateDoctorCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send<IResponse>(request));
    }
}