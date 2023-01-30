namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Queries.CheckAndVerifyUserVerification;

public class
    CheckAndVerifyUserVerificationQueryRequestValidator : AbstractValidator<CheckAndVerifyUserVerificationQueryRequest>
{
    public CheckAndVerifyUserVerificationQueryRequestValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty();
    }
}