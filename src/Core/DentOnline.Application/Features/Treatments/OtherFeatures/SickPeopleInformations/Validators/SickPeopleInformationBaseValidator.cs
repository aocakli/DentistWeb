using DentOnline.Domain.Concrete.Treatments.OtherDomains.SickPeopleInformations.Abstracts;

namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Validators;

public class
    SickPeopleInformationBaseValidator<T> : AbstractValidator<T> where T : ISickPeopleInformationBase
{
    public SickPeopleInformationBaseValidator()
    {
        RuleFor(x => x.Age)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo((byte)0)
            .LessThanOrEqualTo((byte)140);

        RuleFor(x => x.Gender).NotNull().NotEmpty();
    }
}