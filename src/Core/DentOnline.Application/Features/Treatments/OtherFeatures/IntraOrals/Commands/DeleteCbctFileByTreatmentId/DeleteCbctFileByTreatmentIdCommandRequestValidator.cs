namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Commands.DeleteCbctFileByTreatmentId;

public class
    DeleteCbctFileByTreatmentIdCommandRequestValidator : AbstractValidator<DeleteCbctFileByTreatmentIdCommandRequest>
{
    public DeleteCbctFileByTreatmentIdCommandRequestValidator()
    {
        RuleFor(x => x.TreatmentId).NotNull().NotEmpty();
    }
}