namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentsByIdentities;

public class GetTreatmentsByIdentitiesQueryRequestValidator : AbstractValidator<GetTreatmentsByIdentitiesQueryRequest>
{
    public GetTreatmentsByIdentitiesQueryRequestValidator()
    {
        RuleFor(x => x.Identities).NotNull().NotEmpty();
    }
}