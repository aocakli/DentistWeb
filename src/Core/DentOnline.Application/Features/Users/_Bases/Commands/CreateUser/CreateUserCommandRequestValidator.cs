using DentOnline.Application.Constants;
using DentOnline.Application.Features.Users._Bases.Validators;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

public class CreateUserCommandRequestValidator : AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandRequestValidator()
    {
        Include(new UserBaseValidator<CreateUserCommandRequest>());

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .Equal(x => x.ConfirmPassword);

        RuleFor(x => x.ConfirmPassword)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .Equal(x => x.Password);

        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Roles)
            .Must(x => x.Any(c => c == UserRoles.Unknown) is false)
            .WithMessage(Messages.UserRolesIsUnknown);
    }
}