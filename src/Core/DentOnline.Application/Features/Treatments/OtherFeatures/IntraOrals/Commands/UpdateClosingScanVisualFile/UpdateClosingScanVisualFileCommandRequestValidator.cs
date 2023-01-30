namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateClosingScanVisualFile;

public class
    UpdateClosingScanVisualFileCommandRequestValidator : AbstractValidator<UpdateClosingScanVisualFileCommandRequest>
{
    public UpdateClosingScanVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}