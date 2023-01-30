using DentOnline.Application.Features.Treatments.OtherFeatures.Comments.Commands.CreateCommentToTreatment;
using DentOnline.Application.Utilities.Responses.Abstracts;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("comments")]
public class CommentsController : ApiControllerBase
{
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(IMediator mediator, ILogger<CommentsController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost("add-comment-to-treatment")]
    [Authorize(Roles = AuthRoles.DoctorOrAdmin)]
    public async Task<IActionResult> CreateAsync([FromForm] CreateCommentToTreatmentCommandRequest request)
    {
        return GenerateResponse(await Mediator.Send<IResponse>(request));
    }
}