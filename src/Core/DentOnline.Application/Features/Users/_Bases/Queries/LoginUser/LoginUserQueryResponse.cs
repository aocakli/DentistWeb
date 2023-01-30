using DentOnline.Application.Features.Users._Bases.Dtos;
using DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Dtos;

namespace DentOnline.Application.Features.Users._Bases.Queries.LoginUser;

public class LoginUserQueryResponse : UserLoginDto
{
    public UserTokenDto AccessToken { get; set; }
}