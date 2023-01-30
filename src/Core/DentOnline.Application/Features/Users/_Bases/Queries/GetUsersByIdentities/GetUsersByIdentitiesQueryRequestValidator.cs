namespace DentOnline.Application.Features.Users._Bases.Queries.GetUsersByIdentities;

public class GetUsersByIdentitiesQueryRequestValidator : AbstractValidator<GetUsersByIdentitiesQueryRequest>
{
    public GetUsersByIdentitiesQueryRequestValidator()
    {
        RuleFor(x => x.Identities).NotNull().NotEmpty();
    }
}