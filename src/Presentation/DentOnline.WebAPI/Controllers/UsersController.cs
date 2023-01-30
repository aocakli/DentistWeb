using DentOnline.Application.Features.Users._Bases.Queries.GetUserById;
using DentOnline.Application.Features.Users._Bases.Queries.GetUsers;
using DentOnline.Application.Features.Users._Bases.Queries.LoginUser;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Queries.GetUserTokensByRefreshToken;
using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Queries.CheckAndVerifyUserVerification;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;
using DentOnline.WebAPI.Controllers._Bases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentOnline.WebAPI.Controllers;

[Route("users")]
public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet("get-users")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> GetUsersAsync()
    {
        return GenerateResponse(await Mediator.Send(new GetUsersQueryRequest()));
    }

    [HttpGet("get-user-by-id")]
    public async Task<IActionResult> GetUsersAsync([FromQuery] GetUserByIdQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPost("check-and-verify-for-email")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckAndVerifyForEmailAsync(CheckAndVerifyUserVerificationQueryRequest request)
    {
        request.VerificationType = UserVerificationType.Email;
        return GenerateResponse(await Mediator.Send(request));
    }

    [HttpPost("get-user-tokens-with-refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserTokensWithRefreshTokenAsync(GetUserTokensByRefreshTokenQueryRequest request)
    {
        return GenerateResponse(await Mediator.Send(request));
    }
}