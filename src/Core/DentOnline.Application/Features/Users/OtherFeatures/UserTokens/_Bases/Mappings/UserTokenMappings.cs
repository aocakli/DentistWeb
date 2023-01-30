using DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Dtos;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserTokens._Bases.Mappings;

public class UserTokenMappings : Profile
{
    public UserTokenMappings()
    {
        CreateMap<UserToken, UserTokenDto>();
    }
}