namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.UpdateLowerJawVisualFile;

public class UpdateLowerJawVisualFileCommandRequestValidator : AbstractValidator<UpdateLowerJawVisualFileCommandRequest>
{
    public UpdateLowerJawVisualFileCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
        RuleFor(x => x.File).NotNull().NotEmpty();
    }
}