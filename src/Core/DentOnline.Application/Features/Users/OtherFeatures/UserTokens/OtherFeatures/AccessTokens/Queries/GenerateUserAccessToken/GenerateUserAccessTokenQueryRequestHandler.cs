using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.Queries.GetUserById;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.AccessTokens.Queries.
    GenerateUserAccessToken;

public class
    GenerateUserAccessTokenQueryRequestHandler : IRequestHandler<GenerateUserAccessTokenQueryRequest,
        IDataResponse<GenerateUserAccessTokenQueryResponse>>
{
    private readonly IConfiguration _configuration;
    private readonly LanguageService _languageService;
    private readonly IMediator _mediator;

    public GenerateUserAccessTokenQueryRequestHandler(IConfiguration configuration, IMediator mediator,
        LanguageService languageService)
    {
        _configuration = configuration;
        _mediator = mediator;
        _languageService = languageService;
    }

    public async Task<IDataResponse<GenerateUserAccessTokenQueryResponse>> Handle(
        GenerateUserAccessTokenQueryRequest request,
        CancellationToken cancellationToken)
    {
        var userResult = await _mediator.Send(new GetUserByIdQueryRequest(request.UserId));
        if (userResult.IsSuccess is false)
            return new ErrorDataResponse<GenerateUserAccessTokenQueryResponse>(userResult.Message);

        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenExpiryDate =
            DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Token:AccessTokenExpiryAsMinute"]));

        var securityToken = new JwtSecurityToken(
            _configuration["Token:Issuer"],
            _configuration["Token:Audience"],
            expires: tokenExpiryDate,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: GetClaims(request.UserId, userResult.Data.Role.UserRolesCollection)
        );

        JwtSecurityTokenHandler tokenHandler = new();

        var token = tokenHandler.WriteToken(securityToken);

        GenerateUserAccessTokenQueryResponse responseModel = new(token, tokenExpiryDate);

        return new SuccessDataResponse<GenerateUserAccessTokenQueryResponse>(
            _languageService.Get(Messages.UserAccessTokenIsGenerated),
            responseModel);
    }

    private static ICollection<Claim> GetClaims(string userId,
        HashSet<UserRoles> userRoles)
    {
        List<Claim> claimsToAdd = new()
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        foreach (var userRole in userRoles)
            claimsToAdd.Add(new Claim(ClaimTypes.Role, userRole.ToString()));

        return claimsToAdd;
    }
}