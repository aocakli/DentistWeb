using System.Linq.Expressions;
using DentOnline.Application.Repositories._Bases;
using DentOnline.Application.Utilities.Paginations;
using DentOnline.Domain.Abstractions;
using MongoDB.Driver;

namespace DentOnline.Persistence.Repositories.MongoDb._Bases;

public class MongoReadRepositoryBase<T> : MongoRepositoryBase<T>, IReadRepository<T> where T : IDocument
{
    public MongoReadRepositoryBase(MongoClient mongoClient) : base(mongoClient)
    {
    }

    public async Task<HashSet<string>> GetIdentitiesAsync(PaginationBase? pagination = null)
    {
        var query = Collection
            .Find(x => true);

        if (pagination is not null)
        {
            if (pagination.ItemCount.HasValue)
                query = query.Limit(pagination.ItemCount);

            query = query.Skip((pagination.Page - 1) * pagination.ItemCount ?? 0);
        }

        var datas = await query
            .Project(x => x.Id)
            .SortByDescending(x => x.CreatedDate)
            .ToListAsync();

        return datas.ToHashSet();
    }

    public async Task<HashSet<string>> GetIdentitiesAsync(Expression<Func<T, bool>> exp)
    {
        var datas = await Collection
            .Find(exp)
            .Project(x => x.Id)
            .SortByDescending(x => x.CreatedDate)
            .ToListAsync();

        return datas.ToHashSet();
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await Collection.Find(x => true).ToListAsync();
    }

    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> exp)
    {
        return await Collection.Find(exp).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp)
    {
        return await Collection.Find(exp).AnyAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> exp)
    {
        return await Collection.Find(exp).FirstOrDefaultAsync();
    }

    public async Task<long> CountAsync()
    {
        return await Collection.CountDocumentsAsync(x => true);
    }
}