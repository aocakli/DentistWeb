namespace DentOnline.Application.Features.Users._Bases.Queries.GetUserById;

public class GetUserByIdQueryRequestValidator : AbstractValidator<GetUserByIdQueryRequest>
{
    public GetUserByIdQueryRequestValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}