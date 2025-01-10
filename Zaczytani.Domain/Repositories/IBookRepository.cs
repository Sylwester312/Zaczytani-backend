using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookRepository
{
    Task AddAsync(Book entity);
    IQueryable<Book> GetBySearchPhrase(string searchPhrase);
    Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    IQueryable<Book> GetUnseenBooks(Guid userId);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task DeleteAsync(Book entity,CancellationToken cancellation);
}
