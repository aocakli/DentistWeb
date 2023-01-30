namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Queries.GetUserTokensByRefreshToken;

public class
    GetUserTokensByRefreshTokenQueryRequestValidator : AbstractValidator<GetUserTokensByRefreshTokenQueryRequest>
{
    public GetUserTokensByRefreshTokenQueryRequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
        RuleFor(x => x.RefreshToken).NotNull().NotEmpty();
    }
}