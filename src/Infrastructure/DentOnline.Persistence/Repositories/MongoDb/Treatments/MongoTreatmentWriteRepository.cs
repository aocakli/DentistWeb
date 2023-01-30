using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Domain.Concrete.Treatments._Bases;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.Treatments;

public class MongoTreatmentWriteRepository : MongoWriteRepositoryBase<Treatment>, ITreatmentWriteRepository
{
    public MongoTreatmentWriteRepository(MongoClient mongoClient, IClientSessionHandle clientSessionHandle) : base(
        mongoClient, clientSessionHandle)
    {
    }
}