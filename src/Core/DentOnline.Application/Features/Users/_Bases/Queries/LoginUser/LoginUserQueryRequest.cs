namespace DentOnline.Application.Features.Users._Bases.Queries.LoginUser;

public class LoginUserQueryRequest : IRequest<IDataResponse<LoginUserQueryResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}