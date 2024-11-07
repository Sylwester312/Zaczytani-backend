using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IBookRepository
{
    Task AddAsync(Book entity);
    Task SaveChangesAsync();
}
