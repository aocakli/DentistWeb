using DentOnline.Application.Repositories._Bases;
using DentOnline.Domain.Abstractions;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb._Bases;

public class MongoWriteRepositoryBase<T> : MongoRepositoryBase<T>, IWriteRepository<T> where T : IDocument
{
    protected readonly IClientSessionHandle ClientSessionHandle;

    public MongoWriteRepositoryBase(MongoClient mongoClient, IClientSessionHandle clientSessionHandle) :
        base(mongoClient)
    {
        ClientSessionHandle = clientSessionHandle;

        // ClientSessionHandle = MongoClient.StartSession();
        //
        // ClientSessionHandle.StartTransaction();
    }

    public async Task<T> CreateAsync(T document)
    {
        await Collection.InsertOneAsync(ClientSessionHandle, document);

        return document;
    }

    public async Task<IEnumerable<T>> CreateBulkAsync(IEnumerable<T> documents)
    {
        await Collection.InsertManyAsync(ClientSessionHandle, documents);

        return documents;
    }

    public async Task UpdateAsync(string id, T document)
    {
        await Collection.FindOneAndReplaceAsync(x => x.Id.Equals(id), document);
    }

    public async Task<bool> SaveChangesAsync()
    {
        await ClientSessionHandle.CommitTransactionAsync();

        return true;
    }
}