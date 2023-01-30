using DentOnline.Domain.Abstractions;

namespace DentOnline.Application.Repositories._Bases;

public interface IWriteRepository<T> where T : IDocument
{
    public Task<T> CreateAsync(T document);

    public Task<IEnumerable<T>> CreateBulkAsync(IEnumerable<T> documents);

    public Task UpdateAsync(string id, T document);
    public Task<bool> SaveChangesAsync();
}