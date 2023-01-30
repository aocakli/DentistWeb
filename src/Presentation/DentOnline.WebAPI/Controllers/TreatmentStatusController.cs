using DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatuToTreatment;
using DentOnline.Application.Utilities.Responses.Abstracts;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("treatment-status")]
public class TreatmentStatusController : ApiControllerBase
{
    private readonly ILogger<TreatmentStatusController> _logger;

    public TreatmentStatusController(IMediator mediator, ILogger<TreatmentStatusController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost("add-statu-to-treatment")]
    [Authorize(Roles = AuthRoles.DoctorOrAdmin)]
    public async Task<IActionResult> AddToTreatmentAsync(CreateTreatmentStatuToTreatmentCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send<IResponse>(request));
    }
}