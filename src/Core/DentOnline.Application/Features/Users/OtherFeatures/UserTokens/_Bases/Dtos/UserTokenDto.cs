using DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Dtos;

public class UserTokenDto : UserToken
{
    public UserTokenDto()
    {
    }

    public UserTokenDto(string token, DateTime tokenExpiryDate)
    {
        Token = token;
        ExpiryDate = tokenExpiryDate;
    }
}