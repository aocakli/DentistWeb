using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.GenerateUserVerification;

public class GenerateUserVerificationCommandRequest : IRequest<IDataResponse<UserVerification>>
{
    public GenerateUserVerificationCommandRequest()
    {
    }

    public GenerateUserVerificationCommandRequest(UserVerificationType verificationType)
    {
        VerificationType = verificationType;
    }

    public UserVerificationType VerificationType { get; set; }
}