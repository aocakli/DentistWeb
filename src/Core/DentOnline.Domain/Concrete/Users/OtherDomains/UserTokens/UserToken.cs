using DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens.Abstracts;

namespace DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens;

public class UserToken : IUserTokenBase
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
}