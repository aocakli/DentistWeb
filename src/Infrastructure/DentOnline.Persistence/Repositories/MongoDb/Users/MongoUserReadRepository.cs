using DentOnline.Application.Repositories.Users;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Domain.Concrete.Users.OtherDomains.UserRoles.Enums;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.Users;

public class MongoUserReadRepository : MongoReadRepositoryBase<User>, IUserReadRepository
{
    public MongoUserReadRepository(MongoClient mongoClient) : base(mongoClient)
    {
    }

    public async Task<HashSet<UserRoles>> GetUserRolesByUserIdAsync(string userId)
    {
        var userRoles = await Collection
            .Find(_user => _user.Id.Equals(userId))
            .Project(_user => _user.Roles)
            .FirstOrDefaultAsync();

        return userRoles.ToHashSet();
    }

    public async Task<HashSet<string>> GetEmailsByRoleAsync(UserRoles role)
    {
        var emailAddresses = await Collection
            .Find(_user => _user.Roles.Any(_role => _role.Equals(role)))
            .Project(_user => _user.Email)
            .ToListAsync();

        return emailAddresses.ToHashSet();
    }
}