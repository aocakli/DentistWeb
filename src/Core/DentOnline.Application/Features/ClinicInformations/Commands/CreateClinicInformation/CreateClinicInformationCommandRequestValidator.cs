namespace DentOnline.Application.Features.ClinicInformations.Commands.CreateClinicInformation;

public class CreateClinicInformationCommandRequestValidator : AbstractValidator<CreateClinicInformationCommandRequest>
{
    public CreateClinicInformationCommandRequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();

        RuleFor(x => x.TelephoneNumber)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(13);

        RuleFor(x => x.CargoAddress)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(x => x.ClinicName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .When(x => x.ClinicName is not null);

        RuleFor(x => x.CompanyTitle)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .When(x => x.CompanyTitle is not null);

        RuleFor(x => x.TaxOffice)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .When(x => x.TaxOffice is not null);

        RuleFor(x => x.TaxNumber)
            .NotNull()
            .NotEmpty()
            .Length(10)
            .When(x => x.TaxNumber is not null);
    }
}