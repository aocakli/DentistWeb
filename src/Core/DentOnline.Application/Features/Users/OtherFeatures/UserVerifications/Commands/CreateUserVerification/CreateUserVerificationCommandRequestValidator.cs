namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.CreateUserVerification;

public class CreateUserVerificationCommandRequestValidator : AbstractValidator<CreateUserVerificationCommandRequest>
{
    public CreateUserVerificationCommandRequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
        RuleFor(x => x.VerificationType).NotNull().NotEmpty();
    }
}