using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Enums;

namespace DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications.Abstracts;

public interface IUserVerificationBase
{
    public UserVerificationType VerificationType { get; set; }
    public string Code { get; set; }
    public DateTime ExpiryDate { get; set; }
}