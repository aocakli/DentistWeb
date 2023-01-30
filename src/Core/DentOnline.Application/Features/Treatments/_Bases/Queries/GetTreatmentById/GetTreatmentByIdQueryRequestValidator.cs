namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatmentById;

public class GetTreatmentByIdQueryRequestValidator : AbstractValidator<GetTreatmentByIdQueryRequest>
{
    public GetTreatmentByIdQueryRequestValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}