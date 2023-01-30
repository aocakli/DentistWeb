using System.Text.Json.Serialization;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Features.Users._Bases.Dtos;

public class UserRoleDto
{
    public UserRoleDto()
    {
    }

    public UserRoleDto(HashSet<UserRoles> rolesCollection)
    {
        UserRolesCollection = rolesCollection;
    }

    [JsonIgnore] public HashSet<UserRoles> UserRolesCollection { get; }

    public HashSet<string> Roles => UserRolesCollection.Select(_userRole => _userRole.ToString()).ToHashSet();

    public bool IsDoctor => UserRolesCollection.Any(_role => _role == UserRoles.Doctor);
    public bool IsAdmin => UserRolesCollection.Any(_role => _role == UserRoles.Admin);
}