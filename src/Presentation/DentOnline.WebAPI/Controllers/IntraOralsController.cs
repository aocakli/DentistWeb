using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.DeleteCbctFileByTreatmentId;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCbctVisualFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCephalometryVisualFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateClosingScanVisualFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateLowerJawVisualFile;
using DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateUpperJawVisualFile;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("intra-orals")]
public class IntraOralsController : ApiControllerBase
{
    private readonly ILogger<IntraOralsController> _logger;

    public IntraOralsController(IMediator mediator, ILogger<IntraOralsController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpDelete("delete-cbct-visual-file-by-treatment")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> DeleteCbctVisualByTreatmentAsync(DeleteCbctFileByTreatmentIdCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPut("update-cbct-visual-file-by-treatment")]
    [DisableRequestSizeLimit]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateCbctVisualFileByTreatment(
        [FromForm] UpdateCbctVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPut("update-upper-jaw-visual-file-by-treatment")]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateUpperJawVisualFileByTreatment(
        [FromForm] UpdateUpperJawVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPut("update-lower-jaw-visual-file-by-treatment")]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateLowerJawVisualFileByTreatment(
        [FromForm] UpdateLowerJawVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPut("update-closing-scan-visual-file-by-treatment")]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateClosingScanVisualFileByTreatment(
        [FromForm] UpdateClosingScanVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPut("update-cephalometry-visual-file-by-treatment")]
    [Authorize(Roles = AuthRoles.Doctor)]
    public async Task<IActionResult> UpdateCephalometryVisualFileByTreatment(
        [FromForm] UpdateCephalometryVisualFileCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
}