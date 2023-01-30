using DentOnline.Application.Features.Treatments._Bases.Commands.CreateTreatment;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentById;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatments;
using DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByDoctorId;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("treatments")]
public class TreatmentsController : ApiControllerBase
{
    private readonly ILogger<TreatmentsController> _logger;

    public TreatmentsController(IMediator mediator, ILogger<TreatmentsController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost("create")]
    [DisableRequestSizeLimit]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> CreateAsync([FromForm] CreateTreatmentCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPost("get-treatments")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> GetTreatmentsAsync(GetTreatmentsQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpGet("get-treatment-by-id")]
    [Authorize(Roles = AuthRoles.DoctorOrAdmin)]
    public async Task<IActionResult> GetTreatmentByIdAsync([FromQuery] GetTreatmentByIdQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpGet("get-treatments-by-doctor-id")]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> GetTreatmentsByDoctorIdAsync(
        [FromQuery] GetTreatmentsByDoctorIdQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
}