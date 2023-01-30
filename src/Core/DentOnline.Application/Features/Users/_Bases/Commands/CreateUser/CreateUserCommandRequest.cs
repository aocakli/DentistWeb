using System.Text.Json.Serialization;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users._Bases.Abstracts;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Commands.CreateUser;

public class CreateUserCommandRequest : IUserBase, IRequest<IDataResponse<User>>
{
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    [JsonIgnore] public ICollection<UserRoles>? Roles { get; set; }

    [JsonIgnore] public bool IsSaveChanges { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}