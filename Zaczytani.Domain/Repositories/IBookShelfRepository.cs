using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookShelfRepository
{
    Task<IEnumerable<BookShelf>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<BookShelf?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BookShelf?> GetByIdWithBooksAsync(Guid shelfId, Guid userId, CancellationToken cancellationToken);
    Task AddAsync(BookShelf bookshelf, CancellationToken cancellationToken);
    Task UpdateAsync(BookShelf bookshelf, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
