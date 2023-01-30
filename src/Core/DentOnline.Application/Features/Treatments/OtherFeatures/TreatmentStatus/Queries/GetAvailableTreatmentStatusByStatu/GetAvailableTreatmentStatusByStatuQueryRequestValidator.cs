namespace DentOnline.Application.Features.Treatments.OtherFeatures.TreatmentStatus.Queries.
    GetAvailableTreatmentStatusByStatu;

public class
    GetAvailableTreatmentStatusByStatuQueryRequestValidator : AbstractValidator<
        GetAvailableTreatmentStatusByStatuQueryRequest>
{
    public GetAvailableTreatmentStatusByStatuQueryRequestValidator()
    {
        RuleFor(x => x.TreatmentStatuType).NotNull().NotEmpty();
    }
}