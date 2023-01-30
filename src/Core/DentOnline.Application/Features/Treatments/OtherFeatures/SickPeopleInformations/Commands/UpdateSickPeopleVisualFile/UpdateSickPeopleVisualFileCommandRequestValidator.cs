namespace DentOnline.Application.Features.Treatments.OtherFeatures.SickPeopleInformations.Commands.
    UpdateSickPeopleVisualFile;

public class
    UpdateSickPeopleVisualFileCommandRequestValidator : AbstractValidator<UpdateSickPeopleVisualFileCommandRequest>
{
    public UpdateSickPeopleVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}