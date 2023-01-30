namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.
    CreateTreatmentStatuToTreatment;

public class
    CreateTreatmentStatuToTreatmentCommandRequestValidator : AbstractValidator<
        CreateTreatmentStatuToTreatmentCommandRequest>
{
    public CreateTreatmentStatuToTreatmentCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.StatuType).NotNull().NotEmpty();
    }
}