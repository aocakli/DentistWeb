using DentOnline.Application.Repositories.ClinicInformations;
using DentOnline.Domain.Concrete.ClinicInformations;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.ClinicInformations;

public class MongoClinicInformationReadRepository : MongoReadRepositoryBase<ClinicInformation>,
    IClinicInformationReadRepository
{
    public MongoClinicInformationReadRepository(MongoClient mongoClient) : base(mongoClient)
    {
    }
}