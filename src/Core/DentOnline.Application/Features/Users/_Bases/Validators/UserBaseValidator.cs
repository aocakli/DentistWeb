using DentOnline.Domain.Concrete.Users._Bases.Abstracts;

namespace DentOnline.Application.Features.Users._Bases.Validators;

public class UserBaseValidator<T> : AbstractValidator<T> where T : IUserBase
{
    public UserBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email).NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}