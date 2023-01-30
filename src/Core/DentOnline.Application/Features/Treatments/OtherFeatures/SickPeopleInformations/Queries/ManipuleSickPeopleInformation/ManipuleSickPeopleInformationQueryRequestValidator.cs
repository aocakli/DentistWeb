namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Queries.
    ManipuleSickPeopleInformation;

public class
    ManipuleSickPeopleInformationQueryRequestValidator : AbstractValidator<ManipuleSickPeopleInformationQueryRequest>
{
    public ManipuleSickPeopleInformationQueryRequestValidator()
    {
        RuleFor(x => x.SickPeopleInformation).NotNull().NotEmpty();
    }
}