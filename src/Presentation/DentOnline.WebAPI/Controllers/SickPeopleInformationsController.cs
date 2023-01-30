using DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    UpdateSickPeopleVisualFile;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("sick-people-informations")]
public class SickPeopleInformationsController : ApiControllerBase
{
    private readonly ILogger<SickPeopleInformationsController> _logger;

    public SickPeopleInformationsController(IMediator mediator, ILogger<SickPeopleInformationsController> logger) :
        base(mediator)
    {
        _logger = logger;
    }

    [HttpPut("update-sick-people-visual-file-by-treatment")]
    [DisableRequestSizeLimit]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateSickPeopleVisualFileByTreatmentAsync(
        [FromForm] UpdateSickPeopleVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
}