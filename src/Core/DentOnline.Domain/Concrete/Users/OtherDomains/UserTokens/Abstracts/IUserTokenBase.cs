namespace DentOnline.Domain.Concrete.Users.OtherDomains.UserTokens.Abstracts;

public interface IUserTokenBase
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
}