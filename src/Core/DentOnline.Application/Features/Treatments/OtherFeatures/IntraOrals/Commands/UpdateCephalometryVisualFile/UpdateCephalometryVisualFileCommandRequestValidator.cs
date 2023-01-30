namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCephalometryVisualFile;

public class
    UpdateCephalometryVisualFileCommandRequestValidator : AbstractValidator<UpdateCephalometryVisualFileCommandRequest>
{
    public UpdateCephalometryVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}