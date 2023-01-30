using DentOnline.Application.Repositories.Users;
using DentOnline.Domain.Concrete.Users._Bases;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.Users;

public class MongoUserWriteRepository : MongoWriteRepositoryBase<User>, IUserWriteRepository
{
    public MongoUserWriteRepository(MongoClient mongoClient, IClientSessionHandle clientSessionHandle) : base(
        mongoClient, clientSessionHandle)
    {
    }
}