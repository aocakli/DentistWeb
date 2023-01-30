namespace DentOnline.Application.Features.Users._Bases.Queries.LoginUser;

public class LoginUserQueryRequestValidator : AbstractValidator<LoginUserQueryRequest>
{
    public LoginUserQueryRequestValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}