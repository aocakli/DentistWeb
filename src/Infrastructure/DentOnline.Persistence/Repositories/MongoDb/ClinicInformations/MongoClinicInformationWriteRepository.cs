using DentOnline.Application.Repositories.ClinicInformations;
using DentOnline.Domain.Concrete.ClinicInformations;
using DentOnline.Persistence.Repositories.MongoDb._Bases;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb.ClinicInformations;

public class MongoClinicInformationWriteRepository : MongoWriteRepositoryBase<ClinicInformation>,
    IClinicInformationWriteRepository
{
    public MongoClinicInformationWriteRepository(MongoClient mongoClient, IClientSessionHandle clientSessionHandle) :
        base(mongoClient, clientSessionHandle)
    {
    }
}