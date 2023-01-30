using DentOnline.Application.Constants;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    CreateSickPeopleInformation;

public class
    CreateSickPeopleInformationCommandRequestValidator : AbstractValidator<CreateSickPeopleInformationCommandRequest>
{
    public CreateSickPeopleInformationCommandRequestValidator(LanguageService languageService)
    {
        RuleFor(x => x.Age).NotNull();

        RuleFor(x => x.Gender)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Gender)
            .Must(x => x.Equals('M') || x.Equals('W'))
            .WithMessage(languageService.Get(Messages.GenderShouldEqualMorW));
    }
}