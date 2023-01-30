namespace DentOnline.Application.Features.Users._Bases.Queries.GetRolesByUserId;

public class GetRolesByUserIdQueryRequestValidator : AbstractValidator<GetRolesByUserIdQueryRequest>
{
    public GetRolesByUserIdQueryRequestValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}