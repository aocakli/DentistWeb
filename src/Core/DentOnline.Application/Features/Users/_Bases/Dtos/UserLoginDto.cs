using DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Dtos;

namespace DentOnline.Application.Features.Users._Bases.Dtos;

public class UserLoginDto : UserDto
{
    public UserTokenDto? RefreshToken { get; set; }
}