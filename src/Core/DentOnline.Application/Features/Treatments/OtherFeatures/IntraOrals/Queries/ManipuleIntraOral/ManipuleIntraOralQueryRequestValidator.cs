namespace DentOnline.Application.Features.Treatments.OtherFeatures.IntraOrals.Queries.ManipuleIntraOral;

public class ManipuleIntraOralQueryRequestValidator : AbstractValidator<ManipuleIntraOralQueryRequest>
{
    public ManipuleIntraOralQueryRequestValidator()
    {
        RuleFor(x => x.IntraOral).NotNull().NotEmpty();
    }
}