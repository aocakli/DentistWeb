using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Commands.CreateUserVerification;

public class CreateUserVerificationCommandRequest : IRequest<IResponse>
{
    public CreateUserVerificationCommandRequest()
    {
    }

    public CreateUserVerificationCommandRequest(string userId, UserVerificationType verificationType,
        bool ısSendNotificationToUser)
    {
        UserId = userId;
        VerificationType = verificationType;
        IsSendNotificationToUser = ısSendNotificationToUser;
    }

    public string UserId { get; set; }
    public UserVerificationType VerificationType { get; set; }
    public bool IsSendNotificationToUser { get; set; }
}