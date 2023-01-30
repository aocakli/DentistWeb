namespace DentOnline.Domain.Concrete.Users._Bases.Abstracts;

public interface IUserBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}