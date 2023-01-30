namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateCbctVisualFile;

public class UpdateCbctVisualFileCommandRequestValidator : AbstractValidator<UpdateCbctVisualFileCommandRequest>
{
    public UpdateCbctVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}