using System.Text.Json.Serialization;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Application.Features.Users.OtherFeatures.UserVerifications.Queries.CheckAndVerifyUserVerification;

public class CheckAndVerifyUserVerificationQueryRequest : IRequest<IResponse>
{
    [JsonIgnore] public UserVerificationType VerificationType { get; set; }

    public string Code { get; set; }
}