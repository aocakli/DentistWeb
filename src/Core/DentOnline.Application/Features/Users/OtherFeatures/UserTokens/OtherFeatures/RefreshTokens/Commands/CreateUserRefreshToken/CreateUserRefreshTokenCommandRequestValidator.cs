namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens.OtherFeatures.RefreshTokens.Commands.
    CreateUserRefreshToken;

public class CreateUserRefreshTokenCommandRequestValidator : AbstractValidator<CreateUserRefreshTokenCommandRequest>
{
    public CreateUserRefreshTokenCommandRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .When(x => x.User is null);

        RuleFor(x => x.User)
            .NotNull()
            .NotEmpty()
            .When(x => x.UserId is null);
    }
}