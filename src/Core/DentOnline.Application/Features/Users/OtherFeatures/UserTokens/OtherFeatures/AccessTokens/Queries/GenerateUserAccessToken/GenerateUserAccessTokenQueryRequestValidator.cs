namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.AccessTokens.Queries.
    GenerateUserAccessToken;

public class GenerateUserAccessTokenQueryRequestValidator : AbstractValidator<GenerateUserAccessTokenQueryRequest>
{
    public GenerateUserAccessTokenQueryRequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}