using System.Linq.Expressions;
using DentOnline.Application.Utilities.Paginations;
using DentOnline.Domain.Abstractions;

namespace DentOnline.Application.Repositories._Bases;

public interface IReadRepository<T> where T : IDocument
{
    public Task<HashSet<string>> GetIdentitiesAsync(PaginationBase? pagination = null);

    public Task<HashSet<string>> GetIdentitiesAsync(Expression<Func<T, bool>> exp);

    public Task<ICollection<T>> GetAllAsync();

    public Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> exp);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> exp);

    public Task<T> GetAsync(Expression<Func<T, bool>> exp);

    public Task<long> CountAsync();
}