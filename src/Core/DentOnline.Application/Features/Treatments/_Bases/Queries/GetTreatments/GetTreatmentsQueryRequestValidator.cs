namespace DentOnline.Application.Features.Treatments._Bases.Queries.GetTreatments;

public class GetTreatmentsQueryRequestValidator : AbstractValidator<GetTreatmentsQueryRequest>
{
    public GetTreatmentsQueryRequestValidator()
    {
        RuleFor(x => x.Pagination).NotNull();

        RuleFor(x => x.Pagination.Page)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Pagination.ItemCount)
            .GreaterThan(0)
            .When(x => x.Pagination.ItemCount.HasValue);
    }
}