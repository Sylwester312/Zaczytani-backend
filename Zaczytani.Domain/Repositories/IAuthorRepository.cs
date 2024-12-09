using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IAuthorRepository
{
    IQueryable<Author> GetAll();
    Task<Author?> GetByIdAsync(Guid authorId);
    Task<Author?> GetByNameAsync(string name);
    Task SaveChangesAsync();
}
