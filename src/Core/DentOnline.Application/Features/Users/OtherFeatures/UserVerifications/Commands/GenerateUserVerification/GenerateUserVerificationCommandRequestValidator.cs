namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;

public class GenerateUserVerificationCommandRequestValidator : AbstractValidator<GenerateUserVerificationCommandRequest>
{
    public GenerateUserVerificationCommandRequestValidator()
    {
        RuleFor(x => x.VerificationType).NotNull().NotEmpty();
    }
}