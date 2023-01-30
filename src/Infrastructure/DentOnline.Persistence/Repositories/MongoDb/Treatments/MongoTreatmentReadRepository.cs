using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Domain.Concrete.Treatments._Bases;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.Treatments;

public class MongoTreatmentReadRepository : MongoReadRepositoryBase<Treatment>, ITreatmentReadRepository
{
    public MongoTreatmentReadRepository(MongoClient mongoClient) : base(mongoClient)
    {
    }
}