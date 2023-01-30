namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateUpperJawVisualFile;

public class UpdateUpperJawVisualFileCommandRequestValidator : AbstractValidator<UpdateUpperJawVisualFileCommandRequest>
{
    public UpdateUpperJawVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}