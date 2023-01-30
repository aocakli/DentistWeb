using DentOnline.Domain.Abstractions;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb._Bases;

public abstract class MongoRepositoryBase<T> where T : IDocument
{
    protected readonly IMongoCollection<T> Collection;
    protected readonly MongoClient MongoClient;

    public MongoRepositoryBase(MongoClient mongoClient)
    {
        MongoClient = mongoClient;

        // var client = new MongoClient("mongodb+srv://dentonline:Dentonline456!@dentonlinefirstcluster.ixeared.mongodb.net/?retryWrites=true&w=majority");

        var collectionName = typeof(T).Name.ToLowerInvariant();

        Collection = MongoClient.GetDatabase("dentonline").GetCollection<T>(collectionName);
    }
}