namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Commands.CreateTreatmentStatu;

public class CreateTreatmentStatuCommandRequestValidator : AbstractValidator<CreateTreatmentStatuCommandRequest>
{
    public CreateTreatmentStatuCommandRequestValidator()
    {
        RuleFor(x => x.StatuType).NotNull().NotEmpty();
    }
}