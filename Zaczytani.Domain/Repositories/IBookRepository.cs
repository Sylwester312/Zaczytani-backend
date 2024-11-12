using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookRepository
{
    Task AddAsync(Book entity);
    IQueryable<Book> GetBySearchPhrase(string searchPhrase);
    Task<Book?> GetByIdAsync(Guid bookId);
    Task<Author?> GetAuthorByIdAsync(Guid authorId);
    Task SaveChangesAsync();
}
