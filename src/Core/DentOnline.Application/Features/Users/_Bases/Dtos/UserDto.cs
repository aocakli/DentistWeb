using DentOnline.Application.Abstracts;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserVerifications;

namespace DentOnline.Application.Features.Users._Bases.Dtos;

public class UserDto : DtoBase
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string FullName => string.Join(' ', Name, Surname);

    public string Email { get; set; }

    public UserRoleDto Role { get; set; }

    public ICollection<UserVerification> UserVerifications { get; set; }
}