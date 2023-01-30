using DentOnline.Application.Repositories._Bases;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;

namespace DentOnline.Application.Repositories.Users;

public interface IUserReadRepository : IReadRepository<User>
{
    Task<HashSet<UserRoles>> GetUserRolesByUserIdAsync(string userId);

    Task<HashSet<string>> GetEmailsByRoleAsync(UserRoles role);
}