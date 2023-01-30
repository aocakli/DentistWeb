using DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Mappings;

public class UserVerificationMappings : Profile
{
    public UserVerificationMappings()
    {
        CreateMap<GenerateUserVerificationCommandRequest, UserVerification>()
            .ForMember(x => x.CreatedDate, mopt => mopt.MapFrom(v => DateTime.UtcNow));
    }
}